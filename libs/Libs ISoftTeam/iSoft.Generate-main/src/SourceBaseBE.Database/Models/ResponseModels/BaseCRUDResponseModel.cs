using iSoft.Common.Utils;
using iSoft.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PRPO.Database.Helpers;
using SourceBaseBE.Database.Entities;
using SourceBaseBE.Database.Models.RequestModels;

namespace SourceBaseBE.Database.Models.ResponseModels
{
    public class BaseCRUDResponseModel<TEntity> where TEntity : BaseCRUDEntity, new()
    {
        public long? Id { get; set; }
        public long? Order { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? DeletedFlag { get; set; }
        public string? CreatedUsername { get; set; }
        public string? UpdatedUsername { get; set; }

        public virtual object SetData(TEntity entity)
        {
            this.Id = entity.Id;
            this.Order = entity.Order;
            this.CreatedBy = entity.CreatedBy;
            this.CreatedAt = entity.CreatedAt;
            this.UpdatedBy = entity.UpdatedBy;
            this.UpdatedAt = entity.UpdatedAt;
            this.DeletedFlag = entity.DeletedFlag;
            this.CreatedUsername = entity.CreatedUsername;
            this.UpdatedUsername = entity.UpdatedUsername;
            return entity;
        }
        public virtual List<object> SetData(List<TEntity> listEntity)
        {
            List<object> listRS = new List<object>();
            foreach (TEntity entity in listEntity)
            {
                listRS.Add(new BaseCRUDResponseModel<TEntity>().SetData(entity));
            }
            return listRS;
        }
    }
}
