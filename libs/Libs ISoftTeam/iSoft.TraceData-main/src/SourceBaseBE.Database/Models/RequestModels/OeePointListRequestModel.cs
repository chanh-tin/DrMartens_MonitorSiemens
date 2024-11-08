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
    public class OeePointListRequestModel : PagingFilterRequestModel
    {
        public long? OeePointId { get; set; }
        public long? Id { get; set; }
        public override string GetKeyCache()
        {
            string keyCache = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
              base.GetKeyCache(),
              this.SearchStr?.RemoveSpecialChar(),
              this.FilterStr?.RemoveSpecialChar(),
              this.SortStr?.RemoveSpecialChar(),
              this.DateFrom == null ? "" : this.DateFrom.Value.ToString(ConstDateTimeFormat.YYYYMMDDTHHMMSS_FFF),
              this.DateTo == null ? "" : this.DateTo.Value.ToString(ConstDateTimeFormat.YYYYMMDDTHHMMSS_FFF),
              this.Language,
              this.OeePointId,
              this.Id);
            string rs = EncodeUtil.MD5(keyCache);
            return rs;
        }
    }
}
