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
        Run = 1,
        Stop = 2,
        Unknown = 3,
    }
    public enum EnumGeneralMachineStatus
    {
        FirstRun = 0,
        Run = 1,
        Stop = 2,
        Disconnect = 3, // disconnected or inited,
        None,
    }
}
