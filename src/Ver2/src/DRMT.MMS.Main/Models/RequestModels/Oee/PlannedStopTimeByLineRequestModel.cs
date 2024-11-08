using iSoft.Common.Models.RequestModels;

namespace iMag.Oee.Models.RequestModels.Oee
{
    public class PlannedStopTimeByLineRequestModel: PagingRequestModel
    {
        public long LineId { get; set; }
        public override string GetKeyCache()
        {
            return $"{this.Page}|{this.PageSize}|{this.LineId}";
        }
    }
}
