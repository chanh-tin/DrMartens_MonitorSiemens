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
    public class FrontendDataRepository : BaseFrontendDataRepository
    {
        public FrontendDataRepository(CommonDBContext dbContext)
            : base(dbContext)
        {
        }
        public override bool IsCacheData
        {
            get { return false; }
        }
        public FrontendDataEntity? GetByKey(string key, bool isTracking = false)
        {
            try
            {
                FrontendDataEntity? result = null;
                var dataSet = _context.Set<FrontendDataEntity>();
                IQueryable<FrontendDataEntity> queryable;

                if (!isTracking)
                {
                    queryable = dataSet.AsNoTracking().AsQueryable();
                }
                else
                {
                    queryable = dataSet.AsQueryable();
                }
                queryable = queryable
                      .Where(entity => entity.DeletedFlag != true && entity.Key == key);

                if (!isTracking)
                {
                    queryable = queryable.Select(entity => new FrontendDataEntity()
                    {
                        Id = entity.Id,
                        Order = entity.Order,
                        DeletedFlag = entity.DeletedFlag,
                        CreatedAt = entity.CreatedAt,
                        CreatedBy = entity.CreatedBy,
                        UpdatedAt = entity.UpdatedAt,
                        UpdatedBy = entity.UpdatedBy,
                        Key = entity.Key,
                        Value = entity.Value,

                    });
                    result = queryable.FirstOrDefault();
                }
                else
                {
                    var entity = queryable.FirstOrDefault();
                    if (entity != null)
                    {


                    };
                    result = entity;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new DBException(ex);
            }
        }
    }
}
