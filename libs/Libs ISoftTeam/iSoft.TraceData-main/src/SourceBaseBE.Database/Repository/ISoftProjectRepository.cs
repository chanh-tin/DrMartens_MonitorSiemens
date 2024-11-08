using iSoft.Common;
using iSoft.Common.Exceptions;
using iSoft.Common.Models.RequestModels;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using iSoft.Redis.Services;
using SourceBaseBE.Database.DBContexts;
using SourceBaseBE.Database.Entities;
using iSoft.Database.Models;
using iSoft.Common.Models.ConfigModel.Subs;
using iSoft.Common.Enums;

namespace SourceBaseBE.Database.Repository
{
	public class ISoftProjectRepository : BaseCRUDRepository<ISoftProjectEntity>
	{
		public ISoftProjectRepository(CommonDBContext dbContext)
			: base(dbContext)
		{
		}
		public override string GetName()
		{
			return nameof(ISoftProjectRepository);
		}
		/// <summary>
		/// GetById (@GenCRUD)
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		/// <exception cref="DBException"></exception>
		public override ISoftProjectEntity? GetById(long id, bool isTracking = false)
		{
			try
			{
				ISoftProjectEntity? result = null;
				//string cacheKey = $"{cacheKeyDetail}:{id}";
				//if (!isDirect && !isTracking)
				//{
				//	result = CachedFunc.GetRedisData<ISoftProjectEntity>(cacheKey, null);
				//}

				//if (result == null)
    //    {
          var dataSet = _context.Set<ISoftProjectEntity>();
          IQueryable<ISoftProjectEntity> queryable;
          if (!isTracking)
          {
            queryable = dataSet.AsNoTracking().AsQueryable();
          }
          else
          {
            queryable = dataSet.AsQueryable();
          }
          result = queryable
                .Include(entity => entity.ListUser)/*[GEN-7]*/
							  .Where(entity => entity.DeletedFlag != true && entity.Id == id)
							  .FirstOrDefault();
					//result.ISoftProject2s = result.ISoftProject2s.Select(x => new ISoftProject2Entity() { Id = x.Id, LossName = x.LossName }).ToList();

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
		public override List<ISoftProjectEntity> GetList(PagingRequestModel pagingReq = null, bool isDirect = false, bool isTracking = false)
		{
			try
			{
				string cacheKey = $"{cacheKeyList}";
				if (pagingReq != null)
				{
					cacheKey = $"{cacheKeyList}:{pagingReq.Page}|{pagingReq.PageSize}";
				}
				List<ISoftProjectEntity>? result = CachedFunc.GetRedisData<List<ISoftProjectEntity>>(cacheKey, null);
				if (result == null)
				{
					result = new List<ISoftProjectEntity>();
					if (pagingReq != null)
					{
						result = _context.Set<ISoftProjectEntity>()
								.AsNoTracking()
								.AsQueryable()
								.Include(entity => entity.ListUser)/*[GEN-11]*/
								.Where(entity => entity.DeletedFlag != true)
								.OrderBy(entity => entity.Order).ThenBy(entity => entity.Id)
								.Skip(pagingReq.GetSkip()).Take(pagingReq.GetLimit())
								.AsParallel()
								.ToList();

						//.Select(entity => new ISoftProjectEntity
						// {
						//   Id = entity.Id,
						//   ISoftProject2s = entity.ISoftProject2s.Select(x => new ISoftProject2Entity { Id = x.Id, LossName = x.LossName }).ToList()
						// })

						for (var i = 0; i < result.Count; i++)
						{

							result[i].ListUser = result[i].ListUser?.Select(x => new UserEntity() { Id = x.Id }).ToList();
							/*[GEN-12]*/
						}
					}
					else
					{
						result = _context.Set<ISoftProjectEntity>()
								.AsNoTracking()
								.AsQueryable()
								.Include(entity => entity.ListUser)/*[GEN-13]*/
								.Where(entity => entity.DeletedFlag != true)
								.OrderBy(entity => entity.Order).ThenBy(entity => entity.Id)
								.Skip(0).Take(ConstCommon.ConstSelectListMaxRecord)
								.AsParallel()
								.ToList();

						for (var i = 0; i < result.Count; i++)
						{

							result[i].ListUser = result[i].ListUser?.Select(x => new UserEntity() { Id = x.Id }).ToList();
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
		public ISoftProjectEntity Upsert(ISoftProjectEntity entity, List<UserEntity> authUserChildren/*[GEN-8]*/, long? userId = null)
		{
			try
			{
				if (entity.Id <= 0)
				{
					// Insert
					entity = Insert(entity, authUserChildren/*[GEN-4]*/, userId);
				}
				else
				{
					// Update
					entity = Update(entity, authUserChildren/*[GEN-4]*/, userId);
				}
				return entity;
			}
			catch (Exception ex)
			{
				throw new DBException(ex);
			}
		}
		public ISoftProjectEntity Insert(ISoftProjectEntity entity, List<UserEntity> authUserChildren/*[GEN-8]*/, long? userId = null)
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
					entity.ListUser = MergeChildrenEntity(entity.ListUser, authUserChildren);
					/*[GEN-10]*/
					_context.Set<ISoftProjectEntity>().Add(entity);
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
		public ISoftProjectEntity Update(ISoftProjectEntity entity, List<UserEntity> authUserChildren/*[GEN-8]*/, long? userId = null)
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
					entity.ListUser = MergeChildrenEntity(entity.ListUser, authUserChildren);
					/*[GEN-9]*/
					_context.Set<ISoftProjectEntity>().Update(entity);
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
		public override object GetDisplayField(ISoftProjectEntity entity)
		{
			return entity.Name.ToString();
		}
		public override List<FormSelectOptionModel> GetSelectData(string entityName, EnumAttributeRelationshipType category)
		{
			List<FormSelectOptionModel> rs = GetListWithNoInclude().Select(x => new FormSelectOptionModel(x.Id, GetDisplayField(x))).ToList();
			return rs;
		}
	}
}
