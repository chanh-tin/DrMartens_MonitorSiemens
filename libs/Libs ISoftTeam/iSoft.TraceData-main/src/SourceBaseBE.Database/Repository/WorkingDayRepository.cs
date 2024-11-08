using iSoft.Common;
using iSoft.Common.Exceptions;
using iSoft.Common.Models.RequestModels;
using Microsoft.EntityFrameworkCore;
using iSoft.Redis.Services;
using SourceBaseBE.Database.DBContexts;
using SourceBaseBE.Database.Entities;
using iSoft.Common.Enums;
using SourceBaseBE.Database.Helpers;
using SourceBaseBE.Database.Models.ResponseModels;
using iSoft.Common.Models.ResponseModels;
using SourceBaseBE.Database.Models.RequestModels;
using SourceBaseBE.Database.Models.RequestModels.Report;
using iSoft.Common.Utils;
using SourceBaseBE.Database.Models.SpecialModels;


using iSoft.Common.ExtensionMethods;
using System;
using iSoft.Database.Entities;

namespace SourceBaseBE.Database.Repository
{
    public class WorkingDayRepository : BaseCRUDRepository<WorkingDayEntity>
    {
        public WorkingDayRepository(CommonDBContext dbContext)
          : base(dbContext)
        {
        }
        public override string GetName()
        {
            return nameof(WorkingDayRepository);
        }
        /// <summary>
        /// GetById (@GenCRUD)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="DBException"></exception>
        public override WorkingDayEntity? GetById(long id, bool isTracking = false)
        {
            try
            {
                WorkingDayEntity? result = null;
                //string cacheKey = $"{cacheKeyDetail}:{id}";
                //if (!isDirect && !isTracking)
                //{
                //	result = CachedFunc.GetRedisData<WorkingDayEntity>(cacheKey, null);
                //}

                //if (result == null)
                //    {
                var dataSet = _context.Set<WorkingDayEntity>();
                IQueryable<WorkingDayEntity> queryable;
                if (!isTracking)
                {
                    queryable = dataSet.AsNoTracking().AsQueryable();
                }
                else
                {
                    queryable = dataSet.AsQueryable();
                }
                result = queryable
                  .Include(x => x.Employee)
                            .AsQueryable()
                            /*[GEN-7]*/
                            .Where(entity => entity.DeletedFlag != true && entity.Id == id)
                            .FirstOrDefault();
                //result.WorkingDay2s = result.WorkingDay2s.Select(x => new WorkingDay2Entity() { Id = x.Id, LossName = x.LossName }).ToList();

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
        public async Task<WorkingDayEntity>? GetByIdAsync(long id, bool isDirect = false, bool isTracking = true)
        {
            try
            {
                WorkingDayEntity? result = null;
                //string cacheKey = $"{cacheKeyDetail}:{id}";
                //if (!isDirect && !isTracking)
                //{
                //	result = CachedFunc.GetRedisData<WorkingDayEntity>(cacheKey, null);
                //}

                //if (result == null)
                //{
                result = await (isTracking ?
                _context.Set<WorkingDayEntity>()
                  //.AsNoTracking()
                  .AsQueryable()
                  /*[GEN-7]*/
                  .Where(entity => entity.DeletedFlag != true && entity.Id == id)
                  .FirstOrDefaultAsync() :
                  _context.Set<WorkingDayEntity>()
                  .AsNoTracking()
                  .AsQueryable()
                  /*[GEN-7]*/
                  .Where(entity => entity.DeletedFlag != true && entity.Id == id)
                  .FirstOrDefaultAsync());
                //result.WorkingDay2s = result.WorkingDay2s.Select(x => new WorkingDay2Entity() { Id = x.Id, LossName = x.LossName }).ToList();

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
        public override List<WorkingDayEntity> GetList(PagingRequestModel pagingReq = null, bool isDirect = false, bool isTracking = false)
        {
            try
            {
                string cacheKey = $"{cacheKeyList}";
                if (pagingReq != null)
                {
                    cacheKey = $"{cacheKeyList}:{pagingReq.Page}|{pagingReq.PageSize}";
                }
                List<WorkingDayEntity>? result = CachedFunc.GetRedisData<List<WorkingDayEntity>>(cacheKey, null);
                if (result == null)
                {
                    result = new List<WorkingDayEntity>();
                    if (pagingReq != null)
                    {
                        result = _context.Set<WorkingDayEntity>()
                          .AsNoTracking()
                          .AsQueryable()
                          /*[GEN-11]*/
                          .Where(entity => entity.DeletedFlag != true)
                          .OrderBy(entity => entity.Order).ThenBy(entity => entity.Id)
                          .Skip(pagingReq.GetSkip()).Take(pagingReq.GetLimit())
                          .AsParallel()
                          .ToList();

                        //.Select(entity => new WorkingDayEntity
                        // {
                        //   Id = entity.Id,
                        //   WorkingDay2s = entity.WorkingDay2s.Select(x => new WorkingDay2Entity { Id = x.Id, LossName = x.LossName }).ToList()
                        // })

                        for (var i = 0; i < result.Count; i++)
                        {


                            /*[GEN-12]*/
                        }
                    }
                    else
                    {
                        result = _context.Set<WorkingDayEntity>()
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
        public WorkingDayEntity Upsert(WorkingDayEntity entity/*[GEN-8]*/, long? userId = null)
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
                return entity;
            }
            catch (Exception ex)
            {
                throw new DBException(ex);
            }
        }
        public WorkingDayEntity Insert(WorkingDayEntity entity/*[GEN-8]*/, long? userId = null)
        {
            try
            {
                if (entity.Id > 0)
                {
                    throw new DBException($"Insert, Unexpected Id in entity, Id={entity.Id}");
                }
                else
                {
                    var existed = this.CheckIfExist(entity).Result;
                    if (userId != null)
                    {
                        entity.CreatedBy = userId;
                    }
                    if (existed == null)
                    {
                        entity.CreatedAt = DateTime.Now;
                        entity.UpdatedBy = entity.CreatedBy;
                        entity.UpdatedAt = entity.CreatedAt;
                        entity.DeletedFlag = false;
                    }
                    else
                    {
                        entity.Id = existed.Id;
                        return this.Update(entity, userId);
                    }
                    /*[GEN-10]*/
                    _context.Set<WorkingDayEntity>().Add(entity);
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
        public WorkingDayEntity Update(WorkingDayEntity entity/*[GEN-8]*/, long? userId = null)
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
                    entity.UpdatedAt = DateTime.Now;
                    //_context.Entry<WorkingDayEntity>(entity).State = EntityState.Detached;
                    /*[GEN-9]*/
                    base.Update(entity);
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
        public override object GetDisplayField(WorkingDayEntity entity)
        {
            return entity.Notes.ToString();
        }
        // solution2
        public WorkingDayPagingResponseModel GetListWorkingDayv2(
         PagingFilterRequestModel pagingReq,
         Dictionary<string, object> paramFilter,
        SearchModel paramSearch,
         Dictionary<string, long> paramSort)
        {
            try
            {
                string cacheKey = $"{cacheKeyList}_Attendance_GetListWorkingDayv2";
                if (pagingReq != null)
                {
                    cacheKey = $"{cacheKey}:{pagingReq.GetKeyCache()}";
                }
                cacheKey += $"{paramFilter.ToJson()}";
                cacheKey += $"{paramSearch.ToJson()}";
                cacheKey += $"{paramSort.ToJson()}";
                cacheKey = EncodeUtil.MD5(cacheKey);

                WorkingDayPagingResponseModel rs = CachedFunc.GetRedisData<WorkingDayPagingResponseModel>(cacheKey, null);
                if (rs == null)
                {
                    rs = new WorkingDayPagingResponseModel();
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
                    DateTime? startDate = DateTimeHelper.GetStartOfDate(pagingReq.DateFrom.GetValueOrDefault()).AddDays(-1);
                    DateTime? endDate = DateTimeHelper.GetEndOfDate(pagingReq.DateTo.GetValueOrDefault());

                    var query = _context.Set<WorkingDayEntity>()
                      .AsNoTracking()
                      .AsQueryable();
                    //* where
                    query = query.AsNoTracking()
                         .Include(p => p.Employee)
                         .ThenInclude(jt => jt.Department)
                         .Include(p => p.Employee)
                         .ThenInclude(d => d.JobTitle)
                         .OrderBy(x => x.UpdatedAt)
                         .AsQueryable();
                    query = query.Where(entity => entity.DeletedFlag != true && entity.Employee.DeletedFlag != true);
                    query = query.Where(s => s.WorkingDate.Value >= startDate && s.WorkingDate.Value <= endDate);
                    var totalByDate = query;
                    query = DashboardResponseModel.PrepareWhereQueryFilter(query, paramFilter);
                    query = DashboardResponseModel.PrepareWhereQuerySearch(query, paramSearch);
                    query = DashboardResponseModel.PrepareQuerySort(query, paramSort);

                    var skip = pagingReq.GetSkip();
                    rs.ListData = query.Skip(pagingReq.GetSkip()).Take(pagingReq.GetLimit()).ToList().Select(x => new DashboardResponseModel().SetData(x)).ToList();
                    rs.TotalRecord = query.LongCount();
                    var countModels = new List<CountResponseModel>
      {
           new CountResponseModel { Key = "inside", Number  = totalByDate.LongCount(x=>x.InOutState== EnumInOutTypeStatus.Inside) },
           new CountResponseModel { Key = "outside", Number =totalByDate.LongCount(x=>x.InOutState== EnumInOutTypeStatus.Outside) },
           new CountResponseModel { Key = "unknown", Number = totalByDate .LongCount(x=>x.InOutState== EnumInOutTypeStatus.Unknown) },

      };
                    //rs.Counts = countModels;
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

        public DetailAttendancePagingResponseModel GetListWorkingDayByEmployeeId2(
                                                      EmployeeAttendanceDetailRequest pagingReq,
                                                      Dictionary<string, object> paramFilter,
                                                      SearchModel paramSearch,
                                                      Dictionary<string, long> paramSort)
        {
            try
            {
                string cacheKey = $"{cacheKeyList}_GetListWorkingDayByEmployeeId2";
                if (pagingReq != null)
                {
                    cacheKey = $"{cacheKey}:{pagingReq.GetKeyCache()}" +
                                $"_{EncodeUtil.MD5(paramFilter.ToJson())}" +
                                $"_{EncodeUtil.MD5(paramSearch.ToJson())}" +
                                $"_{EncodeUtil.MD5(paramSort.ToJson())}";
                    cacheKey = EncodeUtil.MD5(cacheKey);
                }
                DetailAttendancePagingResponseModel rs = CachedFunc.GetRedisData<DetailAttendancePagingResponseModel>(cacheKey, null);
                if (rs == null || rs.rawDatas == null)
                {
                    rs = new DetailAttendancePagingResponseModel();
                    if (pagingReq == null)
                    {
                        pagingReq = new EmployeeAttendanceDetailRequest();
                        pagingReq.Page = 0;
                        pagingReq.PageSize = ConstCommon.ConstSelectListMaxRecord;
                    }
                    if (pagingReq.PageSize <= 0)
                    {
                        pagingReq.PageSize = ConstCommon.ConstSelectListMaxRecord;
                    }
                    DateTime? startDate = DateTimeHelper.GetStartOfDate(pagingReq.DateFrom.GetValueOrDefault());
                    DateTime? endDate = DateTimeHelper.GetEndOfDate(pagingReq.DateTo.GetValueOrDefault());

                    var queryCurrentWd = _context.Set<WorkingDayEntity>()
                      .AsNoTracking()
                      .AsQueryable();
                    //* where
                    queryCurrentWd = queryCurrentWd.Where(p => p.DeletedFlag != true && p.EmployeeEntityId == pagingReq.EmployeeId);
                    queryCurrentWd = queryCurrentWd.AsNoTracking()
                         .Include(p => p.Employee)
                         .ThenInclude(jt => jt.Department)
                         .Include(p => p.Employee)
                         .ThenInclude(d => d.JobTitle)
                         .Include(x => x.WorkingType)
                          .Include(x => x.WorkingDayUpdates)
                         .ThenInclude(x => x.WorkingDayApprovals)
                        .Include(x => x.TimeSheets)
                         .OrderBy(x => x.WorkingDate)
                         .AsQueryable();

                    queryCurrentWd = DetailAttendanceResponse.PrepareDetailReportWhereQueryFilter(queryCurrentWd, paramFilter);
                    queryCurrentWd = DetailAttendanceResponse.PrepareDetailReportWhereQuerySearch(queryCurrentWd, paramSearch);
                    queryCurrentWd = DetailAttendanceResponse.PrepareDetailReportQuerySort(queryCurrentWd, paramSort);
                    queryCurrentWd = queryCurrentWd.Where(s => (s.WorkingDate.Value >= startDate && s.WorkingDate.Value <= endDate));
                    rs.TotalRecord = queryCurrentWd.LongCount();
                    rs.rawDatas = queryCurrentWd.Skip(pagingReq.GetSkip()).Take(pagingReq.GetLimit()).ToList();
                    //var calculate = CalculateMonthWorkingType(rs.rawDatas, listWorkingType);
                    rs.ListData = rs.rawDatas
                      //.OrderBy(x => x.WorkingDate)
                      .Select(x => new DetailAttendanceResponse().SetData(x))
                      .ToList();
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
        public List<WorkingDayEntity> GetListWorkingDayByEmployeeId3(
                                                      long employeeId,
                                                      DateTime getDate)
        {
            try
            {
                string cacheKey = $"{cacheKeyList}_GetListWorkingDayByEmployeeId3";
                cacheKey = $"{cacheKey}:{getDate}" +
                            $"_{employeeId}";
                cacheKey = EncodeUtil.MD5(cacheKey);
                List<WorkingDayEntity> rs = CachedFunc.GetRedisData<List<WorkingDayEntity>>(cacheKey, null);
                if (rs == null)
                {
                    rs = new List<WorkingDayEntity>();

                    DateTime? startDate = DateTimeHelper.GetStartOfDate(getDate);
                    DateTime? endDate = DateTimeHelper.GetEndOfDate(getDate);

                    var queryCurrentWd = _context.Set<WorkingDayEntity>()
                      .AsNoTracking()
                      .AsQueryable();
                    //* where
                    queryCurrentWd = queryCurrentWd.Where(p => p.DeletedFlag != true && p.EmployeeEntityId == employeeId);
                    queryCurrentWd = queryCurrentWd.AsNoTracking()
                         .Include(p => p.Employee)
                         .ThenInclude(jt => jt.Department)
                         .Include(p => p.Employee)
                         .ThenInclude(d => d.JobTitle)
                         .Include(x => x.WorkingType)
                          .Include(x => x.WorkingDayUpdates)
                         .ThenInclude(x => x.WorkingDayApprovals)
                        .Include(x => x.TimeSheets)
                         .OrderBy(x => x.WorkingDate)
                         .AsQueryable();

                    queryCurrentWd = queryCurrentWd.Where(s => (s.WorkingDate.Value >= startDate && s.WorkingDate.Value <= endDate));
                    rs = queryCurrentWd.ToList();
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

        public async Task<List<WorkingTypeProcess>> CalculateMonthWorkingType_bk(List<WorkingDayEntity> workingDayEntity)
        {
            List<WorkingTypeProcess> ret = new List<WorkingTypeProcess>();
            for (int i = 0; i < workingDayEntity.Count; i++)
            {
                var rcmWT = new WorkingTypeProcess()
                {
                    Date = workingDayEntity[i].WorkingDate.GetValueOrDefault()
                };
                var timeSheets = workingDayEntity[i].TimeSheets?.OrderBy(x => x.RecordedTime).ToList();
                if (timeSheets == null || timeSheets.Count <= 0) continue;
                double counterBeforeShift3 = 0;
                double counterExtendShift3 = 0;
                bool isCheckInDay = false;
                var timeCheckIns = timeSheets.Where(x => x.Status == EnumFaceId.Check_In).Select(x =>
                {
                    var timeCheckInNew = DateTimeUtil.RoundToNearest(x.RecordedTime.GetValueOrDefault(), TimeSpan.FromMinutes(30));
                    timeCheckInNew = new DateTime(1901, 1, 1, timeCheckInNew.Hour, timeCheckInNew.Minute, timeCheckInNew.Second);
                    return timeCheckInNew;
                })?.OrderByDescending(x => x)?.FirstOrDefault();
                var timeCheckOuts = timeSheets.Where(x => x.Status == EnumFaceId.Check_Out).Select(x =>
                {
                    var timeCheckInNew = DateTimeUtil.RoundToNearest(x.RecordedTime.GetValueOrDefault(), TimeSpan.FromMinutes(30));
                    timeCheckInNew = new DateTime(1901, 1, 1, timeCheckInNew.Hour, timeCheckInNew.Minute, timeCheckInNew.Second);
                    return timeCheckInNew;
                })?.OrderByDescending(x => x)?.FirstOrDefault();
                if (timeCheckIns.Value.Year == 0001) timeCheckIns = null;           //  if time is default value => set to null 
                if (timeCheckOuts.Value.Year == 0001) timeCheckOuts = null;     // 
                if (timeCheckIns == null && timeCheckOuts != null)
                {
                    timeCheckIns = timeCheckOuts.Value.AddHours(-8);
                }
                if (timeCheckOuts == null && timeCheckIns != null)
                {
                    if (i >= workingDayEntity.Count - 1)
                    {
                        timeCheckOuts = timeCheckIns.Value.AddHours(8);
                    }
                    else
                    {
                        timeCheckOuts = workingDayEntity[i + 1]
                          .TimeSheets
                          .FirstOrDefault(x => x.RecordedTime.Value.Year == rcmWT.Date.AddDays(1).Year
                          && x.RecordedTime.Value.Month == rcmWT.Date.AddDays(1).Month
                          && x.RecordedTime.Value.Day == rcmWT.Date.AddDays(1).Day
                          && x.Status == EnumFaceId.Check_Out
                          )?.RecordedTime;
                        timeCheckOuts = new DateTime(1901, 1, 2, timeCheckOuts.Value.Hour, timeCheckOuts.Value.Minute, timeCheckOuts.Value.Second);
                    }
                }
                if ((timeCheckIns >= ConstDatabase.StartTimeShift3 && timeCheckIns <= ConstDatabase.NextdayStartTimeShift1)) // if main shift is 3
                {
                    // check if any OT time from  0 -> 22 h
                    if (timeCheckOuts.Value.Day != timeCheckIns.Value.Day)
                    {
                        timeCheckOuts = timeCheckIns.GetValueOrDefault().AddHours(8);
                        counterBeforeShift3 = timeCheckOuts < ConstDatabase.StartTimeShift2.AddDays(1)
                        ? (timeCheckOuts - ConstDatabase.StartTimeShift1.AddDays(1)).Value.TotalHours
                        : (timeCheckOuts - ConstDatabase.StartTimeShift2.AddDays(1)).Value.TotalHours;
                    }
                    else
                    {
                        counterBeforeShift3 = timeCheckOuts < ConstDatabase.StartTimeShift2
                      ? (timeCheckOuts - ConstDatabase.StartTimeShift1).Value.TotalHours
                      : (timeCheckOuts - ConstDatabase.StartTimeShift2).Value.TotalHours;
                    }

                    rcmWT.Code = (counterBeforeShift3 > 0 ? $"{counterBeforeShift3}+" : "") + "C3";
                }
                else // main shift is 1,2
                {
                    if ((timeCheckIns < ConstDatabase.StartTimeShift1) || timeCheckOuts > ConstDatabase.StartTimeShift3) // đi sớm, hoặc về muộn lấn giờ ca 3 => tính OT <x>C3
                    {
                        rcmWT.Code = $"" +
                          $"{((ConstDatabase.StartTimeShift1 - timeCheckIns).Value.TotalHours > 0 ? (ConstDatabase.StartTimeShift1 - timeCheckIns).Value.TotalHours : 0) +
                          ((timeCheckOuts - ConstDatabase.StartTimeShift3).Value.TotalHours > 0 ? (timeCheckOuts - ConstDatabase.StartTimeShift3).Value.TotalHours : 0)}C3";
                    }
                    else
                    {
                        rcmWT.Code = $"{(timeCheckOuts - timeCheckIns)?.TotalHours - 8}"; // normal OT, if working hour is more than 8
                    }
                }
                /// 
                ret.Add(rcmWT);
            }

            return ret;
        }
        public Task<List<WorkingDayEntity>> GetEmployeeWorkingDayByDate(long employeeId, DateTime startTime, DateTime endTime)
        {
            return _context.Set<WorkingDayEntity>()
                .Where(x => x.DeletedFlag != true)
              .Where(x => x.EmployeeEntityId == employeeId && x.WorkingDate >= startTime && x.WorkingDate <= endTime)
              .Include(x => x.WorkingType)
              .OrderBy(x => x.WorkingDate)
              .ToListAsync();
        }

        public Dictionary<long, List<WorkingDayEntity>> GetEmployeeWorkingDayByDate(
          List<long> listEmployeeId,
          DateTime startTime,
          DateTime endTime,
          bool isDirect = false)
        {
            try
            {
                Dictionary<long, List<WorkingDayEntity>>? result = null;
                string cacheKey = $"{cacheKeyListByListIds}_GetEmployeeWorkingDayByDate_{startTime}_{endTime}:{(listEmployeeId == null ? "" : string.Join(",", listEmployeeId))}";
                cacheKey = EncodeUtil.MD5(cacheKey);
                if (!isDirect)
                {
                    result = CachedFunc.GetRedisData<Dictionary<long, List<WorkingDayEntity>>>(cacheKey, null);
                }

                if (result == null || result.Count <= 0)
                {
                    result = new Dictionary<long, List<WorkingDayEntity>>();
                    var list = _context.Set<WorkingDayEntity>()
                    .Where(x => (x.EmployeeEntityId != null && listEmployeeId.Contains(x.EmployeeEntityId.GetValueOrDefault()))
                          && x.WorkingDate >= startTime
                          && x.WorkingDate <= endTime)
                    .Include(x => x.WorkingType)
          .Include(x => x.TimeSheets)
          .OrderBy(x => x.WorkingDate)
                    .ToList();

                    foreach (var item in list)
                    {
                        if (!result.ContainsKey(item.EmployeeEntityId.Value))
                        {
                            result.Add(item.EmployeeEntityId.Value, new List<WorkingDayEntity>());
                        }
                        result[item.EmployeeEntityId.Value].Add(item);
                    }

                    CachedFunc.AddEntityCacheKey(GetName(), cacheKey, true);
                    // TODO: error at convert to json so long when call SetRedisData()
                    CachedFunc.SetRedisData(cacheKey, result, ConstCommon.ConstGetListCacheExpiredTimeInSeconds);
                }
                return result;
            }
            catch (Exception dbEx)
            {
                throw new DBException(dbEx);
            }
        }

        /// <summary>
        ///  Get Employee current OT Hours, by default it cannot upper 40
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="workingTypeEntities"> list of working type OT </param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<int> GetOTHour(long employeeId, List<WorkingTypeEntity> workingTypeEntities, List<HolidayScheduleEntity> holidayScheduleEntities, DateTime startTime, DateTime endTime, WorkingDayEntity incomming = null)
        {
            var listWTIds = workingTypeEntities
              .Select(x => x.Code).ToList();
            var ots = _context.Set<WorkingDayEntity>()
              .Include(x => x.WorkingType)
              .AsNoTracking()
              .Where(x => x.DeletedFlag != true)
              .Where(x => x.WorkingDate >= startTime && x.WorkingDate <= endTime)
              .Where(x => x.EmployeeEntityId == employeeId)
              .Where(x => (x.WorkingType != null && listWTIds.Contains(x.WorkingType.Code)) || (x.RecommendType != null && listWTIds.Contains(x.RecommendType))).ToList();
            if (incomming != null)
            {
                ots.Add(incomming);
            }
            int sumOT = 0;
            foreach (var ot in ots)
            {
                if (ot.WorkingType == null && ot.WorkingTypeEntityId.GetValueOrDefault() > 0)
                {
                    ot.WorkingType = workingTypeEntities.FirstOrDefault(x => x.Id == ot.WorkingTypeEntityId);
                }
                sumOT += await GetOTHour(workingTypeEntities, holidayScheduleEntities, ot);
            }
            return sumOT;
        }
        public async Task<int> GetOTHour(List<WorkingTypeEntity> workingTypeEntities, List<HolidayScheduleEntity> holidayScheduleEntities, List<WorkingDayEntity> ots)
        {
            var listWTIds = workingTypeEntities
              .Select(x => x.Code).ToList();
            int sumOT = 0;
            foreach (var ot in ots)
            {
                if (ot.WorkingType == null && ot.WorkingTypeEntityId.GetValueOrDefault() > 0)
                {
                    ot.WorkingType = workingTypeEntities.FirstOrDefault(x => x.Id == ot.WorkingTypeEntityId);
                }
                if (ot.WorkingTypeEntityId == null && ot.RecommendType != null)
                {
                    ot.WorkingType = workingTypeEntities.FirstOrDefault(x => x.Code == ot.RecommendType);
                }
                sumOT += await GetOTHour(workingTypeEntities, holidayScheduleEntities, ot);
            }
            return sumOT;
        }
        public async Task<int> GetOTHour(
            List<WorkingTypeEntity> workingTypeEntities,
            List<HolidayScheduleEntity> holidayScheduleEntities,
            WorkingDayEntity ot)
        {
            var listWTIds = workingTypeEntities
              .Select(x => x.Id).ToList();
            int sumOT = 0;
            int meal = 0, ot150 = 0, nightshift30 = 0, ot200 = 0, weeken_meal = 0, weeken_200 = 0, ot_night_270 = 0, hld_meal = 0, ot_300 = 0, ot_390 = 0;
            var holiday = holidayScheduleEntities.Where(x => x.StartDate <= ot.WorkingDate && x.EndDate >= ot.WorkingDate).FirstOrDefault();
            if (holiday != null)
            {
                switch (holiday.HolidayType.GetValueOrDefault())
                {
                    case EnumHolidayCode.HLD:

                        ot_300 += (ot.WorkingType?.Holiday_OT_300).GetValueOrDefault();
                        hld_meal += (ot.WorkingType?.Holiday_Meal).GetValueOrDefault();
                        ot_390 += (ot.WorkingType?.Holiday_OT_Night_390).GetValueOrDefault();
                        break;
                    case EnumHolidayCode.OFF:
                        weeken_200 += (ot.WorkingType?.Weekend_OT_200).GetValueOrDefault();
                        weeken_meal += (ot.WorkingType?.Weekend_Meal).GetValueOrDefault();
                        ot_night_270 += (ot.WorkingType?.Weekend_Night_OT_270).GetValueOrDefault();
                        break;
                    case EnumHolidayCode.XMS:
                        var weeken = ot.WorkingDate.GetValueOrDefault().DayOfWeek;
                        if (weeken == DayOfWeek.Saturday || weeken == DayOfWeek.Sunday)
                        {
                            weeken_200 += (ot?.WorkingType?.Weekend_OT_200).GetValueOrDefault();
                            weeken_meal += (ot.WorkingType?.Weekend_Meal).GetValueOrDefault();
                            ot_night_270 += (ot.WorkingType?.Weekend_Night_OT_270).GetValueOrDefault();
                        }
                        else
                        {
                            meal += (ot.WorkingType?.Normal_Meal).GetValueOrDefault();
                            nightshift30 += (ot.WorkingType?.Normal_Night_30).GetValueOrDefault();
                            ot150 += (ot.WorkingType?.OT_150).GetValueOrDefault();
                            ot200 += (ot.WorkingType?.OT_200).GetValueOrDefault();
                        }
                        break;
                }
            }
            else
            {
                var weeken = ot.WorkingDate.GetValueOrDefault().DayOfWeek;
                if (weeken == DayOfWeek.Saturday || weeken == DayOfWeek.Sunday)
                {
                    weeken_200 += (ot.WorkingType?.Weekend_OT_200).GetValueOrDefault();
                    weeken_meal += (ot.WorkingType?.Weekend_Meal).GetValueOrDefault();
                    ot_night_270 += (ot.WorkingType?.Weekend_Night_OT_270).GetValueOrDefault();
                }
                else
                {
                    meal += (ot.WorkingType?.Normal_Meal).GetValueOrDefault();
                    nightshift30 += (ot.WorkingType?.Normal_Night_30).GetValueOrDefault();
                    ot150 += (ot.WorkingType?.OT_150).GetValueOrDefault();
                    ot200 += (ot.WorkingType?.OT_200).GetValueOrDefault();
                }
            }
            return meal + ot150 + ot200 + nightshift30 + weeken_meal + weeken_200 + ot_night_270 + hld_meal + ot_300 + ot_390;
        }
        public async Task<WorkingDayEntity> GetEmpDate(DateTime date, long empId)
        {

            var data = await _context.Set<WorkingDayEntity>()
              .Include(x => x.TimeSheets)
              .FirstOrDefaultAsync(x => x.WorkingDate.Value.Year == date.Year
              && x.WorkingDate.Value.Month == date.Month
              && x.WorkingDate.Value.Day == date.Day
              && x.EmployeeEntityId == empId
              );
            return data;

        }
        public async Task<WorkingDayEntity> GetByTimeSheet(TimeSheetEntity timeSheetEntity)
        {
            return _context.Set<WorkingDayEntity>()
              .AsNoTracking()
              .Include(x => x.Employee)
              .FirstOrDefault(x => x.EmployeeEntityId == timeSheetEntity.EmployeeId
              && x.WorkingDate.Value.Day == timeSheetEntity.RecordedTime.Value.Day
            && x.WorkingDate.Value.Month == timeSheetEntity.RecordedTime.Value.Month
              && x.WorkingDate.Value.Year == timeSheetEntity.RecordedTime.Value.Year);
            ;
        }
        private async Task<WorkingDayEntity> CheckIfExist(WorkingDayEntity entity)
        {
            return await _context.Set<WorkingDayEntity>().FirstOrDefaultAsync(x => x.Id == entity.Id);
        }

        public Task<List<WorkingDayEntity>> GetWorkingDayByDate(DateTime startTime, DateTime endTime)
        {
            return _context.Set<WorkingDayEntity>()
              .Where(x => x.WorkingDate >= startTime && x.WorkingDate <= endTime)
              .Include(x => x.Employee)
              .ThenInclude(x => x.Department)
              .Include(x => x.Employee)
              .ThenInclude(x => x.JobTitle)
              .OrderBy(x => x.WorkingDate)
              .ToListAsync();
        }
    }
}
