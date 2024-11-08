using iSoft.Common.ConfigsNS;
using iSoft.Common.ExtensionMethods;
using iSoft.Common.Utils;
using iSoft.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SourceBaseBE.Database.Entities.TraceData;
using SourceBaseBE.Database.DBContexts;
using iSoft.DBLibrary.DBConnections.Factory;
using SourceBaseBE.CommonFunc.DataService;
using SourceBaseBE.CommonFunc.EnvConfigDataNS;
using iSoft.Common.Enums;
using Npgsql;
using iSoft.Redis.Services;
using iSoft.InfluxDB.Services;
using SourceBaseBE.CommonFunc.Services.HostedServices;
using iSoft.Common.Models.ConfigModel.Subs;
using iSoft.RabbitMQ.EnumNS;
using iSoft.Common.Payloads;
using System.Threading;

namespace iSoft.RabbitMQ.Services
{
    public class TraceDataConsumerHostedService : DBConsumerService
    {
        private ImportDataFactory importDataFactory = null;
        Dictionary<long, DeviceConnectionEntity> dicConnEntity0 = new Dictionary<long, DeviceConnectionEntity>();
        InfluxDBService _influxDBService;
        //private string influxORG = Environment.GetEnvironmentVariable("INFLUXDB_CONFIG__ORGANIZATION");
        //private string influxORGId = Environment.GetEnvironmentVariable("INFLUXDB_CONFIG__ORGANIZATION_ID");
        //private string bucket = Environment.GetEnvironmentVariable("INFLUXDB_CONFIG__DATABASE_NAME");
        //private List<KeyValuePair<string, string>> Tags = new List<KeyValuePair<string, string>>()
        //{
        //        KeyValuePair.Create("data", "trace"),
        //};
        public TraceDataConsumerHostedService(
                DBServerConfigModel dbConfig,
                ILogger<TraceDataConsumerHostedService> logger,
                RabbitMQService rabbitMQService,
                InfluxDBService influxDBService,
                string queueName)
            : base(logger, rabbitMQService, queueName)
        {
            this._influxDBService = influxDBService;
            this.dbConfig = dbConfig;
            lock (lockObj_initDB)
            {
                CachedSupportFunc.SetRedisConfig(CommonConfig.GetConfig().RedisSupportConfig);
                DBConnectionStatus = initDB(this.dbConfig);
                //if (CommonConfig.GetConfig().UseInfluxDB == 1)
                //{
                //    _influxDBService.CreateBucket();
                //}
            }
            //var bucketRes = _influxDBService.CreateBucket("bucket", "influxORGId", "Create By TraceDataService").Result;
        }

