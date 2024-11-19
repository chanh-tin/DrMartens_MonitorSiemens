using iMag.Oee.Models;
using System;
using System.Collections.Generic;

namespace SourceBaseBE.MainService.Models
{
    public class ChartDataGroupValueModel
    {
        public List<DurationDataModel> ListDataDuration = new List<DurationDataModel>();
        public DateTime TimeGroup { get; set; }
    }
    
}
