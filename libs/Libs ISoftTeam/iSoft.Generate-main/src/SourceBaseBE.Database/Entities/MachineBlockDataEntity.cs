using iSoft.Common.Enums;
using iSoft.Common.Utils;
using iSoft.Database.Entities;
using iSoft.Database.Entities.Interface;
using iSoft.Database.Extensions;
using iSoft.DBLibrary.Entities;
using MathNet.Numerics.Differentiation;
using Microsoft.AspNetCore.Mvc.Formatters;
using SourceBaseBE.Database.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.Json.Serialization;
using static iSoft.Common.ConstCommon;

namespace SourceBaseBE.Database.Entities
{
    [Table("MachineBlockDatas")]
    public class MachineBlockDataEntity : BaseCRUDEntity
    {
        public long? LineId { get; set; }
        public EnumMachineStatus? MachineStatus { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public long? DurationInMiliSeconds { get; set; } = 0;
        public long? BlockCountIn { get; set; } = 0;
        public long? BlockGoodCount { get; set; } = 0;
        public long? BlockNGCount { get; set; } = 0;
        public long? LastCountIn { get; set; } = 0;
        public long? LastGoodCount { get; set; } = 0;
        public long? LastNGCount { get; set; } = 0;
        public string? LastMessageId { get; set; }
        public DateTime? LastReceivedTime { get; set; }
        public DateTime? LastChangeCounterTime { get; set; }


        //[NotFormData]
        [ForeignKey(nameof(MachineLossEntity))]
        public long? MachineLossId { get; set; }
        public MachineLossEntity? ItemMachineLoss { get; set; }


        //[NotFormData]
        [ForeignKey(nameof(MachineLossPositionEntity))]
        public long? MachineLossPositionId { get; set; }
        public MachineLossPositionEntity? ItemMachineLossPosition { get; set; }


        //[NotFormData]
        [ForeignKey(nameof(MachineLossDescribeEntity))]
        public long? MachineLossDescribeId { get; set; }
        public MachineLossDescribeEntity? ItemMachineLossDescribe { get; set; }


        //[NotFormData]
        [ForeignKey(nameof(OeePointEntity))]
        public long? OeePointId { get; set; }
        public OeePointEntity? ItemOeePoint { get; set; }

        public MachineBlockDataEntity Clone()
        {
            return new MachineBlockDataEntity
            {
                LineId = this.LineId,
                MachineStatus = this.MachineStatus,
                StartDateTime = this.StartDateTime,
                EndDateTime = this.EndDateTime,
                DurationInMiliSeconds = this.DurationInMiliSeconds,
                BlockCountIn = this.BlockCountIn,
                BlockGoodCount = this.BlockGoodCount,
                BlockNGCount = this.BlockNGCount,
                LastCountIn = this.LastCountIn,
                LastGoodCount = this.LastGoodCount,
                LastNGCount = this.LastNGCount,
                LastMessageId = this.LastMessageId,
                LastReceivedTime = this.LastReceivedTime,
                MachineLossId = this.MachineLossId,
                ItemMachineLoss = this.ItemMachineLoss,
                MachineLossPositionId = this.MachineLossPositionId,
                ItemMachineLossPosition = this.ItemMachineLossPosition,
                MachineLossDescribeId = this.MachineLossDescribeId,
                ItemMachineLossDescribe = this.ItemMachineLossDescribe,
                OeePointId = this.OeePointId,
                ItemOeePoint = this.ItemOeePoint
            };
        }

        public override string ToString()
        {
            return $"[{this.OeePointId}: {this.MachineStatus.Value.ToString()} | {this.StartDateTime.Value.GetDateTimeStr()} -> {this.EndDateTime.Value.GetDateTimeStr()}]";
        }
    }
}
