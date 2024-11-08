//using iSoft.Common.Exceptions;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace iSoft.ConnectionCommon.Model
//{
//    public class EnvData
//    {
//        [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
//        public string Key { get; set; }


//        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
//        public string Value { get; set; }


//        [JsonProperty("updateAt", NullValueHandling = NullValueHandling.Ignore)]
//        public long UpdateAt { get; set; }


//        public EnvData(string key, string value, long updateAt)
//        {
//            Key = key;
//            Value = value;
//            UpdateAt = updateAt;
//        }

//        public EnvData(string key, double value)
//        {
//            Key = key;
//            Value = value.ToString();
//            UpdateAt = 0;
//        }
//        public EnvData()
//        {

//        }

//        public double GetValue()
//        {
//            bool convertResult = double.TryParse(this.Value, out double doubleVal);
//            if (convertResult)
//            {
//                return doubleVal;
//            };
//            throw new BaseException($"Convert string to double error, input: {this.Value}");
//        }
//        public override string ToString()
//        {
//            return $"{{{Key}: {Value}}}";
//        }
//    }
//}
