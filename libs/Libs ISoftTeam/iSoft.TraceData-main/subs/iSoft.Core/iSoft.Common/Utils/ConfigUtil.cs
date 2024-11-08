#define MASTER_DBx
#define TRACE_DB
#define RABBITMQ
#define ELASTICSEARCHx
#define REDIS
#define SOCKETIOx
#define CONNECTIVITY
#define REMOTE_CONFIGx
#define INFLUXDB
#define FCM_CONFIGx

using iSoft.Common.ConfigsNS;
using iSoft.Common.Enums.DBProvider;
using iSoft.Common.Exceptions;
using iSoft.Common.Models.ConfigModel;
using iSoft.Common.Models.ConfigModel.Subs;
using iSoft.Common.ResponseObjectNS;
using iSoft.Common.Security;
using iSoft.Common.Services;
using iSoft.Common.Utils;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;

namespace iSoft.Common.Util
{
    public class ConfigUtil
    {

        public static SourceBaseBEConfigModel FillEnvKey(SourceBaseBEConfigModel configModel)
        {
            if (Environment.GetEnvironmentVariable("ENV") == null)
            {
                DotNetEnv.Env.Load();
            }

            configModel.ENV = Environment.GetEnvironmentVariable("ENV");
            Log.Logger.Information($"[FillEnvKey] ENV={configModel.ENV}");

            configModel.Version = Environment.GetEnvironmentVariable("VERSION");

            configModel.AuthenticationSecret = Environment.GetEnvironmentVariable("AUTHENTICATION_SECRET");

            configModel.UseInfluxDB = int.Parse(Environment.GetEnvironmentVariable("USE_INFLUXDB"));

            configModel.UsePostgres = int.Parse(Environment.GetEnvironmentVariable("USE_POSTGRES"));


#if MASTER_DB
            string? masterDatabaseDatabaseType = Environment.GetEnvironmentVariable("MASTER_DB_CONFIG__DATABASE_TYPE");
            string? masterDatabaseAddress = Environment.GetEnvironmentVariable("MASTER_DB_CONFIG__ADDRESS");
            string? masterDatabasePort = Environment.GetEnvironmentVariable("MASTER_DB_CONFIG__PORT");
            string? masterDatabaseDatabaseName = Environment.GetEnvironmentVariable("MASTER_DB_CONFIG__DATABASE_NAME");
            string? masterDatabaseUsername = Environment.GetEnvironmentVariable("MASTER_DB_CONFIG__USERNAME");
            string? masterDatabasePassword = Environment.GetEnvironmentVariable("MASTER_DB_CONFIG__PASSWORD");
#endif
#if TRACE_DB
            string? traceDatabaseDatabaseType = Environment.GetEnvironmentVariable("TRACE_DB_CONFIG__DATABASE_TYPE");
            string? traceDatabaseAddress = Environment.GetEnvironmentVariable("TRACE_DB_CONFIG__ADDRESS");
            string? traceDatabasePort = Environment.GetEnvironmentVariable("TRACE_DB_CONFIG__PORT");
            string? traceDatabaseDatabaseName = Environment.GetEnvironmentVariable("TRACE_DB_CONFIG__DATABASE_NAME");
            string? traceDatabaseUsername = Environment.GetEnvironmentVariable("TRACE_DB_CONFIG__USERNAME");
            string? traceDatabasePassword = Environment.GetEnvironmentVariable("TRACE_DB_CONFIG__PASSWORD");
#endif
#if RABBITMQ
            string? rabbitMQAddress = Environment.GetEnvironmentVariable("RABBITMQ_CONFIG__ADDRESS");
            string? rabbitMQPort = Environment.GetEnvironmentVariable("RABBITMQ_CONFIG__PORT");
            string? rabbitMQUsername = Environment.GetEnvironmentVariable("RABBITMQ_CONFIG__USERNAME");
            string? rabbitMQPassword = Environment.GetEnvironmentVariable("RABBITMQ_CONFIG__PASSWORD");
#endif
#if ELASTICSEARCH
            string? elasticSearchAddress = Environment.GetEnvironmentVariable("ELASTICSEARCH_CONFIG__ADDRESS");
            string? elasticSearchPort = Environment.GetEnvironmentVariable("ELASTICSEARCH_CONFIG__PORT");
            string? elasticSearchUsername = Environment.GetEnvironmentVariable("ELASTICSEARCH_CONFIG__USERNAME");
            string? elasticSearchPassword = Environment.GetEnvironmentVariable("ELASTICSEARCH_CONFIG__PASSWORD");
#endif
#if REDIS
            string? redisAddress = Environment.GetEnvironmentVariable("REDIS_CONFIG__ADDRESS");
            string? redisPort = Environment.GetEnvironmentVariable("REDIS_CONFIG__PORT");
            string? redisUsername = Environment.GetEnvironmentVariable("REDIS_CONFIG__USERNAME");
            string? redisPassword = Environment.GetEnvironmentVariable("REDIS_CONFIG__PASSWORD");

            string? redisSupportAddress = Environment.GetEnvironmentVariable("REDIS_SUPPORT_CONFIG__ADDRESS");
            string? redisSupportPort = Environment.GetEnvironmentVariable("REDIS_SUPPORT_CONFIG__PORT");
            string? redisSupportUsername = Environment.GetEnvironmentVariable("REDIS_SUPPORT_CONFIG__USERNAME");
            string? redisSupportPassword = Environment.GetEnvironmentVariable("REDIS_SUPPORT_CONFIG__PASSWORD");
#endif
#if SOCKETIO
            string? socketIOAddress = Environment.GetEnvironmentVariable("SOCKETIO_CONFIG__ADDRESS");
            string? socketIOPort = Environment.GetEnvironmentVariable("SOCKETIO_CONFIG__PORT");
#endif
#if CONNECTIVITY
            string? Connectivity_Address = Environment.GetEnvironmentVariable("CONNECTIVITY__ADDRESS");
            string? Connectivity_Port = Environment.GetEnvironmentVariable("CONNECTIVITY__PORT");
            string? Connectivity_APIKey = Environment.GetEnvironmentVariable("CONNECTIVITY__API_KEY");
#endif
#if REMOTE_CONFIG
            string? remoteConfigAddress = Environment.GetEnvironmentVariable("REMOTE_CONFIG__ADDRESS");
            string? remoteConfigPort = Environment.GetEnvironmentVariable("REMOTE_CONFIG__PORT");
            string? remoteConfigAPIKey = Environment.GetEnvironmentVariable("REMOTE_CONFIG__API_KEY");
#endif
#if INFLUXDB
            string? influxDBAddress = Environment.GetEnvironmentVariable("INFLUXDB_CONFIG__ADDRESS");
            string? influxDBPort = Environment.GetEnvironmentVariable("INFLUXDB_CONFIG__PORT");
            string? influxDBUsername = Environment.GetEnvironmentVariable("INFLUXDB_CONFIG__USERNAME");
            string? influxDBPassword = Environment.GetEnvironmentVariable("INFLUXDB_CONFIG__PASSWORD");
            string? influxDBToken = Environment.GetEnvironmentVariable("INFLUXDB_CONFIG__TOKEN");
            string? influxDBOrganization = Environment.GetEnvironmentVariable("INFLUXDB_CONFIG__ORGANIZATION");
            string? influxDBDatabaseName = Environment.GetEnvironmentVariable("INFLUXDB_CONFIG__DATABASE_NAME");
#endif
#if FCM_CONFIG
            string? FCMConfigServer_ServerKey = Environment.GetEnvironmentVariable("FCM_CONFIG_SERVER__SERVER_KEY");
            string? FCMConfigServer_MessagingSenderId = Environment.GetEnvironmentVariable("FCM_CONFIG_SERVER__MESSAGING_SENDER_ID");
            string? FCMConfigServer_DefaultIcon = Environment.GetEnvironmentVariable("FCM_CONFIG_SERVER__DEFAULT_ICON");
            string? FCMConfigServer_DefaultVibrate = Environment.GetEnvironmentVariable("FCM_CONFIG_SERVER__DEFAULT_VIBRATE");
            string? FCMConfigClient_ApiKey = Environment.GetEnvironmentVariable("FCM_CONFIG_CLIENT__API_KEY");
            string? FCMConfigClient_AuthDomain = Environment.GetEnvironmentVariable("FCM_CONFIG_CLIENT__AUTH_DOMAIN");
            string? FCMConfigClient_ProjectId = Environment.GetEnvironmentVariable("FCM_CONFIG_CLIENT__PROJECT_ID");
            string? FCMConfigClient_StorageBucket = Environment.GetEnvironmentVariable("FCM_CONFIG_CLIENT__STORAGE_BUCKET");
            string? FCMConfigClient_MessagingSenderId = Environment.GetEnvironmentVariable("FCM_CONFIG_CLIENT__MESSAGING_SENDER_ID");
            string? FCMConfigClient_AppId = Environment.GetEnvironmentVariable("FCM_CONFIG_CLIENT__APP_ID");

#endif

#if MASTER_DB
            try
            {
                configModel.MasterDatabaseConfig = new DBServerConfigModel((EnumDBProvider)int.Parse(masterDatabaseDatabaseType),
                                              masterDatabaseAddress,
                                              int.Parse(masterDatabasePort),
                                              masterDatabaseDatabaseName,
                                              masterDatabaseUsername,
                                              masterDatabasePassword);
            }
            catch { }
#endif
#if TRACE_DB
            try
            {
                configModel.TraceDatabaseConfig = new DBServerConfigModel((EnumDBProvider)int.Parse(traceDatabaseDatabaseType),
                                            traceDatabaseAddress,
                                            int.Parse(traceDatabasePort),
                                            traceDatabaseDatabaseName,
                                            traceDatabaseUsername,
                                            traceDatabasePassword);
            }
            catch { }
#endif
#if RABBITMQ
            try
            {
                configModel.RabbitMQConfig = new ServerConfigModel(rabbitMQAddress,
                                            int.Parse(rabbitMQPort),
                                            rabbitMQUsername,
                                            rabbitMQPassword);
            }
            catch { }
#endif
#if ELASTICSEARCH
            try
            {
                configModel.ElasticSearchConfig = new ServerConfigModel(elasticSearchAddress,
                                            int.Parse(elasticSearchPort),
                                            elasticSearchUsername,
                                            elasticSearchPassword);
            }
            catch { }
#endif
#if REDIS
            try
            {
                configModel.RedisConfig = new ServerConfigModel(redisAddress,
                                            int.Parse(redisPort),
                                            redisUsername,
                                            redisPassword);
            }
            catch { }
            try
            {
                configModel.RedisSupportConfig = new ServerConfigModel(redisSupportAddress,
                                            int.Parse(redisSupportPort),
                                            redisSupportUsername,
                                            redisSupportPassword);
            }
            catch { }
#endif
#if SOCKETIO
            try
            {
                var isInt = int.TryParse(socketIOPort, out int intPort);
                if (isInt)
                    configModel.SocketIOConfig = new ServerConfigModel(socketIOAddress,
                        intPort, "", "");
                else
                    configModel.SocketIOConfig = new ServerConfigModel(socketIOAddress,
                        0, "", "");
            }
            catch { }
#endif
#if CONNECTIVITY
            try
            {
                configModel.ConnectivityConfig = new ServerConfigModel(Connectivity_Address,
                                                                        int.Parse(Connectivity_Port),
                                                                        Connectivity_APIKey);
            }
            catch { }
#endif
#if REMOTE_CONFIG
            try
            {
                configModel.RemoteConfig = new ServerConfigModel(remoteConfigAddress,
                                            int.Parse(remoteConfigPort),
                                            "",
                                            "");
            }
            catch { }
            try
            {
                configModel.RemoteConfigAPIKey = remoteConfigAPIKey;
            }
            catch { }
#endif
#if INFLUXDB
            try
            {
                configModel.InfluxDBConfig = new InfluxDBServerConfigModel(influxDBAddress,
                                            int.Parse(influxDBPort),
                                            influxDBOrganization,
                                            influxDBDatabaseName,
                                            influxDBUsername,
                                            influxDBPassword,
                                            influxDBToken);
            }
            catch { }
#endif
#if FCM_CONFIG
            try
            {
                configModel.FCMConfig = new FCMConfigModel(new FMCServerModel(FCMConfigServer_ServerKey,
                                                                            FCMConfigServer_MessagingSenderId,
                                                                            FCMConfigServer_DefaultIcon,
                                                                            FCMConfigServer_DefaultVibrate),
                                                          new FMCClientModel(FCMConfigClient_ApiKey,
                                                                              FCMConfigClient_AuthDomain,
                                                                              FCMConfigClient_ProjectId,
                                                                              FCMConfigClient_StorageBucket,
                                                                              FCMConfigClient_MessagingSenderId,
                                                                              FCMConfigClient_AppId));
            }
            catch { }
#endif
            return configModel;
        }
        public static T GetAppSetting<T>(string key)
        {
            try
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
                string json = File.ReadAllText(filePath);
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                var sectionPath = key.Split(":")[0];

                if (key.Split(":").Length >= 2 && !string.IsNullOrEmpty(sectionPath))
                {
                    var keyPath = key.Split(":")[1];
                    return (T)jsonObj[sectionPath][keyPath];
                }
                else
                {
                    return (T)jsonObj[sectionPath];
                }
            }
            catch (Exception ex)
            {
                throw new BaseException(ex);
            }
        }
        public static void AddOrUpdateAppSetting<T>(string key, T value)
        {
            try
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
                string json = File.ReadAllText(filePath);
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                var sectionPath = key.Split(":")[0];

                if (key.Split(":").Length >= 2 && !string.IsNullOrEmpty(sectionPath))
                {
                    var keyPath = key.Split(":")[1];
                    jsonObj[sectionPath][keyPath] = value;
                }
                else
                {
                    jsonObj[sectionPath] = value; // if no sectionpath just set the value
                }

                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, output);
            }
            catch (Exception ex)
            {
                throw new BaseException(ex);
            }
        }
        public static async Task<RemoteConfigModel> GetRemoteConfig(string getVersion, bool isRefresh)
        {
            string funcName = "GetRemoteConfig";
            var backupFilePath = Path.Combine("Configs", "Backup", "Lastest.txt");
            var localFilePath = Path.Combine("Configs", "Local.json");

            try
            {
                string jsonStr = FileUtil.ReadFile(localFilePath);
                if (jsonStr == null)
                {
                    var dic = new Dictionary<string, string>();
                    var xApiKey = CommonConfig.GetConfig().RemoteConfigAPIKey;
                    var remoteConfigURL = $"{CommonConfig.GetConfig().RemoteConfig.Address}:{CommonConfig.GetConfig().RemoteConfig.Port}/api/v1/config/getconfig" + $"?v={getVersion}";
                    dic.Add("X-Api-Key", xApiKey);
                    HttpServiceResponse httpServiceResponse = await HttpService.GetAsync(remoteConfigURL, dic);

                    if (httpServiceResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        if (string.IsNullOrEmpty(httpServiceResponse.ResponseString))
                        {
                            if (isRefresh)
                            {
                                Log.Logger.Information($"{funcName} RemoteConfig not change, v={getVersion}");
                                return null;
                            }
                        }
                        ResponseObject ro = JsonConvert.DeserializeObject<ResponseObject>(httpServiceResponse.ResponseString);
                        if (ro.Data == null || string.IsNullOrEmpty(ro.Data.ToString()))
                        {
                            if (isRefresh)
                            {
                                Log.Logger.Information($"{funcName} RemoteConfig not change, v={getVersion}");
                                return null;
                            }
                            throw new Exception($"{funcName} Remote config data is null, version: " + getVersion);
                        }

                        string receivedStr = ro.Data.ToString();

                        try
                        {
                            FileUtil.WriteFile(backupFilePath, receivedStr);
                        }
                        catch (Exception ex)
                        {
                            Log.Logger.LogMsg(Messages.ErrBaseException, $"Save backup file error, {backupFilePath}");
                        }

                        jsonStr = DataCipher.DecryptASE(receivedStr);
                        var rs = JsonConvert.DeserializeObject<RemoteConfigModel>(jsonStr);

                        Log.Logger.LogMsg(Messages.ISuccess_0_1, funcName, rs.GetLogStr());
                        return rs;
                    }
                    else
                    {
                        throw new BaseException("StatusCode error");
                    }
                }
                else
                {
                    var rs = JsonConvert.DeserializeObject<RemoteConfigModel>(jsonStr);

                    Log.Logger.Information($"{funcName}, [LOADED LOCAL CONFIG FILE]");
                    return rs;
                }
            }
            catch (Exception ex)
            {
                var errMessage = Messages.ErrException.SetParameters($"{funcName} error", ex.Message);
                Log.Logger.LogMsg(errMessage);

                if (isRefresh)
                {
                    return null;
                }
                string receivedStr = FileUtil.ReadFile(backupFilePath);
                if (receivedStr != null)
                {
                    string jsonStr = DataCipher.DecryptASE(receivedStr);
                    var rs = JsonConvert.DeserializeObject<RemoteConfigModel>(jsonStr);

                    Log.Logger.LogInformation($"{funcName}, [LOADED REMOTE CONFIG LASTEST BACKUP FILE]");
                    return rs;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
