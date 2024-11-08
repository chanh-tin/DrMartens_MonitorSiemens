using System;
using System.Collections.Generic;

namespace SourceBaseBE.MainService.Models
{
    public class ChartObjDataGroupValueModel
    {
        public List<ObjDataValue> ListDataDuration = new List<ObjDataValue>();
        public DateTime TimeGroup { get; set; }
    }
    public class ObjDataValue
    {
        public string Name { get; set; }
        public long Duration { get; set; }
    }
}
