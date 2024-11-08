using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.Common.ResponseObjectNS
{
  public class ResponseObject
  {

    [JsonProperty("Status")]
    public string? Status { get; set; } = null;

    [JsonProperty("Code")]
    public string? Code { get; set; } = null;

    [JsonProperty("Message")]
    public string? Message { get; set; } = null;

    [JsonProperty("Data")]
    public object Data { get; set; } = null;


    public ResponseObject()
    {
    }

    public ResponseObject(string status, object data)
    {
      Status = status;
      Data = data;
    }

    public ResponseObject(string status, string code, string message)
    {
      Status = status;
      Code = code;
      Message = message;
    }

    public enum ResponseStatus
    {
      Success,
      Error
    }
  }
}
