﻿using iSoft.Common.ConfigsNS;
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
using NPOI.SS.Util;
using iSoft.DBLibrary.DBConnections.Interfaces;
using Microsoft.Extensions.Hosting;
using iSoft.ConnectionCommon.MessageQueueNS;

namespace SourceBaseBE.CommonFunc.Services.HostedServices
{
    public class DBHostService : BackgroundService
    {
        public ILogger _logger;

        protected object lockObj_initDB = new object();
        protected IDbConnection conn = null;
        protected IDBConnectionCustom _connCus = null;
        protected DBServerConfigModel dbConfig = null;

        protected CommonDBContext _dbContext;

        protected object lockObj_lastErrorTime = new object();
        protected DateTime lastErrorTime = DateTime.MinValue;
        const int ConstTimeRetryConnectDB = 30;

        public EnumConnectionStatus DBConnectionStatus = EnumConnectionStatus.None;

        public DBHostService(
            DBServerConfigModel dbConfig,
            ILogger<DBHostService> logger)
        {
            _logger = logger;
            this.dbConfig = dbConfig;
        }

        public virtual int GetLoopInverval()
        {
            return 1000;
        }

        public virtual void InitRepository()
        {
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(() => Init(stoppingToken));
        }

        private void Init(CancellationToken stoppingToken)
        {
            try
            {
                lock (lockObj_initDB)
                {
                    CachedSupportFunc.SetRedisConfig(CommonConfig.GetConfig().RedisSupportConfig);
                    DBConnectionStatus = initDB(this.dbConfig);
                }

                while (!stoppingToken.IsCancellationRequested)
                {
                    DoWork();
                    Thread.Sleep(GetLoopInverval());
                }
            }
            catch (Exception ex)
            {
                _logger.LogMsg(Messages.ErrException, ex);
            }
        }

        private void DoWork()
        {
            try
            {
                string funcName = nameof(DoWork);
                DateTime startTime = DateTime.Now;

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

                DoWorkImp();

                //_logger.LogMsg(Messages.ISuccess_0_1, funcName, DateTimeUtil.GetHumanStr(DateTime.Now - startTime));
                return;
            }
            catch (Exception ex) when (ex is DbException || ex is DBException)
            {
                _logger.LogMsg(Messages.ErrDBException.SetParameters(ex));

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
            catch (Exception ex)
            {
                _logger.LogMsg(Messages.ErrException.SetParameters(ex));
                //throw new BaseException(ex);
            }

        }

        public virtual void DoWorkImp()
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