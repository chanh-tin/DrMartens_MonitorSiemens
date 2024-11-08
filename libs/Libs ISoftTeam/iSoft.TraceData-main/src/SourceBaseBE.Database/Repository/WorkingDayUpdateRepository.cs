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
using SourceBaseBE.Database.Models.ResponseModels;
using SourceBaseBE.Database.Models.RequestModels;
using iSoft.Common.Enums;
using iSoft.Common.Models.ResponseModels;
using SourceBaseBE.Database.Helpers;
using SourceBaseBE.Database.Enums;
using SourceBaseBE.Database.Models.RequestModels.Report;
using iSoft.Common.ExtensionMethods;
using iSoft.Common.Utils;

namespace SourceBaseBE.Database.Repository
{
	public class WorkingDayUpdateRepository : BaseCRUDRepository<WorkingDayUpdateEntity>
	{
		public WorkingDayUpdateRepository(CommonDBContext dbContext)
			: base(dbContext)
		{
		}
		public override string GetName()
		{
			return nameof(WorkingDayUpdateRepository);
		}
		/// <summary>
		/// GetById (@GenCRUD)
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		/// <exception cref="DBException"></exception>
		public override WorkingDayUpdateEntity? GetById(long id, bool isTracking = false)
		{
			try
			{
				WorkingDayUpdateEntity? result = null;
				//string cacheKey = $"{cacheKeyDetail}:{id}";
				//if (!isDirect && !isTracking)
				//{
				//	result = CachedFunc.GetRedisData<WorkingDayUpdateEntity>(cacheKey, null);
				//}

				//if (result == null)
				//    {
				var dataSet = _context.Set<WorkingDayUpdateEntity>();
				IQueryable<WorkingDayUpdateEntity> queryable;
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
				//result.WorkingDayUpdate2s = result.WorkingDayUpdate2s.Select(x => new WorkingDayUpdate2Entity() { Id = x.Id, LossName = x.LossName }).ToList();

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
		public async Task<WorkingDayUpdateEntity>? GetByIdAsync(long id, bool isDirect = false, bool isTracking = true)
		{
			try
			{
				WorkingDayUpdateEntity? result = null;
				//string cacheKey = $"{cacheKeyDetail}:{id}";
				//if (!isDirect && !isTracking)
				//{
				//	result = CachedFunc.GetRedisData<WorkingDayUpdateEntity>(cacheKey, null);
				//}

				//if (result == null)
				//{
				result = await (isTracking ?
				_context.Set<WorkingDayUpdateEntity>()
						  //.AsNoTracking()
						  .AsQueryable()
						  /*[GEN-7]*/
						  .Where(entity => entity.DeletedFlag != true && entity.Id == id)
						  .FirstOrDefaultAsync() :
						  _context.Set<WorkingDayUpdateEntity>()
						  .AsNoTracking()
						  .AsQueryable()
						  /*[GEN-7]*/
						  .Where(entity => entity.DeletedFlag != true && entity.Id == id)
						  .FirstOrDefaultAsync());
				//result.WorkingDayUpdate2s = result.WorkingDayUpdate2s.Select(x => new WorkingDayUpdate2Entity() { Id = x.Id, LossName = x.LossName }).ToList();

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
		public override List<WorkingDayUpdateEntity> GetList(PagingRequestModel pagingReq = null, bool isDirect = false, bool isTracking = false)
		{
			try
			{
				string cacheKey = $"{cacheKeyList}_GetList";
				if (pagingReq != null)
				{
					cacheKey = $"{cacheKeyList}_GetList:{pagingReq.Page}|{pagingReq.PageSize}";
				}
				List<WorkingDayUpdateEntity>? result = CachedFunc.GetRedisData<List<WorkingDayUpdateEntity>>(cacheKey, null);
				if (result == null)
				{
					result = new List<WorkingDayUpdateEntity>();
					if (pagingReq != null)
					{
						result = _context.Set<WorkingDayUpdateEntity>()
								.AsNoTracking()
								.AsQueryable()
								/*[GEN-11]*/
								.Where(entity => entity.DeletedFlag != true)
								.OrderBy(entity => entity.Order).ThenBy(entity => entity.Id)
								.Skip(pagingReq.GetSkip()).Take(pagingReq.GetLimit())
								.AsParallel()
								.ToList();

						//.Select(entity => new WorkingDayUpdateEntity
						// {
						//   Id = entity.Id,
						//   WorkingDayUpdate2s = entity.WorkingDayUpdate2s.Select(x => new WorkingDayUpdate2Entity { Id = x.Id, LossName = x.LossName }).ToList()
						// })

						for (var i = 0; i < result.Count; i++)
						{


							/*[GEN-12]*/
						}
					}
					else
					{
						result = _context.Set<WorkingDayUpdateEntity>()
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
		public WorkingDayUpdateEntity Upsert(WorkingDayUpdateEntity entity/*[GEN-8]*/, long? userId = null)
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
		public WorkingDayUpdateEntity Insert(WorkingDayUpdateEntity entity/*[GEN-8]*/, long? userId = null)
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

					/*[GEN-10]*/
					_context.Set<WorkingDayUpdateEntity>().Add(entity);
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
		public WorkingDayUpdateEntity Update(WorkingDayUpdateEntity entity/*[GEN-8]*/, long? userId = null)
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

					/*[GEN-9]*/
					base.Update(entity);
				}
				return entity;
			}
			catch (Exception ex)
			{
				throw new DBException(ex);
			}
		}
		public override object GetDisplayField(WorkingDayUpdateEntity entity)
		{
			return entity.ToString();
		}
		public override List<FormSelectOptionModel> GetSelectData(string entityName, EnumAttributeRelationshipType category)
		{
			List<FormSelectOptionModel> rs = GetListWithNoInclude().Select(x => new FormSelectOptionModel(x.Id, GetDisplayField(x))).ToList();
			return rs;
		}
		public PendingRequestPagingResponseModel GetListPendingRequest(
			List<EmployeeEntity> employees,
			 PagingFilterRequestModel pagingReq)
		{
			try
			{
				if (employees == null || employees.Count <= 0)
				{
					throw new Exception("Invalid parameter: employee");
				}
				var employeeIds = employees.Select(x => x.Id).ToList();
				string cacheKey = $"{cacheKeyList}_GetListPendingRequest";
				if (pagingReq != null)
				{
					cacheKey = $"{cacheKey}:{pagingReq.GetKeyCache()}";
				}
				PendingRequestPagingResponseModel rs = CachedFunc.GetRedisData<PendingRequestPagingResponseModel>(cacheKey, null);
				if (rs == null)
				{
					rs = new PendingRequestPagingResponseModel();
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

					var query = _context.Set<WorkingDayUpdateEntity>()
							.AsNoTracking()
							.AsQueryable();
					//* where
					query = query
						.Where(p => p.DeletedFlag != true)
						.Where(x => employeeIds.Contains(x.EmployeeId.GetValueOrDefault()) || employeeIds.Contains(x.WorkingDay.EmployeeEntityId.GetValueOrDefault()))
						.Include(x => x.Editer)
						.Include(p => p.WorkingDay)
							.ThenInclude(x => x.Employee)
							.ThenInclude(x => x.Department)
						.Include(p => p.WorkingDay)
							.ThenInclude(x => x.Employee)
							.ThenInclude(x => x.JobTitle)
						.Include(p => p.Editer)
						.Include(x => x.WorkingType)
						.Include(x => x.WorkingDayApprovals)
						 .OrderBy(x => x.Id)
						;
					query = query.Where(x => x.CreatedAt >= startDate && (x.CreatedAt <= endDate || x.UpdatedAt <= endDate));
					//var list = query.Skip(pagingReq.GetSkip()).Take(pagingReq.GetLimit()).ToList();
					rs.ListData = WorkingdayUpdateDTO.SetData(query.ToList(), employees);
					rs.TotalRecord = employees.Count;


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
		public async Task<PersonalPendingRequestPagingResponseModel> GetPersonalPendingRequest(
		   long employeeId,
		   PagingFilterRequestModel pagingReq,
			Dictionary<string, object> paramFilter,
			SearchModel paramSearch,
			Dictionary<string, long> paramSort
			)
		{
			if (employeeId <= 0) throw new Exception($"Invalid Employee");
			try
			{
				string cacheKey = $"{cacheKeyList}_GetPersonalPendingRequest";
				if (pagingReq != null)
				{
					cacheKey = $"{cacheKey}:{pagingReq.GetKeyCache()}";
				}
				cacheKey += $"{paramFilter.ToJson()}";
				cacheKey += $"{paramSearch.ToJson()}";
				cacheKey += $"{paramSort.ToJson()}";
				cacheKey += $"{employeeId.ToJson()}";
				cacheKey = EncodeUtil.MD5(cacheKey);

				PersonalPendingRequestPagingResponseModel rs = CachedFunc.GetRedisData<PersonalPendingRequestPagingResponseModel>(cacheKey, null);
				if (rs == null || rs.ListData == null || rs.ListData.Count <= 0)
				{
					rs = new PersonalPendingRequestPagingResponseModel();
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

					var query = _context.Set<WorkingDayUpdateEntity>()
							.AsNoTracking()
							.AsQueryable();

					//* where
					query = query.Where(p => p.DeletedFlag != true)
						.AsNoTracking()
						.Include(x => x.Editer)
						.ThenInclude(x => x.ItemEmployee)
						.Include(p => p.WorkingDay)
							.ThenInclude(x => x.Employee)
						.Include(x => x.WorkingType)
						.Include(x => x.OriginWorkingType)
						.Include(x => x.WorkingDayApprovals)
						.Where(x => x.EmployeeId == employeeId)
						.Where(x => x.CreatedAt >= startDate && x.CreatedAt <= endDate)
								 .OrderBy(x => x.UpdatedAt)
						;
					var lst_working_aprrovals = query.SelectMany(x => x.WorkingDayApprovals);
					query = DetailWorkingdayUpdateDTO.PrepareWhereQueryFilter(query, paramFilter);
					//query = DetailWorkingdayUpdateDTO.PrepareWhereQuerySearch(query, paramSearch);
					query = DetailWorkingdayUpdateDTO.PrepareQuerySort(query, paramSort);
					rs.ListData = query.Skip(pagingReq.GetSkip()).Take(pagingReq.GetLimit()).Select(x => DetailWorkingdayUpdateDTO.SetData(x)).ToList();
					rs.TotalRecord = query.AsParallel().LongCount();
					var countModels = new List<CountResponseModel>
					  {
						 new CountResponseModel { Key = "approval", Number =  lst_working_aprrovals.LongCount(x=>x.ApproveStatus== Enums.EnumApproveStatus.ACCEPT) },
						 new CountResponseModel { Key = "denied", Number  = lst_working_aprrovals.LongCount(x=>x.ApproveStatus== Enums.EnumApproveStatus.REJECT)},
						 new CountResponseModel { Key = "pending", Number = lst_working_aprrovals.LongCount(x=>x.ApproveStatus== Enums.EnumApproveStatus.PENDING) }
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
		public Task<WorkingDayUpdateEntity> GetByIdWithWdAAsync(long id)
		{
			if (id <= 0) throw new Exception($"Invalid Id WOD");
			return _context.Set<WorkingDayUpdateEntity>()
				.AsNoTracking()
				.Include(x => x.WorkingDayApprovals)
				.Include(x => x.WorkingDay)
				.FirstOrDefaultAsync(x => x.Id == id);
		}
		public Task<List<WorkingDayUpdateEntity>> GetEmployeePendingRequest(long employeeId, DateTime dateFrom, DateTime dateTo)
		{
			if (employeeId <= 0) throw new Exception("Invalid Employee Parameter");
			var ret = _context.Set<WorkingDayUpdateEntity>()
						   .Include(x => x.WorkingDay)
						   .ThenInclude(x => x.Employee)
						   .Include(x => x.OriginWorkingType)
						   .Include(x => x.WorkingType)
						   .Include(x => x.Editer)
							 .ThenInclude(x => x.ItemEmployee)
						   .Where(x => x.CreatedAt >= dateFrom && x.CreatedAt <= dateTo && (x.WorkingDay.EmployeeEntityId == employeeId || x.EmployeeId == employeeId));
			return ret.ToListAsync();
		}
		public Task<List<WorkingDayUpdateEntity>> GetEmployeePendingRequest(long employeeId, EnumApproveStatus status, EmployeeAttendanceDetailRequest request)
		{
			if (employeeId <= 0) throw new Exception("Invalid Employee Parameter");
			var ret = _context.Set<WorkingDayUpdateEntity>()
						   .Include(x => x.WorkingDay)
						   .ThenInclude(x => x.Employee)
						   .Include(x => x.OriginWorkingType)
						   .Include(x => x.WorkingType)
						   .Include(x => x.Editer)
							 .ThenInclude(x => x.ItemEmployee)
							 .Include(x => x.WorkingDayApprovals)
						   .Where(x => x.CreatedAt >= request.DateFrom && x.CreatedAt <= request.DateTo && x.EmployeeId == employeeId)
						   .Where(x => x.WorkingDayApprovals.FirstOrDefault().ApproveStatus == status)
						   ;
			return ret.ToListAsync();
		}
		public async Task<DetailAttendancePagingResponseModel> GetIncommingRecord(
		   long employeeId,
			EnumApproveStatus status,
		   PagingFilterRequestModel pagingReq,
			Dictionary<string, object> paramFilter,
			SearchModel paramSearch,
			Dictionary<string, long> paramSort
			)
		{
			if (employeeId <= 0) throw new Exception($"Invalid Employee");
			try
			{
				string cacheKey = $"{cacheKeyList}_{status}_GetIncommingRecord";
				if (pagingReq != null)
				{
					cacheKey = $"{cacheKey}:{pagingReq.GetKeyCache()}";
				}
				cacheKey += $"{paramFilter.ToJson()}";
				cacheKey += $"{paramSearch.ToJson()}";
				cacheKey += $"{paramSort.ToJson()}";
				cacheKey = EncodeUtil.MD5(cacheKey);

				DetailAttendancePagingResponseModel rs = CachedFunc.GetRedisData<DetailAttendancePagingResponseModel>(cacheKey, null);
				if (rs == null || rs.ListData == null || rs.ListData.Count <= 0)
				{
					rs = new DetailAttendancePagingResponseModel();
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
					DateTime? startDate = pagingReq.DateFrom == null ? DateTimeHelper.GetStartOfDate(DateTime.Now).AddHours(6) : DateTimeHelper.GetStartOfDate(pagingReq.DateFrom.GetValueOrDefault());
					DateTime? endDate = pagingReq.DateTo == null ? DateTimeHelper.GetEndOfDate(DateTime.Now).AddDays(1).AddHours(6) : DateTimeHelper.GetEndOfDate(pagingReq.DateTo.GetValueOrDefault());



					//* where
					var query = _context.Set<WorkingDayUpdateEntity>()
							.AsNoTracking()
							.AsQueryable()
					   .Include(x => x.WorkingDay)
					   .ThenInclude(x => x.Employee)
					   .Include(x => x.OriginWorkingType)
					   .Include(x => x.WorkingType)
					   .Include(x => x.Editer)
						 .ThenInclude(x => x.ItemEmployee)
						 .Include(x => x.WorkingDayApprovals)
						 .Where(x => x.DeletedFlag != true)
					   .Where(x => x.CreatedAt >= pagingReq.DateFrom && x.CreatedAt <= pagingReq.DateTo && x.EmployeeId == employeeId && x.WorkingDayId == null)
					   .Where(x => x.WorkingDayApprovals.FirstOrDefault().ApproveStatus == status)
					;
					var lst_working_aprrovals = query.SelectMany(x => x.WorkingDayApprovals);
					query = DetailWorkingdayUpdateDTO.PrepareWhereQueryFilter(query, paramFilter);
					query = DetailWorkingdayUpdateDTO.PrepareWhereQuerySearch(query, paramSearch);
					query = DetailWorkingdayUpdateDTO.PrepareQuerySort(query, paramSort);
					rs.ListIncomingData = query.Skip(pagingReq.GetSkip()).Take(pagingReq.GetLimit()).Select(x => new DetailAttendanceResponse().SetData(x)).ToList();
					rs.TotalRecord = await query.LongCountAsync();
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
	}
}
