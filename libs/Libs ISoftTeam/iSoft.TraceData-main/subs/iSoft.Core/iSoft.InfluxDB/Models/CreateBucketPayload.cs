using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.InfluxDB.Models
{
    public class CreateBucketRequest
    {
        [JsonProperty("orgId")]
        public string Org { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("retentionRules")]
        public RetentionRule[] RetentionRules { get; set; }
    }
    public class CreateBucketResponse
    {
        [JsonProperty("id")]
        public string BucketId { get; set; }
        [JsonProperty("orgID")]
        public string OrgId { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("retentionRules")]
        public RetentionRule[] RetentionRules { get; set; }
    }
    public class RetentionRule
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "expire";
        [JsonProperty("everySeconds")]
        public long TimeRetent { get; set; } = 86400;
        [JsonProperty("shardGroupDurationSeconds")]
        public long shardGroupDurationSeconds { get; set; } = 0;
    }
}