        public override EnumConnectionStatus initDB(DBServerConfigModel dbConfig)
        {
            try
            {
                _logger.LogInformation($"*** TRY CONNECT DATABASE *** {dbConfig.GetHostName()}");

                this._connCus = DBConnectionFactory.CreateDBConnection(dbConfig);
                this._dbContext = new CommonDBContext(_connCus);
                this.conn = _connCus.GetConnection();
                this.conn.Open();

                InitRepository();

                _logger.LogInformation("*** CONNECT DATABASE SUCCESS ***");
                return EnumConnectionStatus.Connected;
            }
            catch (NpgsqlException ex)
            {
                if (ex.SqlState == "3D000")
                {
                    var connCus = DBConnectionFactory.CreateDBConnection(dbConfig);
                    bool result = TraceDBContext.CreateDatabase(connCus).Result;
                    if (result)
                    {
                        _logger.LogInformation("*** CREATE DATABASE SUCCESS ***");
                    }
                    return EnumConnectionStatus.Error;
                }
                else
                {
                    _logger.LogMsg(Messages.ErrException, ex);
                    return EnumConnectionStatus.Error;
                }
            }
            //catch (SqlException ex)
            //{
            //    if (ex.Number == 4060)
            //    {
            //        var connCus = DBConnectionFactory.CreateDBConnection(dbConfig);
            //        bool result = TraceDBContext.CreateDatabase(connCus).Result;
            //        if (result)
            //        {
            //            _logger.LogInformation("*** CREATE DATABASE SUCCESS ***");
            //        }
            //        return EnumConnectionStatus.Error;
            //    }
            //    else
            //    {
            //        _logger.LogMsg(Messages.ErrException, ex);
            //        return EnumConnectionStatus.Error;
            //    }
            //}
            catch (Exception ex)
            {
                var connCus = DBConnectionFactory.CreateDBConnection(dbConfig);
                bool result = TraceDBContext.CreateDatabase(connCus).Result;
                if (result)
                {
                    _logger.LogInformation("*** CREATE DATABASE SUCCESS ***");
                }
                return EnumConnectionStatus.Error;

                //_logger.LogMsg(Messages.ErrException, ex);
                //return EnumConnectionStatus.Error;
            }
        }
        public override void InitRepository()
        {
            while (true)
            {
                try
                {
                    var listEnvConfig = EnvConfigData.Ins.GetListEnvConfigModel();
                    importDataFactory = new ImportDataFactory(conn, _connCus, listEnvConfig, null, _influxDBService);

                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogMsg(Messages.ErrException, ex);
                }

                Thread.Sleep(10000);
            }
        }
        public override EnumHandleMessageStatus handleMessageImp(DevicePayloadMessage message)
        {
            string funcName = nameof(handleMessageImp);
            DateTime startTime = DateTime.Now;

            if (message.Data == null || message.Data.Count <= 0 || message.ConnectionId == null || message.MessageId == null)
            {
                this._logger.LogMsg(Messages.ErrInputInvalid_0_1, funcName, message.MessageId + ", " + $"json: {message.ToJson()}");
                return EnumHandleMessageStatus.DeleteMessageStatus;
            }

            // string hashKey = $"TraceDataConsumer_{message.GetHashKey()}";
            // if (CachedSupportFunc.GetRedisData<bool>(hashKey, false) == false)
            // {
            //     RefreshConnectivityDataJob.AddHashKey(hashKey);

            //     string logKey = $"handleMessageImp_check_hashKey_5m_";
            //     if (!cached.ContainsKey(logKey))
            //     {
            //         cached.AddToCache(logKey, true, 60 * 5);

            //         _logger.LogWarning($"_5m_ [{funcName}] Message Keys Changes" +
            //             $", MessageId: {message.MessageId}" +
            //             $", ConnectionId: {message.ConnectionId}" +
            //             $", Keys: {message.Data.Select(x => x.Key).ToJson()}" +
            //             $"");
            //     }
            //     else
            //     {
            //         _logger.LogWarning($"_5m_ [{funcName}] Message Keys Changes" +
            //             $", MessageId: {message.MessageId}" +
            //             $", ConnectionId: {message.ConnectionId}" +
            //             $"");
            //     }

            //     return EnumHandleMessageStatus.RetryMessageStatus;
            // }

            if (CommonConfig.GetConfig().UsePostgres == 1)
            {
                // Insert to Postgres or SQL Server
                importDataFactory.ImportTraceData(message, this.dbConfig.DatabaseType);
            }

            if (CommonConfig.GetConfig().UseInfluxDB == 1)
            {
                // Insert to InfluxDB
                //SaveTSDb(message);
                importDataFactory.ImportInfluxDB(message);
            }

            try
            {
                _rabbitMQService.PushMessage(message, true, TopicName.OEEDataInputTopic);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[handleMessageImp] PushMessage to OEEDataInputTopic Error, {message.MessageId}, {ex.Message}");
            }

            try
            {
                _rabbitMQService.PushMessage(message, true, TopicName.SearchDataTopic);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[handleMessageImp] PushMessage to SearchDataTopic Error, {message.MessageId}, {ex.Message}");
            }

            _logger.LogInformation($"[handleMessageImp] Finished, {message.MessageId} {DateTimeUtil.GetHumanStr(DateTime.Now - startTime)}");

            return EnumHandleMessageStatus.SuccessStatus;
        }
        //public async Task SaveTSDb(DevicePayloadMessage message)
        //{
        //    try
        //    {
        //        var fields = new Dictionary<string, long>();
        //        message.Data.ForEach(x => fields.Add(x.Key, long.Parse(x.Value)));
        //        await _influxDBService.Write<long>(influxORG, bucket, DateTime.UtcNow, message.ConnectionId, Tags, fields);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
