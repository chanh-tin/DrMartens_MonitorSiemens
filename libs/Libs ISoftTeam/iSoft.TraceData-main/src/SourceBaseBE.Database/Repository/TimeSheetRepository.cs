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
using iSoft.Common.Enums;
using SourceBaseBE.Database.Models.TrackDevice;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using iSoft.Common.Models.ResponseModels;
using SourceBaseBE.Database.Helpers;
using SourceBaseBE.Database.Models.ResponseModels;
using SourceBaseBE.Database.Models.RequestModels;
using iSoft.Common.ExtensionMethods;
using iSoft.Common.Utils;
using SourceBaseBE.Database.Models;

namespace SourceBaseBE.Database.Repository
{
    public class TimeSheetRepository : BaseCRUDRepository<TimeSheetEntity>
    {
        public TimeSheetRepository(CommonDBContext dbContext)
            : base(dbContext)
        {
        }
        public override string GetName()
        {
            return nameof(TimeSheetRepository);
        }
        /// <summary>
        /// GetById (@GenCRUD)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="DBException"></exception>
        public override TimeSheetEntity? GetById(long id, bool isTracking = false)
        {
            try
            {
                TimeSheetEntity? result = null;
                //string cacheKey = $"{cacheKeyDetail}:{id}";
                //if (!isDirect && !isTracking)
                //{
                //	result = CachedFunc.GetRedisData<TimeSheetEntity>(cacheKey, null);
                //}

                //if (result == null)
                //    {
                var dataSet = _context.Set<TimeSheetEntity>();
                IQueryable<TimeSheetEntity> queryable;
                if (!isTracking)
                {
                    queryable = dataSet.AsNoTracking().AsQueryable();
                }
                else
                {
                    queryable = dataSet.AsQueryable();
                }
                result = queryable
                      /*[GEN-7]*/
                      .Where(entity => entity.DeletedFlag != true && entity.Id == id)
                                    .FirstOrDefault();
                //result.TimeSheet2s = result.TimeSheet2s.Select(x => new TimeSheet2Entity() { Id = x.Id, LossName = x.LossName }).ToList();

                //	CachedFunc.AddEntityCacheKey(GetName(), cacheKey, true);
                //	CachedFunc.SetRedisData(cacheKey, result, ConstCommon.ConstGetDetailCacheExpiredTimeInSeconds);
                //}
                return result;
            }
            catch (Exception ex)
            {
                throw new DBException(ex);
            }
        }
        /// <summary>
        /// GetById (@GenCRUD)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="DBException"></exception>
        public async Task<TimeSheetEntity>? GetByIdAsync(long id, bool isDirect = false, bool isTracking = true)
        {
            try
            {
                TimeSheetEntity? result = null;
                //string cacheKey = $"{cacheKeyDetail}:{id}";
                //if (!isDirect && !isTracking)
                //{
                //	result = CachedFunc.GetRedisData<TimeSheetEntity>(cacheKey, null);
                //}

                //if (result == null)
                //{
                result = await (isTracking ?
                _context.Set<TimeSheetEntity>()
                          .AsNoTracking()
                          .AsQueryable()
                          /*[GEN-7]*/
                          .Where(entity => entity.DeletedFlag != true && entity.Id == id)
                          .Include(x => x.WorkingDay)
                          .Include(x => x.Employee)
                          .FirstOrDefaultAsync() :
                          _context.Set<TimeSheetEntity>()
                          .AsNoTracking()
                            .Include(x => x.WorkingDay)
                          .Include(x => x.Employee)
                          .AsQueryable()
                          /*[GEN-7]*/
                          .Where(entity => entity.DeletedFlag != true && entity.Id == id)
                          .FirstOrDefaultAsync());
                //result.TimeSheet2s = result.TimeSheet2s.Select(x => new TimeSheet2Entity() { Id = x.Id, LossName = x.LossName }).ToList();

                //	CachedFunc.AddEntityCacheKey(GetName(), cacheKey, true);
                //	CachedFunc.SetRedisData(cacheKey, result, ConstCommon.ConstGetDetailCacheExpiredTimeInSeconds);
                //}
                return result;
            }
            catch (Exception ex)
            {
                throw new DBException(ex);
            }
        }
        /// <summary>
        /// GetList (@GenCRUD)
        /// </summary>
        /// <param name="pagingReq"></param>
        /// <returns></returns>
        /// <exception cref="DBException"></exception>
        public override List<TimeSheetEntity> GetList(PagingRequestModel pagingReq = null, bool isDirect = false, bool isTracking = false)
        {
            try
            {
                string cacheKey = $"{cacheKeyList}_GetList";
                if (pagingReq != null)
                {
                    cacheKey = $"{cacheKeyList}_GetList:{pagingReq.Page}|{pagingReq.PageSize}";
                }
                List<TimeSheetEntity>? result = CachedFunc.GetRedisData<List<TimeSheetEntity>>(cacheKey, null);
                if (result == null)
                {
                    result = new List<TimeSheetEntity>();
                    if (pagingReq != null)
                    {
                        result = _context.Set<TimeSheetEntity>()
                                .AsNoTracking()
                                .AsQueryable()
                                /*[GEN-11]*/
                                .Where(entity => entity.DeletedFlag != true)
                                .OrderBy(entity => entity.Order).ThenBy(entity => entity.Id)
                                .Skip(pagingReq.GetSkip()).Take(pagingReq.GetLimit())
                                .AsParallel()
                                .ToList();

                        //.Select(entity => new TimeSheetEntity
                        // {
                        //   Id = entity.Id,
                        //   TimeSheet2s = entity.TimeSheet2s.Select(x => new TimeSheet2Entity { Id = x.Id, LossName = x.LossName }).ToList()
                        // })

                        for (var i = 0; i < result.Count; i++)
                        {


                            /*[GEN-12]*/
                        }
                    }
                    else
                    {
                        result = _context.Set<TimeSheetEntity>()
                                .AsNoTracking()
                                .AsQueryable()
                                /*[GEN-13]*/
                                .Where(entity => entity.DeletedFlag != true)
                                .OrderBy(entity => entity.Order).ThenBy(entity => entity.Id)
                                .Skip(0).Take(ConstCommon.ConstSelectListMaxRecord)
                                .AsParallel()
                                .ToList();

                        for (var i = 0; i < result.Count; i++)
                        {


                            /*[GEN-14]*/
                        }
                    }

                    CachedFunc.AddEntityCacheKey(GetName(), cacheKey, true);
                    CachedFunc.SetRedisData(cacheKey, result, ConstCommon.ConstGetListCacheExpiredTimeInSeconds);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new DBException(ex);
            }
        }
        public TimeSheetEntity Upsert(TimeSheetEntity entity/*[GEN-8]*/, long? userId = null)
        {
            try
            {
                if (entity.Id <= 0)
                {
                    // Insert
                    entity = Insert(entity/*[GEN-4]*/, userId);
                }
                else
                {
                    // Update
                    entity = Update(entity/*[GEN-4]*/, userId);
                }
                ClearCachedData();
                return entity;
            }
            catch (Exception ex)
            {
                throw new DBException(ex);
            }
        }
        public TimeSheetEntity Insert(TimeSheetEntity entity/*[GEN-8]*/, long? userId = null)
        {
            EntityEntry<TimeSheetEntity> ret = null;
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

                    /*[GEN-10]*/
                    ret = _context.Set<TimeSheetEntity>().Add(entity);

                }
                var result = _context.SaveChanges();
                ClearCachedData();
                return ret?.Entity;
            }
            catch (Exception ex)
            {
                throw new DBException(ex);
            }
        }
        public TimeSheetEntity Update(TimeSheetEntity entity/*[GEN-8]*/, long? userId = null)
        {
            TimeSheetEntity ret = null;
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
                    entity.UpdatedAt = DateTime.Now;

                    /*[GEN-9]*/
                    base.Update(entity);
                }
                var result = _context.SaveChanges();
                ClearCachedData();
                return ret;
            }
            catch (Exception ex)
            {
                throw new DBException(ex);
            }
        }
        public override object GetDisplayField(TimeSheetEntity entity)
        {
            return entity.Id.ToString();
        }
        public override List<FormSelectOptionModel> GetSelectData(string entityName, EnumAttributeRelationshipType category)
        {
            List<FormSelectOptionModel> rs = GetListWithNoInclude().Select(x => new FormSelectOptionModel(x.Id, GetDisplayField(x))).ToList();
            return rs;
        }
        public async Task<TimeSheetEntity> UpsertIfNotExist(DateTime dateTime, DataArr employee, EnumFaceId inOutTypeStatus)
        {
            var existed = await _context.Set<TimeSheetEntity>()
                .Include(x => x.Employee)
                .Include(x => x.WorkingDay)
                .FirstOrDefaultAsync(x =>
                x.Employee != null &&
                 x.RecordedTime == dateTime
                && x.Employee.EmployeeMachineCode == employee.EmployeeId);
            if (existed != null)
            {
                return null;
            }
            if (existed == null)
            {
                var employeeExist = await _context.Set<EmployeeEntity>().FirstOrDefaultAsync(x => x.EmployeeMachineCode == employee.EmployeeId);
                WorkingDayEntity wod = null;
                if (employeeExist == null)
                {
                    employeeExist = new EmployeeEntity()
                    {
                        Name = employee.FullName,
                        EmployeeMachineCode = employee.EmployeeId,
                        EmployeeCode = employee.EmployeeId,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                    };
                    employeeExist = _context.Set<EmployeeEntity>().Add(employeeExist).Entity;
                    _context.SaveChanges();
                }
                wod = await _context.Set<WorkingDayEntity>()
               .Include(x => x.Employee)
               .FirstOrDefaultAsync(x =>
               x.WorkingDate.Value.Year == employee.AttDate.Year
               && x.WorkingDate.Value.Month == employee.AttDate.Month
               && x.WorkingDate.Value.Day == employee.AttDate.Day
               && x.EmployeeEntityId == employeeExist.Id
               );
                if (wod == null)
                {
                    wod = _context.Set<WorkingDayEntity>().Add(new WorkingDayEntity()
                    {
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        WorkingDate = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day),
                        WorkingDayStatus = EnumWorkingDayStatus.Attended,
                        EmployeeEntityId = employeeExist?.Id
                    }).Entity;
                }

                existed = new TimeSheetEntity()
                {
                    Employee = employeeExist,
                    Status = inOutTypeStatus,
                    RecordedTime = dateTime,
                    WorkingDay = wod,
                    WorkingDayId = wod?.Id,

                };
                existed = this.Upsert(existed);
            }
            return existed;
        }
        public List<TimeSheetEntity> GetTimeSheetsByWdId(long wdId)
        {
            try
            {
                List<TimeSheetEntity>? result = null;
                string cacheKey = $"{cacheKeyListByListIds}_WdId:{string.Join(",", wdId)}";
                CachedFunc.GetRedisData<List<TimeSheetEntity>>(cacheKey, result);
                if (result == null)
                {
                    result = new List<TimeSheetEntity>();
                    result = _context.Set<TimeSheetEntity>()
                            .AsQueryable()
                                              .Where(entity => entity.DeletedFlag != true
                                                      && entity.WorkingDayId == wdId)
                            .ToList();
                    CachedFunc.AddEntityCacheKey(GetName(), cacheKey, true);
                    CachedFunc.SetRedisData(cacheKey, result, ConstCommon.ConstGetListCacheExpiredTimeInSeconds);
                }
                return result;
            }
            catch (Exception dbEx)
            {
                throw new DBException(dbEx);
            }
        }
        public TimeSheetPagingResponse GetListTimeSheetsByWdId(long? wdId,
                                                                                                                    long? employeeId,
                                                                                                                    PagingFilterRequestModel pagingReq,
                                                                                                                    Dictionary<string, object> paramFilter,
                                                                                                                    SearchModel paramSearch,
                                                                                                                    Dictionary<string, long> paramSort
            )
        {
            if (wdId <= 0) throw new Exception($"Invalid Workingday");
            try
            {
                string cacheKey = $"{cacheKeyList}GetListTimeSheetsByWdId";
                if (pagingReq != null)
                {
                    cacheKey = $"{cacheKey}:{pagingReq.GetKeyCache()}_{wdId}_{employeeId}" +
                                            $"_{paramFilter.ToJson()}" +
                                            $"_{paramSearch.ToJson()}" +
                                            $"_{paramSort.ToJson()}";
                    cacheKey = EncodeUtil.MD5(cacheKey);

                }
                TimeSheetPagingResponse rs /*= CachedFunc.GetRedisData<TimeSheetPagingResponse>(cacheKey, null)*/ = null;
                if (rs == null || rs.ListData == null || rs.ListData.Count <= 0)
                {
                    rs = new TimeSheetPagingResponse();
                    if (pagingReq == null)
                    {
                        pagingReq = new PagingFilterRequestModel();
                    }
                    if (pagingReq.Page <= 1)
                    {
                        pagingReq.Page = 1;
                    }
                    if (pagingReq.PageSize <= 0)
                    {
                        pagingReq.PageSize = ConstCommon.ConstSelectListMaxRecord;
                    }
                    DateTime? startDate = DateTimeHelper.GetStartOfDate(pagingReq.DateFrom.GetValueOrDefault());
                    DateTime? endDate = DateTimeHelper.GetEndOfDate(pagingReq.DateTo.GetValueOrDefault());

                    var query = _context.Set<TimeSheetEntity>()
                            .AsNoTracking()
                            .AsQueryable();

                    //* where
                    query = query.Where(p => p.DeletedFlag != true)
                        .AsNoTracking()
                        .Include(x => x.WorkingDay)
                        .Include(p => p.Employee)
                        .Where(x => x.RecordedTime >= startDate && x.RecordedTime <= endDate)
                        .OrderByDescending(x => x.RecordedTime);
                    if (wdId != null)
                    {
                        query = query.Where(x => x.WorkingDayId == wdId);
                    }
                    if (employeeId != null && employeeId >= 1)
                    {
                        query = query.Where(x => x.EmployeeId == employeeId);
                    }
                    query = TimesheetListResponseModel.PrepareWhereQueryFilter(query, paramFilter);
                    query = TimesheetListResponseModel.PrepareWhereQuerySearch(query, paramSearch);
                    query = TimesheetListResponseModel.PrepareQuerySort(query, paramSort);
                    rs.ListData = query.Skip(pagingReq.GetSkip()).Take(pagingReq.GetLimit()).Select(x => new TimesheetListResponseModel().SetData(x)).ToList();
                    rs.TotalRecord = query.AsParallel().LongCount();
                    CachedFunc.AddEntityCacheKey(GetName(), cacheKey, true);
                    CachedFunc.SetRedisData(cacheKey, rs, ConstCommon.ConstGetListCacheExpiredTimeInSeconds);
                }
                return rs;
            }
            catch (Exception ex)
            {
                throw new DBException(ex);
            }
        }
        public List<TimeSheetExcel> GetListTimeSheetsForExport(List<long>? employeeIds, DateTime from, DateTime to)
        {
            if (employeeIds == null) throw new Exception($"Invalid EmployeeIds");
            try
            {
                string cacheKey = $"{cacheKeyList}GetListTimeSheetsForExport";
                DateTime? startDate = DateTimeHelper.GetStartOfDate(from);
                DateTime? endDate = DateTimeHelper.GetEndOfDate(to);

                var query = _context.Set<TimeSheetEntity>()
                        .AsNoTracking()
                        .AsQueryable();

                //* where
                query = query.Where(p => p.DeletedFlag != true)
                    .AsNoTracking()
                    .Include(p => p.Employee)
                    .ThenInclude(x => x.Department)
                    .Where(x => x.RecordedTime >= startDate && x.RecordedTime <= endDate)
                    .OrderBy(x => x.RecordedTime);
                var ret = query.Where(x => employeeIds.Contains(x.EmployeeId.GetValueOrDefault())).ToList();
                return TimeSheetExcel.SetDatas(ret.GroupBy(x => x.EmployeeId));
            }
            catch (Exception ex)
            {
                throw new DBException(ex);
            }
        }
    }
}
