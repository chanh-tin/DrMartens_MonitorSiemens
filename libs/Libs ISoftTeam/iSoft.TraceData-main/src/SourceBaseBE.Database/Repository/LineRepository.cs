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
    public class LineRepository : BaseLineRepository
    {
        public LineRepository(CommonDBContext dbContext)
            : base(dbContext)
        {
        }
        public override List<LineEntity> GetList(PagingRequestModel pagingReq = null, bool isDirect = false, bool isTracking = false)
        {
            try
            {
                List<LineEntity>? result = null;
                string cacheKey = $"{cacheKeyList}";
                if (pagingReq != null)
                {
                    cacheKey = $"{cacheKeyList}:{pagingReq.GetKeyCache()}";
                }
                if (!isDirect && !isTracking)
                {
                    result = this.GetRedisData<List<LineEntity>>(cacheKey, null);
                }

                if (result == null)
                {
                    var dataSet = _context.Set<LineEntity>();
                    IQueryable<LineEntity> queryable;
                    if (!isTracking)
                    {
                        queryable = dataSet.AsNoTracking().AsQueryable();
                    }
                    else
                    {
                        queryable = dataSet.AsQueryable();
                    }
                    queryable = queryable
                                .Include(entity => entity.ListOeePoint)
                                .Include(entity => entity.ListMachine)
                                .Include(entity => entity.ListBreakTime)
                                .Include(entity => entity.ListShift)
                                .Include(entity => entity.ItemWorkshop)

                                .Where(entity => entity.DeletedFlag != true)
                                .OrderBy(entity => entity.Order).ThenBy(entity => entity.Id)
                                ;

                    if (pagingReq != null)
                    {
                        queryable = queryable
                                .Skip(pagingReq.GetSkip()).Take(pagingReq.GetLimit());
                    }
                    else
                    {
                        queryable = queryable
                                .Skip(0).Take(ConstCommon.ConstSelectListMaxRecord);
                    }

                    if (!isTracking)
                    {
                        queryable = queryable.Select(entity => new LineEntity()
                        {
                            Id = entity.Id,
                            Order = entity.Order,
                            DeletedFlag = entity.DeletedFlag,
                            CreatedAt = entity.CreatedAt,
                            CreatedBy = entity.CreatedBy,
                            UpdatedAt = entity.UpdatedAt,
                            UpdatedBy = entity.UpdatedBy,
                            Name = entity.Name,
                            ListOeePoint = entity.ListOeePoint == null ? null : entity.ListOeePoint.Where(x => x.DeletedFlag != true).ToList(),
                            //ListMachine = entity.ListMachine == null ? null : entity.ListMachine.Where(x => x.DeletedFlag != true).ToList(),
                            //ListBreakTime = entity.ListBreakTime == null ? null : entity.ListBreakTime.Where(x => x.DeletedFlag != true).ToList(),
                            //ListShift = entity.ListShift == null ? null : entity.ListShift.Where(x => x.DeletedFlag != true).ToList(),
                            WorkshopId = entity.WorkshopId,
                            //ItemWorkshop = entity.ItemWorkshop,

                        });
                        result = queryable.ToList();
                    }
                    else
                    {
                        result = queryable.ToList();
                        foreach (var entity in result)
                        {
                            entity.ListOeePoint = entity.ListOeePoint == null ? null : entity.ListOeePoint.Where(x => x.DeletedFlag != true).ToList();
                            entity.ListMachine = entity.ListMachine == null ? null : entity.ListMachine.Where(x => x.DeletedFlag != true).ToList();
                            entity.ListBreakTime = entity.ListBreakTime == null ? null : entity.ListBreakTime.Where(x => x.DeletedFlag != true).ToList();
                            entity.ListShift = entity.ListShift == null ? null : entity.ListShift.Where(x => x.DeletedFlag != true).ToList();

                        }
                    }

                    this.AddEntityCacheKey(GetName(), cacheKey, true);
                    this.SetRedisData(cacheKey, result, ConstCommon.ConstGetListCacheExpiredTimeInSeconds);
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
