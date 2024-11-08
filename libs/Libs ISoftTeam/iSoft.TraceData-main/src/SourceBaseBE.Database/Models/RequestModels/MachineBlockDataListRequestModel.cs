using iSoft.Common.Utils;
using Microsoft.AspNetCore.Http;
using SourceBaseBE.Database.Entities;
using static iSoft.Common.ConstCommon;
using iSoft.Common.Enums;
using SourceBaseBE.Database.Enums;
using iSoft.Common;
using iSoft.Common.Models.RequestModels;

namespace SourceBaseBE.Database.Models.RequestModels
{
    public class MachineBlockDataListRequestModel : PagingFilterRequestModel
    {
        public long? PointId { get; set; }
        public long? LineId { get; set; }
        public long? MachineLossPositionId { get; set; }
        public bool? IsShowLossAssigned { get; set; }

        public override string GetKeyCache()
        {
            string keyCache = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}",
              base.GetKeyCache(),
              this.SearchStr?.RemoveSpecialChar(),
              this.FilterStr?.RemoveSpecialChar(),
              this.SortStr?.RemoveSpecialChar(),
              this.DateFrom == null ? "" : this.DateFrom.Value.ToString(ConstDateTimeFormat.YYYYMMDDTHHMMSS_FFF),
              this.DateTo == null ? "" : this.DateTo.Value.ToString(ConstDateTimeFormat.YYYYMMDDTHHMMSS_FFF),
              this.Language,
              this.PointId,
              this.LineId,
              this.MachineLossPositionId,
              this.IsShowLossAssigned);
            string rs = EncodeUtil.MD5(keyCache);
            return rs;
        }
    }
}
