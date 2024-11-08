//using iSoft.AspNetCore.CommonFunc.EnvConfigDataNS;
//using iSoft.Common;
//using iSoft.Common.ConfigsNS;
//using iSoft.Common.Utils;
//using iSoft.DBLibrary.DBConnections.Factory;
//using iSoft.DBLibrary.DBConnections.Interfaces;
//using iSoft.Redis.Services;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using Serilog;
//using SourceBaseBE.Database.DBContexts;
//using SourceBaseBE.Database.Entities;
//using SourceBaseBE.Database.Repository;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//namespace SourceBaseBE.CommonFunc.Cronjobs
//{
//    public class RefreshConnectivityDataJob : IHostedService, IDisposable
//    {
//        public static Dictionary<string, bool> dicHashKey2AllowRunFlag = new Dictionary<string, bool>();
//        private int executionCount = 0;
//        private ILogger<RefreshConnectivityDataJob> _logger;
//        private Timer _timer;

//        public RefreshConnectivityDataJob(ILogger<RefreshConnectivityDataJob> logger)
//        {
//            _logger = logger;
//        }

//        public Task StartAsync(CancellationToken stoppingToken)
//        {
//            _logger.LogInformation($"[{nameof(RefreshConnectivityDataJob)}] start.");

//            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));

//            return Task.CompletedTask;
//        }

//        public static void AddHashKey(string hashKey)
//        {
//            lock (dicHashKey2AllowRunFlag)
//            {
//                dicHashKey2AllowRunFlag[hashKey] = true;
//            }
//        }

//        private async void DoWork(object state)
//        {
//            try
//            {
//                IDBConnectionCustom dBConnectionCustom = DBConnectionFactory.CreateDBConnection(CommonConfig.MasterDatabaseConfig);
//                CommonDBContext dbContext = new CommonDBContext(dBConnectionCustom);
//                ConnectionTagRepository connectionTagRepository = new ConnectionTagRepository(dbContext);

//                var count = Interlocked.Increment(ref executionCount);
//                //_logger.LogInformation($"[{nameof(RefreshConnectivityDataJob)}], Count: " + count);

//                // lock (dicHashKey2AllowRunFlag)
//                // {
//                //     foreach (var item in dicHashKey2AllowRunFlag)
//                //     {
//                //         if (item.Value)
//                //         {
//                DateTime startTime = DateTime.UtcNow;
//                var listTAgs = connectionTagRepository.GetList(null, true, false);
//                var dicLocalConnectionTag = listTAgs.ToDictionary(x => x.OriginId, x => x);

//                EnvConfigData.Ins.ReloadConnectivityData();

//                var newListConnectionTag = EnvConfigData.Ins.listConnectionTag;

//                bool updateDBFlag = false;
//                for (int i = 0; i < newListConnectionTag.Count; i++)
//                {
//                    var newConnectionTag = newListConnectionTag[i];
//                    if (dicLocalConnectionTag.ContainsKey(newConnectionTag.Id))
//                    {
//                        var localConnectionTag = dicLocalConnectionTag[newConnectionTag.Id];
//                        if (localConnectionTag.EnvKey != newConnectionTag.EnvKey
//                            || localConnectionTag.Name != newConnectionTag.Name
//                            || localConnectionTag.Category != newConnectionTag.Category)
//                        {
//                            updateDBFlag = true;
//                            break;
//                        }
//                    }
//                    else
//                    {
//                        updateDBFlag = true;
//                        break;
//                    }
//                }
//                if (updateDBFlag)
//                {
//                    for (int i = 0; i < newListConnectionTag.Count; i++)
//                    {
//                        var newConnectionTag = newListConnectionTag[i];
//                        if (dicLocalConnectionTag.ContainsKey(newConnectionTag.Id))
//                        {
//                            var localConnectionTag = dicLocalConnectionTag[newConnectionTag.Id];
//                            if (localConnectionTag.EnvKey != newConnectionTag.EnvKey
//                                || localConnectionTag.Name != newConnectionTag.Name
//                                || localConnectionTag.Category != newConnectionTag.Category)
//                            {
//                                var entity = connectionTagRepository.GetByOriginId(newConnectionTag.Id);
//                                entity.Name = newConnectionTag.Name;
//                                entity.EnvKey = newConnectionTag.EnvKey;
//                                entity.Category = newConnectionTag.Category;
//                                connectionTagRepository.Upsert(entity);
//                            }
//                        }
//                        else
//                        {
//                            var entity = new ConnectionTagEntity()
//                            {
//                                OriginId = newConnectionTag.Id,
//                                Name = newConnectionTag.Name,
//                                EnvKey = newConnectionTag.EnvKey,
//                                Category = newConnectionTag.Category,
//                            };
//                            connectionTagRepository.Upsert(entity);
//                        }
//                    }
//                }
//                _logger.LogMsg(Messages.ISuccess_0_1, $"Refresh data", $"{DateTimeUtil.GetHumanStr(DateTime.UtcNow - startTime)}");
//            }
//            catch (Exception ex)
//            {
//                _logger.LogMsg(Messages.ErrException, ex);
//            }
//        }

//        public Task StopAsync(CancellationToken stoppingToken)
//        {
//            _logger.LogInformation($"[{nameof(RefreshConnectivityDataJob)}] is stopping.");

//            _timer?.Change(Timeout.Infinite, 0);

//            return Task.CompletedTask;
//        }

//        public void Dispose()
//        {
//            _timer?.Dispose();
//        }
//    }
//}