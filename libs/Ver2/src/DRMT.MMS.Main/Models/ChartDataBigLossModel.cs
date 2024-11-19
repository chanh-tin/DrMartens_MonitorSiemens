using iMag.Oee.Models;
using System;
using System.Collections.Generic;

namespace SourceBaseBE.MainService.Models
{
    public class ChartDataBigLossModel
    {
        public string? Name { get; set; }
        public long? TotalDurationLoss { get; set; }
        public double? PercentageTopLoss { get; set; }

        public List<DataBigLossModel>? ListData = new List<DataBigLossModel>();
    }
}
