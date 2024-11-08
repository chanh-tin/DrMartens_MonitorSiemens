using iSoft.Common.Enums;
using System;

namespace SourceBaseBE.MainService.Models.RequestModels.Historical
{
	public class HistoricalGetChartDataGroupRequest
	{
		public string ConnectionId { get; set; }
		public string SearchPattern { get; set; }
		public virtual string SearchField { get; set; }
		public EnumShiftId? ShiftId { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public virtual string TimeInterval { get; set; }
		public EnumSearchGroupType SearchGroupType { get; set; }

		public override string ToString()
		{
			return $"{SearchPattern}, {ConnectionId}, {SearchField}, {StartTime}, {EndTime}, {TimeInterval}, {SearchGroupType}";
		}
	}
	public class HistoricalGetChartDataGroupRequest2 : HistoricalGetChartDataGroupRequest
	{
		public override string SearchField { get => base.SearchField.ToLower(); set => base.SearchField = value; }
	}
}
