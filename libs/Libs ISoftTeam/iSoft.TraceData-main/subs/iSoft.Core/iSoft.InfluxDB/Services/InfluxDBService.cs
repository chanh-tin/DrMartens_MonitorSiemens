using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core.Flux.Domain;
using InfluxDB.Client.Writes;
using iSoft.Common.Exceptions;
using iSoft.Common.ExtensionMethods;
using iSoft.Common.Models.ConfigModel.Subs;
using iSoft.Common.Utils;
using iSoft.InfluxDB.Models;
using Serilog;
using System.Drawing;
using System.Text;

namespace iSoft.InfluxDB.Services
{
    public class InfluxDBService
    {
        private InfluxDBServerConfigModel _config;
        public ILogger _logger = Serilog.Log.Logger;

        private static readonly Random _random = new Random();

        public InfluxDBService(InfluxDBServerConfigModel config)
        {
            _config = config;
        }
        public static string GetConnectionString(InfluxDBServerConfigModel config)
        {
            return $"{config.GetHostName()}/?org={config.Organization}&bucket={config.DatabaseName}&token={config.Token}";
        }
        //public void CreateBucket()
        //{
        //    this.CreateBucket(_config.DatabaseName, _config.Organization).Wait();
        //}

        public void Write(Action<WriteApi> action)
        {
            using var client = InfluxDBClientFactory.Create(_config.GetHostName(), _config.Token.ToCharArray());
            using var write = client.GetWriteApi();
            action(write);
        }
        public void TestWrite()
        {
            this.Write(write =>
            {
                var point = PointData.Measurement("test")
                              .Tag("action", "push")
                              .Tag("type", "push")
                              .Field("value", _random.Next(10, 20))
                              .Timestamp(DateTime.Now, WritePrecision.Ns);

                var organization = _config.Organization;
                var databaseName = _config.DatabaseName;
                write.WritePoint(point, databaseName, organization);
            });
        }
        public void WriteValues(
            string measurement,
            List<KeyValuePair<string, string>> tags,
            Dictionary<string, object> values,
            DateTime time)
        {
            this.Write(write =>
            {
                var point = PointData.Measurement(measurement);

                if (tags != null && tags.Count > 0)
                {
                    foreach (var tag in tags)
                    {
                        point = point.Tag(tag.Key, tag.Value);
                    }
                }

                foreach(var value in values)
                {
                    point = point.Field(value.Key, value.Value);
                }
                point = point.Timestamp(time, WritePrecision.Ns);

                write.WritePoint(point, _config.DatabaseName, _config.Organization);
            });
        }

        //WriteApi? write = null;
        //InfluxDBClient? client = null;

        //public void Write<T>(
        //    DateTime time,
        //    string measurement,
        //    List<KeyValuePair<string, string>> tags,
        //    Dictionary<string, T> fields
        //    )
        //{
        //    try
        //    {
        //        if (client == null)
        //        {
        //            //InfluxDBClientOptions opts = new InfluxDBClientOptions.Builder()
        //            //    .Url(_config.GetHostName())
        //            //    .AuthenticateToken(_config.Token.ToCharArray())
        //            //    //.Bucket(_config.DatabaseName)
        //            //    //.Org(_config.Organization)
        //            //    //.TimeOut(TimeSpan.FromSeconds(1))
        //            //    .Build();

        //            client = InfluxDBClientFactory.Create(_config.GetHostName(), _config.Token.ToCharArray());
        //            write = client.GetWriteApi();
        //        }

