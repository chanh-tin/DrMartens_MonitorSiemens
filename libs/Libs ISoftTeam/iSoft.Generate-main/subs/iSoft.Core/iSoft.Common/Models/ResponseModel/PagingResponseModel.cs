using Newtonsoft.Json;

namespace iSoft.Common.Models.ResponseModels
{

  public class PagingResponseModel
  {

    [JsonProperty("TotalRecord", NullValueHandling = NullValueHandling.Ignore)]
    public virtual long? TotalRecord { get; set; }

    [JsonProperty("ListData", NullValueHandling = NullValueHandling.Ignore)]
    public virtual List<object> ListData { get; set; }

  }
}
