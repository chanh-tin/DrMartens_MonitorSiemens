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

namespace SourceBaseBE.Database.Models.RequestModels
{
    public class BaseExample002RequestModel : BaseCRUDRequestModel<Example002Entity>
    {
        public virtual string Name { get; set; }
        public virtual List<long>? ListExample001 { get; set; }
        
        public override Example002Entity GetEntity(Example002Entity entity)
        {
            if (this.Id != null) entity.Id = (long)this.Id;
            if (this.Order != null) entity.Order = this.Order;
            if (this.Name != null) entity.Name = this.Name;
            if (this.ListExample001 != null)
            {
                entity.Example001Ids = this.ListExample001;
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