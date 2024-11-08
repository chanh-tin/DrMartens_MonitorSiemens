using iSoft.Common.ConfigsNS;
using iSoft.Common.Exceptions;
using iSoft.Common.ExtensionMethods;
using iSoft.Common.Utils;
using iSoft.Common;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Threading;
using SourceBaseBE.CommonFunc.DataService;
using SourceBaseBE.CommonFunc.EnvConfigDataNS;
using iSoft.Common.Models.ConfigModel.Subs;
using iSoft.Redis.Services;
using iSoft.Common.Enums;
using iSoft.RabbitMQ;
using iSoft.RabbitMQ.Services;
using SourceBaseBE.Database.Repository;
using SourceBaseBE.Database.DBContexts;
using SourceBaseBE.Database.Entities;
using SourceBaseBE.Database.Enums;
using iSoft.Common.Lock;
using System.Data.Common;
using iSoft.DBLibrary.DBConnections.Factory;
using Npgsql;
using System.Data;
using iSoft.RabbitMQ.EnumNS;

using iSoft.DBLibrary.DBConnections.Interfaces;
using iSoft.Common.Payloads;
using iSoft.InfluxDB.Services;

namespace SourceBaseBE.CommonFunc.Services.HostedServices
{
    public class DBConsumerService : BaseConsumerService
    {
        protected object lockObj_initDB = new object();
        protected IDbConnection conn = null;
        protected IDBConnectionCustom _connCus = null;
        protected DBServerConfigModel dbConfig = null;

        protected CommonDBContext _dbContext;

        protected object lockObj_lastErrorTime = new object();
        protected DateTime lastErrorTime = DateTime.MinValue;
        const int ConstTimeRetryConnectDB = 30;

        public EnumConnectionStatus DBConnectionStatus = EnumConnectionStatus.None;

        public DBConsumerService()
        {

        }
        public DBConsumerService(
            ILogger<DBConsumerService> logger,
            RabbitMQService rabbitMQService,
            string queueName)
            :base(logger, rabbitMQService, queueName)
        {
        }
        public DBConsumerService(
            DBServerConfigModel dbConfig,
            ILogger<DBConsumerService> logger,
            RabbitMQService rabbitMQService,
            string queueName)
            : base(logger, rabbitMQService, queueName)
        {
            this.dbConfig = dbConfig;
            lock (lockObj_initDB)
            {
                CachedSupportFunc.SetRedisConfig(CommonConfig.GetConfig().RedisSupportConfig);

                DBConnectionStatus = initDB(this.dbConfig);
            }
        }

        public virtual void InitRepository()
        {
        }

        public virtual EnumConnectionStatus initDB(DBServerConfigModel dbConfig)
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
            catch (Exception ex)
            {
                _logger.LogMsg(Messages.ErrException, ex);
                return EnumConnectionStatus.Error;
            }
        }

        public override async Task handleMessage(DeliveryObj deliveryMessage)
        {
            string errMessage = "";
            string dataJson = "";
            string funcName = "handleMessage";
            DevicePayloadMessage message = null;
            try
            {
                DateTime startTime = DateTime.Now;
                message = deliveryMessage.GetData<DevicePayloadMessage>(ref dataJson, ref errMessage);

                if (message == null)
                {
                    _logger.LogMsg(Messages.ErrInputInvalid_0_1, funcName, $"err: message = null, {errMessage}, json: {dataJson}");
                    this.RemoveErrorMessage(deliveryMessage);
                    return;
                }

                lock (lockObj_lastErrorTime)
                {
                    if (DateTimeUtil.CompareDateTime(lastErrorTime, DateTime.Now) < ConstTimeRetryConnectDB)
                    {
                        throw new RetryException($"DBException just now, dbConfig: {dbConfig.GetLogStr()}, lastErrorTime = {DateTimeUtil.GetDateTimeStr(lastErrorTime)}");
                    }

                    if (DBConnectionStatus != EnumConnectionStatus.Connected)
                    {
                        throw new DBException($"DBConnectionStatus = {DBConnectionStatus}");
                    }
                }

                // TODO: Handle message here
                EnumHandleMessageStatus handleStatus = this.handleMessageImp(message);
                switch (handleStatus)
                {
                    case EnumHandleMessageStatus.DeleteMessageStatus:
                        this.RemoveErrorMessage(deliveryMessage);
                        break;
                    case EnumHandleMessageStatus.RetryMessageStatus:
                        this.RetryMessage(deliveryMessage);
                        break;
                    case EnumHandleMessageStatus.SuccessStatus:
                        this.RemoveSuccessMessage(deliveryMessage);
                        break;
                }

                //_logger.LogMsg(Messages.ISuccess_0_1, funcName, message.MessageId + ", " + DateTimeUtil.GetHumanStr(DateTime.Now - startTime));
                return;
            }
            catch (JsonReaderException ex)
            {
                _logger.LogMsg(Messages.ErrBaseException.SetParameters($"json: {dataJson}", ex));
                this.RemoveErrorMessage(deliveryMessage);
            }
            catch (RetryException ex)
            {
                _logger.LogWarning($"RetryException, {message.MessageId}, " + ex.Message);
                this.RetryMessage(deliveryMessage);
            }
            catch (Exception ex) when (ex is DbException || ex is DBException)
            {
                _logger.LogMsg(Messages.ErrDBException.SetParameters($"json: {dataJson}", ex));
                this.RetryMessage(deliveryMessage);

                //if (DBConnectionStatus != EnumConnectionStatus.Connected 
                //    || ex.Message.Contains("Failed to connect to")) // TODO: postgres
                {
                    lock (lockObj_lastErrorTime)
                    {
                        lastErrorTime = DateTime.Now;
                    }

                    try
                    {
                        if (this.conn != null && this.conn.State == ConnectionState.Open)
                        {
                            this._logger.LogWarning("Try close connection");
                            this.conn.Close();
                            this.conn.Dispose();
                        }
                    }
                    catch (Exception ex2) { }

                    lock (lockObj_initDB)
                    {
                        DBConnectionStatus = initDB(this.dbConfig);
                        if (DBConnectionStatus == EnumConnectionStatus.Connected)
                        {
                            lastErrorTime = DateTime.MinValue;
                        }
                    }
                }

                //throw new BaseException(ex);
            }
            catch (CriticalException ex)
            {
                _logger.LogMsg(Messages.ErrDBException.SetParameters($"json: {dataJson}", ex));
                this.RetryMessage(deliveryMessage);
                //throw new BaseException(ex);
            }
            catch (BaseException ex)
            {
                _logger.LogMsg(Messages.ErrBaseException.SetParameters($"json: {dataJson}", ex));
                this.RetryMessage(deliveryMessage);
                //throw new BaseException(ex);
            }
            catch (Exception ex)
            {
                _logger.LogMsg(Messages.ErrException.SetParameters($"json: {dataJson}", ex));
                this.RetryMessage(deliveryMessage);
                //throw new BaseException(ex);
            }

        }

        public virtual EnumHandleMessageStatus handleMessageImp(DevicePayloadMessage message)
        {
            return EnumHandleMessageStatus.SuccessStatus;
        }

        public override void Dispose()
        {
            try
            {
                if (this.conn != null)
                {
                    if (this.conn.State == ConnectionState.Open)
                    {
                        this.conn.Close();
                    }
                    this.conn.Dispose();
                }
            }
            catch (Exception ex)
            {
                _logger.LogMsg(Messages.ErrException, ex);
            }
            base.Dispose();
        }
    }
}
