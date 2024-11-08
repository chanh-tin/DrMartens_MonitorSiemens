using System;
using System.Collections.Generic;

namespace SourceBaseBE.MainService.Models
{
    public class TimeBlockModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long ValueDuration { get; set; }
        public string CausePlannedStopTime { get; set; }
        public double PercentageChange { get; set; }
    }

    public class TimeBlockOperationMachineModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

}
