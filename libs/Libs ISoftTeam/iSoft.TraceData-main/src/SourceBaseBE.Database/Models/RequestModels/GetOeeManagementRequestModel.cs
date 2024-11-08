using iSoft.Common.Utils;
using Microsoft.AspNetCore.Http;
using SourceBaseBE.Database.Entities;
using static iSoft.Common.ConstCommon;
using iSoft.Common.Enums;
using SourceBaseBE.Database.Enums;
using iSoft.Common;

namespace SourceBaseBE.Database.Models.RequestModels
{
    public class GetOeeManagementRequestModel
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public long OeePointId { get; set; }
    }
}
