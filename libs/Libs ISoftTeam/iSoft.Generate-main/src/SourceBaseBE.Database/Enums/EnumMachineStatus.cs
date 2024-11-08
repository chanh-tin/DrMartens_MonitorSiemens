using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceBaseBE.Database.Enums
{
	public enum EnumMachineStatus
    {
        RunGood = 1,
        RunNG = 2,
        StopUnplanned = 3,
        StopPlanned = 4,
        Unknown = 6,
        FirstRun = 7,
        ResetCount = 8,
    }
}
