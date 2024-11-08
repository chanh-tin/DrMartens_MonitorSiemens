#define VIRTUAL_MODEx

using iSoft.Common.Enums;
using iSoft.Common.Util;
using iSoft.Common.Utils;
using iSoft.ExcelHepler;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SourceBaseBE.Database.Entities;
using iSoft.Common.Services;
using Elasticsearch.Net;
using iSoft.Common.ConfigsNS;
using iSoft.Common.Exceptions;
using iSoft.Common.Models.ConfigModel;
using iSoft.Common.ResponseObjectNS;
using iSoft.Common.Security;
using iSoft.Common;
using Newtonsoft.Json;
using iSoft.Common.ExtensionMethods;
using SourceBaseBE.CommonFunc.APIs;
using Org.BouncyCastle.Asn1.Ocsp;
using iMag.Oee.Models.RequestModels;

namespace SourceBaseBE.CommonFunc.EnvConfigDataNS
{

    public class EnvConfigData
    {
        private static object lockObj = new object();
        private static EnvConfigData ins;
        private bool isLoadingEnvConfig = false;
        private List<EnvConfigModel> arrExampleEnvConfig;
        private List<ConnectionDataModel> listConnection;
        private Dictionary<string, ConnectionDataModel> dicConnection = new Dictionary<string, ConnectionDataModel>();
        public ConnectionDataModel oeeConnection;
        private Dictionary<string, EnvConfigModel> dicEnvConfig = new Dictionary<string, EnvConfigModel>();
        private Dictionary<string, EnvConfigModel> dicEnvKey2EnvConfig = new Dictionary<string, EnvConfigModel>();
        private EnvConfigData() { }
        public static EnvConfigData Ins
        {
            get
            {
                lock (lockObj)
                {
                    if (ins == null)
                    {
                        ins = new EnvConfigData();
                    }
                    if (!ins.isLoadingEnvConfig)
                    {

#if VIRTUAL_MODE
                        ins.GetEnvConfigFromExcel().Wait();
#else
                        //ins.GetEnvConfigFromConnectivity("c45c475d-e8c0-4ad8-8daa-9203c349cb6a");
                        //ins.GetEnvConfigFromConnectivity("a63a94be-8b39-4ddc-82d1-b78a1f17bdf9");

                        ins.ReloadConnectivityData();
#endif
                        ins.isLoadingEnvConfig = true;
                    }
                    return ins;
                }
            }
        }

        public void ReloadConnectivityData(bool is1stLevel = true)
        {
            lock (lockObj)
            {
                ins.listConnection = ConnectivityAPIs.GetListConnectionData().Result;
                ins.dicConnection = ins.listConnection.ToDictionary(x => x.Id, x => x);
                ins.oeeConnection = ins.listConnection.FirstOrDefault(x => x.Name == "OEEService");

                ins.GetEnvConfigFromConnectivity();

                if (is1stLevel
                    && ins.oeeConnection == null)
                {
                    ConnectivityAPIs.UpsertConnection(new ConnectionDataModel()
                    {
                        Name = "OEEService",
                        Category = "OEE",
                        ConnProtocolType = "Virtual",
                        ConnType = "In_Out",
                    }).Wait();
                    ins.ReloadConnectivityData(false);
                }
            }
        }

