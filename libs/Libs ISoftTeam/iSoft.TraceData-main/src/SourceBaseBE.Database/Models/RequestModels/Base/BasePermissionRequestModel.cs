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
    public class BasePermissionRequestModel : BaseCRUDRequestModel<PermissionEntity>
    {
        public virtual string? Name { get; set; }
        public virtual string? Description { get; set; }
        public virtual bool? EnabledFlag { get; set; }
        public virtual List<long>? ListPermissionDetail { get; set; }
        public virtual List<long>? ListUser { get; set; }
        public virtual List<long>? ListUserGroup { get; set; }
        
        public override PermissionEntity GetEntity(PermissionEntity entity)
        {
            if (this.Id != null) entity.Id = (long)this.Id;
            if (this.Order != null) entity.Order = this.Order;
            if (this.Name != null) entity.Name = this.Name;
            if (this.Description != null) entity.Description = this.Description;
            if (this.EnabledFlag != null) entity.EnabledFlag = this.EnabledFlag;
            if (this.ListPermissionDetail != null)
            {
                entity.PermissionDetailIds = this.ListPermissionDetail.Select(x => x).ToList();
            }
            if (this.ListUser != null)
            {
                entity.UserIds = this.ListUser.Select(x => x).ToList();
            }
            if (this.ListUserGroup != null)
            {
                entity.UserGroupIds = this.ListUserGroup.Select(x => x).ToList();
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
