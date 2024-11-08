using iSoft.Common;
using iSoft.Common.ConfigsNS;
using iSoft.Common.Enums;
using iSoft.Common.Utils;
using iSoft.Redis.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NPOI.SS.Formula.Functions;
using Serilog;
using SourceBaseBE.CommonFunc.EnvConfigDataNS;

namespace SourceBaseBE.CommonFunc.Cronjobs
{
    public class RefreshConnectivityDataJob : IHostedService, IDisposable
    {
        public static Dictionary<string, bool> dicHashKey2AllowRunFlag = new Dictionary<string, bool>();
        private int executionCount = 0;
        private ILogger<RefreshConnectivityDataJob> _logger;
        private Timer _timer;

        public RefreshConnectivityDataJob(ILogger<RefreshConnectivityDataJob> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"[{nameof(RefreshConnectivityDataJob)}] start.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));

            return Task.CompletedTask;
        }

        public static void AddHashKey(string hashKey)
        {
            lock (dicHashKey2AllowRunFlag)
            {
                dicHashKey2AllowRunFlag[hashKey] = true;
            }
        }

        private async void DoWork(object state)
        {
            try
            {
                var count = Interlocked.Increment(ref executionCount);
                _logger.LogInformation($"[{nameof(RefreshConnectivityDataJob)}], Count: " + count);

                // lock (dicHashKey2AllowRunFlag)
                // {
                //     foreach (var item in dicHashKey2AllowRunFlag)
                //     {
                //         if (item.Value)
                //         {
                            DateTime startTime = DateTime.Now;

                            EnvConfigData.Ins.ReloadConnectivityData();

                //             CachedSupportFunc.SetRedisDataInMilisecond(item.Key, true, 24 * 3600);

                //             dicHashKey2AllowRunFlag[item.Key] = false;

                //             _logger.LogMsg(Messages.ISuccess_0_1, $"Refresh data, {item.Key}", $"{DateTimeUtil.GetHumanStr(DateTime.Now - startTime)}");
                //         }
                //     }
                //     dicHashKey2AllowRunFlag = dicHashKey2AllowRunFlag.Where(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                // }
            }
            catch (Exception ex)
            {
                _logger.LogMsg(Messages.ErrException, ex);
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"[{nameof(RefreshConnectivityDataJob)}] is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}