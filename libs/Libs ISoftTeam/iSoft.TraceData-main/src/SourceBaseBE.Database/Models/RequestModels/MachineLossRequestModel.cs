using iSoft.Common.Utils;
using Microsoft.AspNetCore.Http;
using SourceBaseBE.Database.Entities;
using static iSoft.Common.ConstCommon;
using iSoft.Common.Enums;
using SourceBaseBE.Database.Enums;
using iSoft.Common;

namespace SourceBaseBE.Database.Models.RequestModels
{
    public class MachineLossRequestModel : BaseMachineLossRequestModel
    {
        public long? OeePointId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public string GetKeyCache()
        {
            string keyCache = string.Format("{0}|{1}|{2}", 
                this.OeePointId,
                this.StartTime == null ? "" : this.StartTime.Value.ToString(ConstDateTimeFormat.YYYYMMDDTHHMMSS_FFF),
                this.EndTime == null ? "" : this.EndTime.Value.ToString(ConstDateTimeFormat.YYYYMMDDTHHMMSS_FFF)
                );
              
            string rs = EncodeUtil.MD5(keyCache);
            return rs;
        }
    }
}
