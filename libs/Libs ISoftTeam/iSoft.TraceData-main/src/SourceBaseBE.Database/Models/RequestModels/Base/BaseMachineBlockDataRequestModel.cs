// -----------------------------------------------------------------------------
// This file was automatically generated.
// Please do not edit this file manually.
//
// Generated Date: 
//
// -----------------------------------------------------------------------------

using iSoft.Common.Utils;
using Microsoft.AspNetCore.Http;
using SourceBaseBE.Database.Entities;
using static iSoft.Common.ConstCommon;
using iSoft.Common.Enums;
using SourceBaseBE.Database.Enums;
using iSoft.Common;

namespace SourceBaseBE.Database.Models.RequestModels
{
    public class BaseMachineBlockDataRequestModel : BaseCRUDRequestModel<MachineBlockDataEntity>
    {
        public virtual long? LineId { get; set; }
        public virtual EnumMachineStatus? MachineStatus { get; set; }
        public virtual DateTime? StartDateTime { get; set; }
        public virtual DateTime? EndDateTime { get; set; }
        public virtual long? DurationInMiliSeconds { get; set; }
        public virtual long? BlockCountIn { get; set; }
        public virtual long? BlockGoodCount { get; set; }
        public virtual long? BlockNGCount { get; set; }
        public virtual long? LastCountIn { get; set; }
        public virtual long? LastGoodCount { get; set; }
        public virtual long? LastNGCount { get; set; }
        public virtual string? LastMessageId { get; set; }
        public virtual DateTime? LastReceivedTime { get; set; }
        public virtual long? MachineLossId { get; set; }
        public virtual long? MachineLossPositionId { get; set; }
        public virtual long? MachineLossDescribeId { get; set; }
        public virtual long? OeePointId { get; set; }
        
        public override MachineBlockDataEntity GetEntity(MachineBlockDataEntity entity)
        {
            if (this.Id != null) entity.Id = (long)this.Id;
            if (this.Order != null) entity.Order = this.Order;
            if (this.LineId != null) entity.LineId = this.LineId;
            if (this.MachineStatus != null) entity.MachineStatus = this.MachineStatus;
            if (this.StartDateTime != null) entity.StartDateTime = this.StartDateTime;
            if (this.EndDateTime != null) entity.EndDateTime = this.EndDateTime;
            if (this.DurationInMiliSeconds != null) entity.DurationInMiliSeconds = this.DurationInMiliSeconds;
            if (this.BlockCountIn != null) entity.BlockCountIn = this.BlockCountIn;
            if (this.BlockGoodCount != null) entity.BlockGoodCount = this.BlockGoodCount;
            if (this.BlockNGCount != null) entity.BlockNGCount = this.BlockNGCount;
            if (this.LastCountIn != null) entity.LastCountIn = this.LastCountIn;
            if (this.LastGoodCount != null) entity.LastGoodCount = this.LastGoodCount;
            if (this.LastNGCount != null) entity.LastNGCount = this.LastNGCount;
            if (this.LastMessageId != null) entity.LastMessageId = this.LastMessageId;
            if (this.LastReceivedTime != null) entity.LastReceivedTime = this.LastReceivedTime;
            if (this.MachineLossId != null) entity.MachineLossId = this.MachineLossId;
            if (this.MachineLossPositionId != null) entity.MachineLossPositionId = this.MachineLossPositionId;
            if (this.MachineLossDescribeId != null) entity.MachineLossDescribeId = this.MachineLossDescribeId;
            if (this.OeePointId != null) entity.OeePointId = this.OeePointId;
        
            return entity;
        }

        public override Dictionary<string, (string, IFormFile)> GetFiles()
        {
            Dictionary<string, (string, IFormFile)> dicRS = new Dictionary<string, (string, IFormFile)>();
            
            
            return dicRS;
        }
    }
}
