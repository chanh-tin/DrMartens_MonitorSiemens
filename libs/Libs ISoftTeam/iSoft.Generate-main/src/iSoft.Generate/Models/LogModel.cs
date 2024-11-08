using iSoft.Common.Enums;
using iSoft.Common.Utils;
using Nest;
using NPOI.SS.Formula.Functions;
using SourceBaseBE.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SourceBaseBE.MainService.Models
{
    public class LogModel
    {
        public string MessageId { get; set; }
        public long OeePointId { get; set; }
        public long LastBlockId { get; set; }
        public string LastBlockStatus { get; set; }
        public string LastBlockNewDuration { get; set; }
        public long Inserted1_BlockId { get; set; } = 0;
        public string Inserted1_BlockStatus { get; set; }
        public long Inserted2_BlockId { get; set; } = 0;
        public string Inserted2_BlockStatus { get; set; }
        public long Inserted3_BlockId { get; set; } = 0;
        public string Inserted3_BlockStatus { get; set; }
        public string Note { get; set; }
        public long LastCountIn { get; set; } = 0;
        public long CountIn { get; set; } = 0;
        public long LastCountNG { get; set; } = 0;
        public long CountNG { get; set; } = 0;

        internal void AddLogNewMachine(MachineBlockDataEntity machineBlockData)
        {
            if (machineBlockData == null)
            {
                return;
            }

            if (this.Inserted1_BlockId == 0)
            {
                this.Inserted1_BlockId = machineBlockData.Id;
                this.Inserted1_BlockStatus = machineBlockData.MachineStatus.ToString();
            }
            else if (this.Inserted2_BlockId == 0)
            {
                this.Inserted2_BlockId = machineBlockData.Id;
                this.Inserted2_BlockStatus = machineBlockData.MachineStatus.ToString();
            }
            else if (this.Inserted3_BlockId == 0)
            {
                this.Inserted3_BlockId = machineBlockData.Id;
                this.Inserted3_BlockStatus = machineBlockData.MachineStatus.ToString();
            }
        }

        public override string ToString()
        {
            return $"[Oee:{this.OeePointId.ToString().PaddingLeft(2)} " +
                $"| msg:{this.MessageId} " +
                $"| bId{this.LastBlockId.ToString().PaddingLeft(5)} " +
                $"| Sts:{this.LastBlockStatus?.PaddingLeft(10)} " +
                $"| Dur:{this.LastBlockNewDuration?.PaddingLeft(8)} " +
                $"| In:{(this.LastCountIn == this.CountIn ? this.LastCountIn : ($"{this.LastCountIn}->{this.CountIn}"))}" +
                $"| NG:{(this.LastCountNG == this.CountNG ? this.LastCountNG : ($"{this.LastCountNG}->{this.CountNG}"))}" +
                $"| {this.Note}" +
                $"| news:{this.Inserted1_BlockId}:{this.Inserted1_BlockStatus}, {this.Inserted2_BlockId}:{this.Inserted2_BlockStatus}, {this.Inserted3_BlockId}:{this.Inserted3_BlockStatus}" +
                $"]";
        }
    }
}
