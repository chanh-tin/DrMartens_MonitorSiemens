//using iSoft.Common.ExtensionMethods;
//using iSoft.Common.Utils;
//using Newtonsoft.Json;
//using System.ComponentModel.DataAnnotations;

//namespace iSoft.RabbitMQ.Payload
//{
//    public class DevicePayloadMessage : BaseMessage
//    {
//        [JsonProperty("connId", NullValueHandling = NullValueHandling.Ignore)]
//        public string ConnectionId { get; set; }


//        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
//        public List<EnvData> Data { get; set; }


//        [JsonProperty("exeAt", NullValueHandling = NullValueHandling.Ignore)]
//        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss.fff}")]
//        public DateTime ExecuteAt { get; set; }


//        private Dictionary<string, EnvData> _dicData = null;
        
//        public bool ContainsKey(string key)
//        {
//            if (_dicData == null)
//            {
//                _dicData = this.Data.ToDictionary(x => x.Key.ConvertToESField(""));
//            }
//            return _dicData.ContainsKey(key.ConvertToESField(""));
//        }

//        public double? GetValueByKey(string key)
//        {
//            string keyConvert = key.ConvertToESField("");

//            if (_dicData == null)
//            {
//                _dicData = this.Data.ToDictionary(x => x.Key.ConvertToESField(""));
//            }

//            if (_dicData.ContainsKey(key.ConvertToESField("")))
//            {
//                return _dicData[keyConvert].GetValue();
//            }
//            return null;
//        }

//        public override string ToString()
//        {
//            //string arrEnvStr = string.Join(", ", Data.ConvertAll(x => x.ToString()).ToArray());
//            return $"{{MessageId: {MessageId}, ConnectionId: {ConnectionId}, ExecuteAt: {ExecuteAt.ToString("yyyy-MM-dd HH:mm:ss.fff")}, Data: [{Data.ToJson()}]}}";
//        }
//    }
//    //public class DevicePayloadMessage : BaseMessage
//    //{
//    //    [JsonProperty("connId", NullValueHandling = NullValueHandling.Ignore)]
//    //    public long ConnectionId { get; set; }

//    //    [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
//    //    public Dictionary<string, object> Data { get; set; }

//    //    [JsonProperty("exeAt", NullValueHandling = NullValueHandling.Ignore)]
//    //    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss.fff}")]
//    //    public DateTime ExecuteAt { get; set; }
//    //    public override string ToString()
//    //    {
//    //        //string arrEnvStr = string.Join(", ", Data.ConvertAll(x => x.ToString()).ToArray());
//    //        return $"{{MessageId: {MessageId}, ConnectionId: {ConnectionId}, ExecuteAt: {ExecuteAt.ToString("yyyy-MM-dd HH:mm:ss.fff")}, Data: [{Data.ToJson()}]}}";
//    //    }
//    //}
//}
