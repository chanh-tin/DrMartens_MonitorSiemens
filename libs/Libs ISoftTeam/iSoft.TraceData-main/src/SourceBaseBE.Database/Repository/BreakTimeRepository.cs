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
    public class BreakTimeRepository : BaseBreakTimeRepository
    {
        public BreakTimeRepository(CommonDBContext dbContext)
            : base(dbContext)
        {
        }
        public List<BreakTimeEntity> GetByLine(long lineId, string startTimeOfShift, string endTimeOfShift, bool isDirect = false, bool isTracking = false)
        {
            try
            {
                List<BreakTimeEntity>? result = null;
                string cacheKey = $"{cacheKeyList}_GetByShiftId:{lineId}_{startTimeOfShift}_{endTimeOfShift}";
                if (!isDirect && !isTracking)
                {
                    result = this.GetRedisData<List<BreakTimeEntity>>(cacheKey, null);
                }

                if (result == null)
                {
                    var dataSet = _context.Set<BreakTimeEntity>();
                    IQueryable<BreakTimeEntity> queryable;
                    if (!isTracking)
                    {
                        queryable = dataSet.AsNoTracking().AsQueryable();
                    }
                    else
                    {
                        queryable = dataSet.AsQueryable();
                    }
                    queryable = queryable
                                .Include(entity => entity.ItemLine)

                                .Where(entity => entity.DeletedFlag != true)
                                .OrderBy(entity => entity.Order).ThenBy(entity => entity.Id)
                                ;

                    queryable = queryable.Where(entity => entity.LineId == lineId);

                    queryable = queryable.Where(entity =>
                        (startTimeOfShift.CompareTo(endTimeOfShift) <= 0 
                            && !(entity.StartTime.CompareTo(endTimeOfShift) >= 0 || entity.EndTime.CompareTo(startTimeOfShift) <= 0)
                        )
                        ||
                        (startTimeOfShift.CompareTo(endTimeOfShift) > 0
                            && (entity.StartTime.CompareTo(startTimeOfShift) >= 0 || entity.EndTime.CompareTo(startTimeOfShift) > 0
                                || entity.StartTime.CompareTo(endTimeOfShift) < 0 || entity.EndTime.CompareTo(endTimeOfShift) <= 0)
                        )
                    );

                    queryable = queryable
                            .Skip(0).Take(ConstCommon.ConstSelectListMaxRecord);


                    if (!isTracking)
                    {
                        queryable = queryable.Select(entity => new BreakTimeEntity()
                        {
                            Id = entity.Id,
                            Order = entity.Order,
                            DeletedFlag = entity.DeletedFlag,
                            CreatedAt = entity.CreatedAt,
                            CreatedBy = entity.CreatedBy,
                            UpdatedAt = entity.UpdatedAt,
                            UpdatedBy = entity.UpdatedBy,
                            StartTime = entity.StartTime,
                            EndTime = entity.EndTime,
                            Name = entity.Name,
                            Description = entity.Description,
                            Note = entity.Note,
                            BreakType = entity.BreakType,
                            LineId = entity.LineId,
                            ItemLine = entity.ItemLine,

                        });
                        result = queryable.ToList();
                    }
                    else
                    {
                        result = queryable.ToList();
                        foreach (var entity in result)
                        {


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
