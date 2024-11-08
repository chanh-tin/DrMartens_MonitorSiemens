using iSoft.Common.Utils;
using Microsoft.AspNetCore.Http;
using SourceBaseBE.Database.Entities;
using static iSoft.Common.ConstCommon;
using iSoft.Common.Enums;
using SourceBaseBE.Database.Enums;
using iSoft.Common;

namespace SourceBaseBE.Database.Models.RequestModels
{
    public class OeePointRequestModel : BaseOeePointRequestModel
    {
        public string? TotalCountInTag { get; set; }
        public string? TotalGoodCountTag { get; set; }
        public string? TotalNGCountTag { get; set; }
    }
}