        //        var point = PointData.Measurement(measurement);
        //        if (fields != null && fields.Count > 0)
        //        {
        //            foreach (var field in fields)
        //            {
        //                point = point.Field(field.Key, field.Value)
        //                     .Timestamp(time, WritePrecision.Ns);
        //            }
        //        }
        //        if (tags != null && tags.Count > 0)
        //        {
        //            foreach (var tag in tags)
        //            {
        //                point = point.Tag(tag.Key, tag.Value);
        //                write.WritePoint(point, _config.DatabaseName, _config.Organization);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new DBException(ex);
        //    }
        //}
        /// <summary>
        ///  Generate Create Org command in Influx Cli
        ///  To use, you must install Influx Cli:
        ///  https://docs.influxdata.com/influxdb/cloud/tools/influx-cli/?t=Windows
        /// </summary>
        /// <param name="org"></param>
        /// <param name="token"></param>
        /// <param name="desc"></param>
        /// <param name="type"></param>
        /// <param name="expiredTime"></param>
        /// <param name="shardGroupDurationSeconds"></param>
        /// <returns></returns>
        public async Task CreateOrgInfluxCli(string org, string token, string desc = "")
        {
            try
            {
                var ret = $"influx org create {org} " +
                    $"\\ --description \"{desc}\" \\\r\n  " +
                    $"--token {token} \\\r\n";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<CreateBucketResponse> CreateBucket(string bucket, string org,
            string desc = "", string type = "expire", long expiredTime = 86400, long shardGroupDurationSeconds = 0)
        {
            try
            {
                using var client = InfluxDBClientFactory.Create(_config.GetHostName(), _config.Token.ToCharArray());
                var data = new CreateBucketRequest()
                {
                    Name = bucket,
                    Org = org,
                    RetentionRules = new RetentionRule[]{
                       new RetentionRule()
                    {
                        Type = type,
                        TimeRetent = expiredTime,
                        shardGroupDurationSeconds = shardGroupDurationSeconds
                    }
                    }
                };
                var url = $"{_config.GetHostName()}/api/v2/buckets";
                var ret = await HttpUtil.PostData<CreateBucketResponse>(url, data.ToJson(), Encoding.ASCII.GetBytes(_config.Token));
                return ret;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void CheckConnect()
        {
            using var client = InfluxDBClientFactory.Create(_config.GetHostName(), _config.Token.ToCharArray());

            try
            {
                var health = client.HealthAsync().Result;
                if (health.Status != HealthCheck.StatusEnum.Pass)
                {
                    throw new DBException("InfluxDB is not reachable.");
                }

                //action(write);
            }
            catch (Exception ex)
            {
                throw new DBException("InfluxDB is not reachable.", ex);
            }
        }
        public List<FluxTable> GetData(string organization,
            string databaseName,
            DateTime from,
            DateTime to,
            List<string> measurements,
            List<KeyValuePair<string, string>> tags,
            List<string> fields
            )
        {
            using var client = InfluxDBClientFactory.Create(_config.GetHostName(), _config.Token.ToCharArray());
            var query = client.GetQueryApiSync();
            var flux = $"from(bucket:\"{databaseName}\")" +
              $" |> range(start: {string.Format("{0:s}.{0:ffffff}Z", from)}, stop: {string.Format("{0:s}.{0:ffffff}Z", to)}) ";
            if (measurements != null && measurements.Count > 0)
            {
                flux += $" |> filter(fn: (r) =>";
                for (int i = 0; i < measurements.Count; i++)
                {
                    if (i == measurements.Count - 1)
                    {
                        flux += $" r[\"_measurement\"] == \"{measurements[i]}\" )";
                        break;
                    }
                    flux += $" r[\"_measurement\"] == \"{measurements[i]}\" or";
                }
            }
            if (tags != null && tags.Count > 0)
            {
                flux += $" |> filter(fn: (r) =>";
                for (int i = 0; i < tags.Count; i++)
                {
                    if (i == tags.Count - 1)
                    {
                        flux += $" r[\"{tags[i].Key}\"] == \"{tags[i].Value}\" )";
                        break;
                    }
                    flux += $" r[\"{tags[i].Key}\"] == \"{tags[i].Value}\" or";
                }
            }
            if (fields != null && fields.Count > 0)
            {
                flux += $" |> filter(fn: (r) =>";
                for (int i = 0; i < fields.Count; i++)
                {
                    if (i == fields.Count - 1)
                    {
                        flux += $" r._field == \"{fields[i]}\" )";
                        break;
                    }
                    flux += $" r._field == \"{fields[i]}\" or";
                }
            }

            var tables = query.QuerySync(flux, organization);
            var data = tables.SelectMany(table => table.Records.Select(x => new InfluxModel() { Name = x.GetField(), Value = x.GetValue(), Time = x.GetTime().Value }));
            return tables;
        }
        public List<FluxTable> GetData(string organization,
           string databaseName,
          string flux
           )
        {
            using var client = InfluxDBClientFactory.Create(_config.GetHostName(), _config.Token.ToCharArray());
            var query = client.GetQueryApiSync();
            var tables = query.QuerySync(flux, organization);
            var data = tables.SelectMany(table => table.Records.Select(x => new InfluxModel() { Name = x.GetField(), Value = x.GetValue(), Time = x.GetTime().Value }));
            return tables;
        }
    }
}
