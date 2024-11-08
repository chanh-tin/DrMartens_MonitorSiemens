using iSoft.Common.Models.ResponseModels;
using Newtonsoft.Json;

namespace iSoft.Common.Models.ResponseModel
{

    public class PagingWithColumnsResponseModel : PagingResponseModel
    {
        [JsonProperty("Columns", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<ColumnResponseModel> Columns { get; set; }
    }
}
