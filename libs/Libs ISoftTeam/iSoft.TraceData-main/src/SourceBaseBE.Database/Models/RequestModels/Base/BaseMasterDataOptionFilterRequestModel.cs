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
    public class BaseMasterDataOptionFilterRequestModel : BaseCRUDRequestModel<MasterDataOptionFilterEntity>
    {
        
        
        public override MasterDataOptionFilterEntity GetEntity(MasterDataOptionFilterEntity entity)
        {
            if (this.Id != null) entity.Id = (long)this.Id;
            if (this.Order != null) entity.Order = this.Order;
            
        
            return entity;
        }

        public override Dictionary<string, (string, IFormFile)> GetFiles()
        {
            Dictionary<string, (string, IFormFile)> dicRS = new Dictionary<string, (string, IFormFile)>();
            
            
            return dicRS;
        }
    }
}
