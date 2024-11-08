using iSoft.Common;
using iSoft.Common.Exceptions;
using iSoft.Common.Models.RequestModels;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using iSoft.Redis.Services;
using SourceBaseBE.Database.DBContexts;
using SourceBaseBE.Database.Entities;
using iSoft.Database.Models;
using UserEntity = SourceBaseBE.Database.Entities.UserEntity;
using ISoftProjectEntity = SourceBaseBE.Database.Entities.ISoftProjectEntity;
using iSoft.Common.Models.ConfigModel.Subs;
using iSoft.Common.Utils;


namespace SourceBaseBE.Database.Repository
{
    public class OeePointConfigRepository : BaseOeePointConfigRepository
    {
        public OeePointConfigRepository(CommonDBContext dbContext)
            : base(dbContext)
        {
        }
        public OeePointConfigEntity Upsert(OeePointConfigEntity entity, long? userId = null)
        {
            try
            {
                if (entity.Id <= 0)
                {
                    // Insert
                    entity = Insert(entity, userId);
                }
                else
                {
                    // Update
                    entity = Update(entity, userId);
                }
                return entity;
            }
            catch (Exception ex)
            {
                throw new DBException(ex);
            }
        }
        public OeePointConfigEntity Insert(OeePointConfigEntity entity, long? userId = null)
        {
            try
            {
                if (entity.Id > 0)
                {
                    throw new DBException($"Insert, Unexpected Id in entity, Id={entity.Id}");
                }
                else
                {
                    if (userId != null)
                    {
                        entity.CreatedBy = userId;
                    }
                    entity.CreatedAt = DateTime.Now;
                    entity.UpdatedBy = entity.CreatedBy;
                    entity.UpdatedAt = entity.CreatedAt;
                    entity.DeletedFlag = false;
                    entity = _context.Set<OeePointConfigEntity>().Add(entity).Entity;
                }
                var result = _context.SaveChanges();
                ClearCachedData();
                return entity;
            }
            catch (Exception ex)
            {
                throw new DBException(ex);
            }
        }
        public OeePointConfigEntity Update(OeePointConfigEntity entity, long? userId = null)
        {
            try
            {
                if (entity.Id <= 0)
                {
                    throw new DBException("Update, Id not found in entity");
                }
                else
                {
                    if (userId != null)
                    {
                        entity.UpdatedBy = userId;
                    }
                    //_context.Entry(entity).State = EntityState.Detached;
                    entity.UpdatedAt = DateTime.Now;
                    //this._context.ChangeTracker.Clear();
                    //if (isTracked)
                    //entity = _context.Set<TEntity>().Update(entity).Entity;
                    _context.Set<OeePointConfigEntity>().Update(entity);
                }
                var result = _context.SaveChanges();
                ClearCachedData();
                return entity;
            }
            catch (Exception ex)
            {
                throw new DBException(ex);
            }
        }
    }
}
