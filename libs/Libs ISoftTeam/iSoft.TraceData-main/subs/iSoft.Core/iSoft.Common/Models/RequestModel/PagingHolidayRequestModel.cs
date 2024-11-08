

using iSoft.Common.Utils;
using static iSoft.Common.ConstCommon;

namespace iSoft.Common.Models.RequestModels
{
	public class PagingHolidayRequestModel : PagingRequestModel
	{
		public string? SearchStr { get; set; }

		public DateTime? DateFrom { get; set; }
		public DateTime? DateTo { get; set; }

		public string? Language { get; set; }

		public string? SortStr { get; set; }

	}
  public class PagingHolidayReportModel
  {
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }

  }
}
