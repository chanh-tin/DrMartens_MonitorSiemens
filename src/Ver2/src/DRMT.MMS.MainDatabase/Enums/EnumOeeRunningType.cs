using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceBaseBE.Database.Enums
{
	public enum EnumOeeRunningType
    {
        Running = 1,
        Downtime_Planed = 2,
        Downtime_Unplaned = 3,
        Unknown = 4,
    }
}
