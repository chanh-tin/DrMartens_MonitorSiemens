using System;

namespace iMag.Oee.Models
{
    public class ProcessDurationData
    {
        public string? Name { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public long? Duration { get; set; }
    }
}
