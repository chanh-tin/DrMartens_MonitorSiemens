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
using iSoft.Common;
using iSoft.Database.Models.RequestModels.Base.BaseCRUD;
using SourceBaseBE.Database.Enums;
using iSoft.Common.Enums;

namespace SourceBaseBE.Database.Models.RequestModels
{
    public class BasePlcRequestModel : BaseCRUDRequestModel<PlcEntity>
    {
        public virtual string SerialCode { get; set; }
        public virtual string Name { get; set; }
        public virtual string? IpAddress { get; set; }
        public virtual int? Port { get; set; }
        public virtual List<long>? ListDataBlock { get; set; }
        
        public override PlcEntity GetEntity(PlcEntity entity)
        {
            if (this.Id != null) entity.Id = (long)this.Id;
            if (this.Order != null) entity.Order = this.Order;
            if (this.Name != null) entity.Name = this.Name;
            if (this.IpAddress != null) entity.IpAddress = this.IpAddress;
            if (this.Port != null) entity.Port = this.Port;
            if (this.ListDataBlock != null)
            {
                entity.DataBlockIds = this.ListDataBlock;
            }
        
            return entity;
        }

        public override Dictionary<string, (string, IFormFile)> GetFiles()
        {
            Dictionary<string, (string, IFormFile)> dicRS = new Dictionary<string, (string, IFormFile)>();
            
            
            return dicRS;
        }
    }
}