using iMag.Oee.Models;
using System;
using System.Collections.Generic;

namespace SourceBaseBE.MainService.Models
{
    public class ChartLossTimeDataModel
    {
        public string Name { get; set; }
        public List<ProcessDurationData> ListData = new List<ProcessDurationData>();
    }
}
