using dotenv.net;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using iSoft.InfluxDB.Services;
using System;
using System.Collections.Generic;
using static System.Net.WebRequestMethods;

namespace iMag.Test
{
    public class Tests
    {
        InfluxDBService _influxDBService;
        private List<KeyValuePair<string, string>> Tags;
        private List<string> Fields;
        private string mesurement = "sensors";
        private string testBucket = "testBucket1";
        private string testOrg = "dea83dfe9a76dac7";
        [SetUp]
        public void Setup()
        {
            DotEnv.Load();
            Tags = new List<KeyValuePair<string, string>>()
            {
                  KeyValuePair.Create("sensor","temp"),
                   KeyValuePair.Create( "sensor","hud")
            };
            Fields = new List<string>()
            {
                "max",
                "avg",
                "min"
            };
            GetAndSetUpInflux();
            _influxDBService.CheckConnect();
            _influxDBService.CreateBucket(testBucket,
                testOrg,
                Environment.GetEnvironmentVariable("INFLUXDB_CONFIG__TOKEN")).Wait();
        }
        private void GetAndSetUpInflux()
        {
            var INFLUXDB_CONFIG__ADDRESS = Environment.GetEnvironmentVariable("INFLUXDB_CONFIG__ADDRESS");
            var INFLUXDB_CONFIG__PORT = Environment.GetEnvironmentVariable("INFLUXDB_CONFIG__PORT");
            var INFLUXDB_CONFIG__USERNAME = Environment.GetEnvironmentVariable("INFLUXDB_CONFIG__USERNAME");
            var INFLUXDB_CONFIG__PASSWORD = Environment.GetEnvironmentVariable("INFLUXDB_CONFIG__PASSWORD");
            var INFLUXDB_CONFIG__TOKEN = Environment.GetEnvironmentVariable("INFLUXDB_CONFIG__TOKEN");
            var INFLUXDB_CONFIG__ORGANIZATION = Environment.GetEnvironmentVariable("INFLUXDB_CONFIG__ORGANIZATION");
            var INFLUXDB_CONFIG__DATABASE_NAME = Environment.GetEnvironmentVariable("INFLUXDB_CONFIG__DATABASE_NAME");
            _influxDBService = new InfluxDBService(
                new iSoft.Common.Models.ConfigModel.Subs.InfluxDBServerConfigModel(
                    INFLUXDB_CONFIG__ADDRESS,
                    int.Parse(INFLUXDB_CONFIG__PORT),
                    INFLUXDB_CONFIG__ORGANIZATION,
                    INFLUXDB_CONFIG__DATABASE_NAME,
                    INFLUXDB_CONFIG__USERNAME,
                    INFLUXDB_CONFIG__PASSWORD,
                    INFLUXDB_CONFIG__TOKEN
                    ));
        }
        [Test]
        public async Task WriteData()
        {
            try
            {
                var organization = Environment.GetEnvironmentVariable("INFLUXDB_CONFIG__ORGANIZATION");
                for (int i = 0; i < Math.Pow(10, 10); i++)
                {
                    var FieldsValue = new Dictionary<string, object>();
                    foreach (var field in Fields)
                    {
                        FieldsValue.Add(field, new Random().Next(0, 1000));
                    }
                    _influxDBService.WriteValues(
                       mesurement,
                       this.Tags,
                       FieldsValue,
                       DateTime.Now
                   );
                }


            }
            catch (Exception ex)
            {

                Assert.Fail();
            }

        }
        [Test]
        public async Task ReadData()
        {
            try
            {
                var organization = Environment.GetEnvironmentVariable("INFLUXDB_CONFIG__ORGANIZATION");
                _influxDBService.GetData(organization,
                    testBucket,
                    DateTime.Now.AddDays(-10),
                    DateTime.Now,
                    new List<string>() { mesurement },
                   this.Tags,
                   null
                    );
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}