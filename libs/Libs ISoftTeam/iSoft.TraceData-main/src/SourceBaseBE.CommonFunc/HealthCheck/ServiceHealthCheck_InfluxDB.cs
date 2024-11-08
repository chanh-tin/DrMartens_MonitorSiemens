using Microsoft.Extensions.Diagnostics.HealthChecks;
using iSoft.InfluxDB.Services;

namespace SourceBaseBE.CommonFunc.HealthCheck
{
  public sealed class ServiceHealthCheck_InfluxDB : IHealthCheck
  {
    private readonly InfluxDBService _influxDBService;

    public ServiceHealthCheck_InfluxDB(InfluxDBService influxDBService)
    {
      _influxDBService = influxDBService;
    }
    public async Task<HealthCheckResult> CheckHealthAsync(
      HealthCheckContext context,
      CancellationToken cancellationToken = new())
    {
      try
      {
        _influxDBService.CheckConnect();

        //_influxDBService.Write(write =>
        //{
        //  var point = PointData.Measurement("test")
        //                        .Tag("action", "push")
        //                        .Tag("type", "push")
        //                        .Field("value", 1)
        //                        .Timestamp(DateTime.Now, WritePrecision.Ns);

        //  var organization = Environment.GetEnvironmentVariable("INFLUXDB_CONFIG__ORGANIZATION");
        //  var databaseName = Environment.GetEnvironmentVariable("INFLUXDB_CONFIG__DATABASE_NAME");
        //  write.WritePoint(point, databaseName, organization);
        //});

        // All is well!
        return HealthCheckResult.Healthy();
      }
      catch (Exception ex)
      {
        return HealthCheckResult.Unhealthy();
      }

    }
  }
}
