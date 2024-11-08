using System.Collections.Generic;
using System;

namespace iMag.Oee.OEEFactors
{
    public class OEEPointOutputs
    {
        public long Id { get ; set; }   
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Note { get; set; }
        public string? TagNames { get; set; } 
        public Configurations? Configurations { get; set; }
        public MachineBlockDatas? MachineBlockData { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
