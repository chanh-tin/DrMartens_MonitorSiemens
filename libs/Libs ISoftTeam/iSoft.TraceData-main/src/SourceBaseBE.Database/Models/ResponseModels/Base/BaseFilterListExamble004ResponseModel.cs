// -----------------------------------------------------------------------------
// This file was automatically generated.
// Please do not edit this file manually.
//
// Generated Date: 
//
// -----------------------------------------------------------------------------

using iSoft.Common.Utils;
using SourceBaseBE.Database.Entities;
using static iSoft.Common.ConstCommon;
using iSoft.Common.Enums;
using SourceBaseBE.Database.Enums;
using Newtonsoft.Json;
using iSoft.Common.Models.ResponseModel;

namespace SourceBaseBE.Database.Models.ResponseModels
{
    public class BaseFilterListExamble004ResponseModel : PagingWithColumnsResponseModel
    {
        [JsonProperty("listData", NullValueHandling = NullValueHandling.Ignore)]
        public new List<Example004ResponseModel> ListData { get; set; }

    }
}
