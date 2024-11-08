using iSoft.Common;
using iSoft.Common.ConfigsNS;
using iSoft.Common.Enums;
using iSoft.Redis.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace SourceBaseBE.CommonFunc.Cronjobs
{
  public class CheckConnectRedisSupportJob : IHostedService, IDisposable
  {
    private int executionCount = 0;
    private ILogger<CheckConnectRedisSupportJob> _logger;
    private Timer _timer;

    public CheckConnectRedisSupportJob(ILogger<CheckConnectRedisSupportJob> logger)
    {
      _logger = logger;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
      _logger.LogInformation("Timed Hosted Service start.");

      _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(15));

      return Task.CompletedTask;
    }

    private async void DoWork(object state)
    {
      try
      {
        var count = Interlocked.Increment(ref executionCount);

        //_logger.LogInformation("Timed Hosted Service is working. Count: " + count);

        await CheckConnectRedisSupport(null);
      }
      catch (Exception ex)
      {
        _logger.LogMsg(Messages.ErrException, ex);
      }
    }
    private async Task CheckConnectRedisSupport(object state)
    {
      var connectStatus = CachedSupportFunc.TestConnection();
      if (connectStatus == EnumConnectionStatus.Connected)
      {
        _logger.LogMsg(Messages.ISuccess_0_1, nameof(CheckConnectRedisSupport), connectStatus.ToString());
      }
      else if (connectStatus == EnumConnectionStatus.Error)
      {
        _logger.LogMsg(Messages.ErrBaseException, nameof(CheckConnectRedisSupport), connectStatus.ToString(), CommonConfig.GetConfig().RedisSupportConfig.GetLogStr());
      }
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
      _logger.LogInformation("Timed Hosted Service is stopping.");

      _timer?.Change(Timeout.Infinite, 0);

      return Task.CompletedTask;
    }

    public void Dispose()
    {
      _timer?.Dispose();
    }
  }
}