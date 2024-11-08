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
    public class BaseOeePointConfigRequestModel : BaseCRUDRequestModel<OeePointConfigEntity>
    {
        public virtual string? TotalCountInTag { get; set; }
        public virtual string? TotalGoodCountTag { get; set; }
        public virtual string? TotalNGCountTag { get; set; }
        public virtual string? AvaiableTag { get; set; }
        public virtual string? PerformanceTag { get; set; }
        public virtual string? QualityTag { get; set; }
        public virtual string? OeeTag { get; set; }
        public virtual string? TotalDowntimeTag { get; set; }
        public virtual string? TotalRunTimeTag { get; set; }
        public virtual string? OeePointStatusTag { get; set; }
        public virtual string? CurrentDurationTag { get; set; }
        public virtual bool? SyncConfigFlag { get; set; }
        public virtual List<long>? OeePointIds { get; set; }
        public virtual List<OeePointEntity>? ListOeePoint { get; set; }
        
        public override OeePointConfigEntity GetEntity(OeePointConfigEntity entity)
        {
            if (this.Id != null) entity.Id = (long)this.Id;
            if (this.Order != null) entity.Order = this.Order;
            if (this.TotalCountInTag != null) entity.TotalCountInTag = this.TotalCountInTag;
            if (this.TotalGoodCountTag != null) entity.TotalGoodCountTag = this.TotalGoodCountTag;
            if (this.TotalNGCountTag != null) entity.TotalNGCountTag = this.TotalNGCountTag;
            if (this.AvaiableTag != null) entity.AvaiableTag = this.AvaiableTag;
            if (this.PerformanceTag != null) entity.PerformanceTag = this.PerformanceTag;
            if (this.QualityTag != null) entity.QualityTag = this.QualityTag;
            if (this.OeeTag != null) entity.OeeTag = this.OeeTag;
            if (this.TotalDowntimeTag != null) entity.TotalDowntimeTag = this.TotalDowntimeTag;
            if (this.TotalRunTimeTag != null) entity.TotalRunTimeTag = this.TotalRunTimeTag;
            if (this.OeePointStatusTag != null) entity.OeePointStatusTag = this.OeePointStatusTag;
            if (this.CurrentDurationTag != null) entity.CurrentDurationTag = this.CurrentDurationTag;
            if (this.SyncConfigFlag != null) entity.SyncConfigFlag = this.SyncConfigFlag;
            if (this.OeePointIds != null) entity.OeePointIds = this.OeePointIds;
            if (this.ListOeePoint != null) entity.ListOeePoint = this.ListOeePoint;
        
            return entity;
        }

        public override Dictionary<string, (string, IFormFile)> GetFiles()
        {
            Dictionary<string, (string, IFormFile)> dicRS = new Dictionary<string, (string, IFormFile)>();
            
            
            return dicRS;
        }
    }
}
