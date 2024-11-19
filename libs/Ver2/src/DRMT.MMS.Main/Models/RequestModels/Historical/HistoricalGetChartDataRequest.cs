using iSoft.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SourceBaseBE.MainService.Models.RequestModels.Historical
{
  public class HistoricalGetChartDataRequest
  {
    public long ConnectionId { get; set; }
    public string SearchPattern { get; set; }
    public string SearchField { get; set; }
    public EnumShiftId? ShiftId { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }

    public override string ToString()
    {
      return $"{SearchPattern}, {ConnectionId}, {SearchField}, {StartTime}, {EndTime}, {Page}, {PageSize}";
    }
  }
}
