using System;
using System.Collections.Generic;

namespace SourceBaseBE.MainService.Models
{
    public class ChartObjDataModel
    {
        public string Name { get; set; }
        public List<ObjDataModel> ListData = new List<ObjDataModel>();
    }

    public class ObjDataModel
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public long Duration { get; set; }
    }
}
