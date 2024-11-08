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

using SourceBaseBE.Database.Enums;


namespace SourceBaseBE.Database.Repository
{
    public class ShiftRepository : BaseShiftRepository
    {
        public ShiftRepository(CommonDBContext dbContext)
            : base(dbContext)
        {
        }
        public ShiftEntity? GetCurrentShift(long lineId)
        {
            try
            {
                ShiftEntity? result = null;
                string cacheKey = $"{cacheKeyDetail}_current:{lineId}";
                result = this.GetRedisData<ShiftEntity>(cacheKey, null);

                if (result == null)
                {
                    var dataSet = _context.Set<ShiftEntity>();
                    IQueryable<ShiftEntity> queryable;
                    queryable = dataSet.AsNoTracking().AsQueryable();
                    queryable = queryable
                          .Include(entity => entity.ItemLine)

                          .Where(entity => entity.DeletedFlag != true && entity.LineId == lineId);

                    var nowString = DateTime.Now.TimeOfDay.ToString(@"hh\:mm\:ss");

                    queryable = queryable.Where(entity =>
                        (entity.StartTime.CompareTo(nowString) <= 0 && entity.EndTime.CompareTo(nowString) > 0
                        )
                        ||
                        (entity.StartTime.CompareTo(entity.EndTime) > 0
                            && (entity.StartTime.CompareTo(nowString) <= 0 || entity.EndTime.CompareTo(nowString) > 0
                               )
                        )
                    );

                    queryable = queryable.Select(entity => new ShiftEntity()
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
                        ShiftType = entity.ShiftType,
                        LineId = entity.LineId,
                        ItemLine = entity.ItemLine,

                    });
                    result = queryable.FirstOrDefault();

                    if (result != null)
                    {
                        this.AddEntityCacheKey(GetName(), cacheKey, true);
                        var seconds = DateTimeUtil.GetDurationInSeconds(nowString, result.EndTime);
                        if (seconds > 0)
                        {
                            this.SetRedisData(cacheKey, result, seconds);
                        }
                    }
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