        public List<EnvConfigModel> GetListEnvConfigModel() => this.arrExampleEnvConfig;
        private async Task<List<EnvConfigModel>> GetEnvConfigFromExcel(string filePath = "./setting/mapping.xlsx")
        {
            try
            {
                List<EnvConfigModel> listParams = new List<EnvConfigModel>();
                //var path 
                var mapping = (await ExcelHepler.GetSheet<EnvConfigModel>(filePath, "Parameter", "A1")).ToList();
                for (int i = mapping.Count - 1; i >= 0; i--)
                {
                    if (!mapping[i].IsValid())
                    {
                        mapping.RemoveAt(i);
                        continue;
                    }
                    mapping[i].EnviromentVarName = mapping[i].EnviromentVarName;
                }
                arrExampleEnvConfig = mapping;

                foreach (var item in arrExampleEnvConfig)
                {
                    if (!dicEnvConfig.ContainsKey(item.GetKey()))
                    {
                        dicEnvConfig.Add(item.GetKey(), item);
                    }

                    if (!dicEnvKey2EnvConfig.ContainsKey(item.EnviromentVarName.ConvertToESField("")))
                    {
                        dicEnvKey2EnvConfig.Add(item.EnviromentVarName.ConvertToESField(""), item);
                    }
                }

                return arrExampleEnvConfig;
            }
            catch (Exception ex)
            {

                throw new BaseException(ex);
            }
        }
        private void GetEnvConfigFromConnectivity()
        {
            List<ConnectionTagDataModel> listConnectionTag = ConnectivityAPIs.GetListConnectionTagData().Result;

            List<EnvConfigModel> list = getLisEnvConfig(listConnectionTag);

            if (arrExampleEnvConfig == null)
            {
                arrExampleEnvConfig = new List<EnvConfigModel>();
            }

            arrExampleEnvConfig.AddRange(list);

            foreach (var item in list)
            {
                if (!dicEnvConfig.ContainsKey(item.GetKey()))
                {
                    dicEnvConfig.Add(item.GetKey(), item);
                }

                if (!dicEnvKey2EnvConfig.ContainsKey(item.EnviromentVarName.ConvertToESField("")))
                {
                    dicEnvKey2EnvConfig.Add(item.EnviromentVarName.ConvertToESField(""), item);
                }
            }
        }

        private List<EnvConfigModel> getLisEnvConfig(List<ConnectionTagDataModel> listConnectionTag)
        {
            List<EnvConfigModel> rs = new List<EnvConfigModel>();
            foreach (var item in listConnectionTag)
            {
                EnvConfigModel envConfigModel = new EnvConfigModel();
                envConfigModel.ConnectionId = item.ConnectionId;
                envConfigModel.EnviromentVarName = item.EnvKey.ToString();
                envConfigModel.Name = item.Name;
                envConfigModel.Type = item.Type.HasValue ? item.Type.Value : EnumDataType.Long;

                string tableSerial = $"{ins.dicConnection[item.ConnectionId].Name}_{item.ConnectionId}".ConvertToESField("");
                tableSerial = tableSerial.SubstringSafe(0, 36);
                //if (tableSerial.Length > 36)
                //{
                //    tableSerial = EncodeUtil.MD5(tableSerial).SubstringSafe(0, 36);
                //}

                envConfigModel.TableSerial = tableSerial;
                envConfigModel.MinTimeIntervalInSeconds = 0;

                rs.Add(envConfigModel);
            }
            return rs;
        }

        public EnvConfigModel GetEnvConfigBySearchField(string searchField, string connectionId)
        {
            if (string.IsNullOrEmpty(connectionId))
            {
                return GetEnvConfigBySearchField(searchField);
            }

            var key = searchField.ConvertToESField(connectionId);
            if (dicEnvConfig.ContainsKey(key))
            {
                return dicEnvConfig[key];
            }
            return null;
        }
        public EnvConfigModel GetEnvConfigBySearchField(string searchField)
        {
            var key = searchField.ConvertToESField("");
            if (dicEnvKey2EnvConfig.ContainsKey(key))
            {
                return dicEnvKey2EnvConfig[key];
            }
            return null;
        }
        public string GetSearchPatternByEnvESFieldName(string searchField, string connectionId)
        {
            if (string.IsNullOrEmpty(connectionId))
            {
                return arrExampleEnvConfig.FirstOrDefault(s => s.EnviromentVarName.ConvertToESField("") == searchField.ConvertToESField(""))?.GetESPatternSearch();
            }

            return arrExampleEnvConfig.FirstOrDefault(s => s.GetKey() == searchField.GetESKey(connectionId))?.GetESPatternSearch();
        }
    }

}
