using iSoft.Common.Utils;
using Microsoft.AspNetCore.Http;
using SourceBaseBE.Database.Entities;
using static iSoft.Common.ConstCommon;
using iSoft.Common.Enums;
using SourceBaseBE.Database.Enums;
using iSoft.Common;

namespace SourceBaseBE.Database.Models.RequestModels
{
    public class MachineBlockDataRequestModel : BaseMachineBlockDataRequestModel
    {
        public string? TimeInterval { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public override MachineBlockDataEntity GetEntity(MachineBlockDataEntity entity)
        {
            if (this.Id != null) entity.Id = (long)this.Id;
            if (this.Order != null) entity.Order = this.Order;
            if (this.LineId != null) entity.LineId = this.LineId;
            if (this.MachineStatus != null) entity.MachineStatus = this.MachineStatus;
            if (this.StartTime != null) entity.StartDateTime = this.StartTime;
            if (this.EndTime != null) entity.EndDateTime = this.EndTime;
            if (this.DurationInMiliSeconds != null) entity.DurationInMiliSeconds = this.DurationInMiliSeconds;
            if (this.BlockCountIn != null) entity.BlockCountIn = this.BlockCountIn;
            if (this.BlockGoodCount != null) entity.BlockGoodCount = this.BlockGoodCount;
            if (this.BlockNGCount != null) entity.BlockNGCount = this.BlockNGCount;
            if (this.LastCountIn != null) entity.LastCountIn = this.LastCountIn;
            if (this.LastGoodCount != null) entity.LastGoodCount = this.LastGoodCount;
            if (this.LastNGCount != null) entity.LastNGCount = this.LastNGCount;
            if (this.LastMessageId != null) entity.LastMessageId = this.LastMessageId;
            if (this.LastReceivedTime != null) entity.LastReceivedTime = this.LastReceivedTime;

            if (this.MachineLossId != null && this.MachineLossId > 0) entity.MachineLossId = this.MachineLossId;
            else if (this.MachineLossId != null && this.MachineLossId <0)
            {
                entity.MachineLossId = null;
            }

            if (this.MachineLossPositionId != null && this.MachineLossPositionId > 0)
            {
                entity.MachineLossPositionId = this.MachineLossPositionId;
            }
            else if (this.MachineLossPositionId != null && this.MachineLossPositionId < 0)
            {
                entity.MachineLossPositionId = null;
            }

            if (this.MachineLossDescribeId != null && this.MachineLossDescribeId > 0)
            {
                entity.MachineLossDescribeId = this.MachineLossDescribeId;
            }
            else if(this.MachineLossDescribeId != null && this.MachineLossDescribeId < 0)
            {
                entity.MachineLossDescribeId = null;
            }

            if (this.OeePointId != null) entity.OeePointId = this.OeePointId;

            return entity;
        }
    }
}
