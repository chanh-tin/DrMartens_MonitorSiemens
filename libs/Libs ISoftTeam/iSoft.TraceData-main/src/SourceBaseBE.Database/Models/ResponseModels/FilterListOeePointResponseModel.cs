using iSoft.Common.Utils;
using SourceBaseBE.Database.Entities;
using static iSoft.Common.ConstCommon;
using iSoft.Common.Enums;
using SourceBaseBE.Database.Enums;
using Newtonsoft.Json;

namespace SourceBaseBE.Database.Models.ResponseModels
{
    public class FilterListOeePointResponseModel : BaseFilterListOeePointResponseModel
    {
        [JsonProperty("totalRecord", NullValueHandling = NullValueHandling.Ignore)]
        public long? TotalRecord { get; set; }
    }
}
