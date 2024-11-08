using iMag.Oee.Models.RequestModels;
using iSoft.Common.ConfigsNS;
using iSoft.Common.Enums;
using iSoft.Common.Exceptions;
using iSoft.Common.ExtensionMethods;
using iSoft.Common.Payloads;
using iSoft.Common.ResponseObjectNS;
using iSoft.Common.Services;
using iSoft.Common.Utils;
using Newtonsoft.Json;
using SourceBaseBE.CommonFunc.EnvConfigDataNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceBaseBE.CommonFunc.APIs
{
    public static class ConnectivityAPIs
    {
        public static async Task<List<ConnectionDataModel>> GetListConnectionData()
        {
            try
            {
                string funcName = nameof(GetListConnectionData);

                var dic = new Dictionary<string, string>();
                var xApiKey = CommonConfig.GetConfig().ConnectivityConfig.APIKey;
                string URL = CommonConfig.GetConfig().ConnectivityConfig.GetHostName() + $"/api/v1/Connection/get-list?page=1&pageSize=10000";
                dic.Add("X-Api-Key", xApiKey);
                try
                {
                    HttpServiceResponse httpServiceResponse = await HttpService.GetAsync(URL, dic);

                    if (httpServiceResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ResponseObject ro = JsonConvert.DeserializeObject<ResponseObject>(httpServiceResponse.ResponseString);
                        if (ro.Data == null || string.IsNullOrEmpty(ro.Data.ToString()))
                        {
                            throw new BaseException($"{funcName} Response data is null or empty, URL: {URL}");
                        }

                        string receivedStr = ro.Data.ToString();
                        //string jsonStr = DataCipher.DecryptASE(receivedStr);
                        string jsonStr = receivedStr;

                        ConnectionResponseModel responseData = JsonConvert.DeserializeObject<ConnectionResponseModel>(jsonStr);

                        return responseData.ListData;
                    }
                    else
                    {
                        throw new BaseException($"{funcName} StatusCode error, StatusCode: {httpServiceResponse.StatusCode}, URL: {URL}");
                    }
                }
                catch (Exception ex)
                {
                    throw new BaseException($"{funcName} Call API to connectivity error, URL: {URL}", ex);
                }
            }
            catch (Exception ex)
            {
                throw new BaseException(ex);
            }
        }
        public static async Task<List<ConnectionTagDataModel>> GetListConnectionTagData(string? connectionId = null)
        {
            try
            {
                string funcName = nameof(GetListConnectionTagData);

                var dic = new Dictionary<string, string>();
                var xApiKey = CommonConfig.GetConfig().ConnectivityConfig.APIKey;
                string URL = CommonConfig.GetConfig().ConnectivityConfig.GetHostName() + $"/api/v1/ConnectionTag/get-list?page=1&pageSize=10000";
                if (connectionId != null)
                {
                    URL = CommonConfig.GetConfig().ConnectivityConfig.GetHostName() + $"/api/v1/ConnectionTag/get-connection-tags?ConnectionId={connectionId}";
                }
                dic.Add("X-Api-Key", xApiKey);
                try
                {
                    HttpServiceResponse httpServiceResponse = await HttpService.GetAsync(URL, dic);

                    if (httpServiceResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ResponseObject ro = JsonConvert.DeserializeObject<ResponseObject>(httpServiceResponse.ResponseString);
                        if (ro.Data == null || string.IsNullOrEmpty(ro.Data.ToString()))
                        {
                            throw new BaseException($"{funcName} Response data is null or empty, URL: {URL}");
                        }

                        string receivedStr = ro.Data.ToString();
                        //string jsonStr = DataCipher.DecryptASE(receivedStr);
                        string jsonStr = receivedStr;

                        ConnectionTagResponseModel responseData = JsonConvert.DeserializeObject<ConnectionTagResponseModel>(jsonStr);

                        return responseData.ListData;
                    }
                    else
                    {
                        throw new BaseException($"{funcName} StatusCode error, StatusCode: {httpServiceResponse.StatusCode}, URL: {URL}");
                    }
                }
                catch (Exception ex)
                {
                    throw new BaseException($"{funcName} Call API to connectivity error, URL: {URL}", ex);
                }
            }
            catch (Exception ex)
            {
                throw new BaseException(ex);
            }
        }

        public static async Task UpsertConnection(ConnectionDataModel data)
        {
            string funcName = nameof(UpsertConnection);
            try
            {
                var dicHeader = new Dictionary<string, string>();
                var xApiKey = CommonConfig.GetConfig().ConnectivityConfig.APIKey;
                var URL = CommonConfig.GetConfig().ConnectivityConfig.GetHostName() + $"/api/v1/Connection/upsert"; // TODO: move to const
                dicHeader.Add("X-Api-Key", xApiKey);

                //string json = "";
                HttpServiceResponse httpServiceResponse = await HttpService.PostAsync(URL, HttpMethod.Post, dicHeader, data, "", "", true);

                if (httpServiceResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new BaseException($"[{funcName}] StatusCode Error, " +
                        $"StatusCode: {httpServiceResponse.StatusCode}, " +
                        $"response: {httpServiceResponse.ResponseString}" +
                        $"");
                }
            }
            catch (Exception ex)
            {
                throw new BaseException($"[{funcName}] Call API Error, " +
                    $"message: {data.ToJson()}, " +
                    $"", ex);
            }
        }

        public static async Task UpsertConnectionTag(ConnectionTagDataModel data)
        {
            string funcName = nameof(UpsertConnectionTag);
            try
            {
                var dicHeader = new Dictionary<string, string>();
                var xApiKey = CommonConfig.GetConfig().ConnectivityConfig.APIKey;
                var URL = CommonConfig.GetConfig().ConnectivityConfig.GetHostName() + $"/api/v1/ConnectionTag/upsert"; // TODO: move to const
                dicHeader.Add("X-Api-Key", xApiKey);

                //string json = "";
                HttpServiceResponse httpServiceResponse = await HttpService.PostAsync(URL, HttpMethod.Post, dicHeader, data, "", "", true);

                if (httpServiceResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new BaseException($"[{funcName}] StatusCode Error, " +
                        $"StatusCode: {httpServiceResponse.StatusCode}, " +
                        $"response: {httpServiceResponse.ResponseString}" +
                        $"");
                }
            }
            catch (Exception ex)
            {
                throw new BaseException($"[{funcName}] Call API Error, " +
                    $"message: {data.ToJson()}, " +
                    $"", ex);
            }
        }

        public static async Task SendDataToConnectivityAsync(DevicePayloadMessage message)
        {
            string funcName = nameof(SendDataToConnectivityAsync);
            try
            {
                var dicHeader = new Dictionary<string, string>();
                var xApiKey = CommonConfig.GetConfig().ConnectivityConfig.APIKey;
                var URL = CommonConfig.GetConfig().ConnectivityConfig.GetHostName() + $"/api/v1/ConnectionTag/update-value";
                dicHeader.Add("X-Api-Key", xApiKey);

                //string json = "";
                HttpServiceResponse httpServiceResponse = await HttpService.PostAsync(URL, HttpMethod.Post, dicHeader,
                    message);

                if (httpServiceResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new BaseException($"[{funcName}] StatusCode Error, , " +
                        $"StatusCode: {httpServiceResponse.StatusCode}, " +
                        $"response: {httpServiceResponse.ResponseString}" +
                        $"");
                }
            }
            catch (Exception ex)
            {
                throw new BaseException($"[{funcName}] Call API Error, " +
                    $"message: {message.ToJson()}, " +
                    $"", ex);
            }
        }
    }
}
