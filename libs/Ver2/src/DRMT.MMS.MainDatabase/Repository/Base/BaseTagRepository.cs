// -----------------------------------------------------------------------------
// This file was automatically generated.
// Please do not edit this file manually.
//
// Generated Date: 
//
// -----------------------------------------------------------------------------

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
using iSoft.Common.Utils;

using iSoft.Common.Enums;
using System.Collections.Generic;
using SourceBaseBE.Database.Models.RequestModels;
using SourceBaseBE.Database.Models.ResponseModels;
using PRPO.Database.Helpers;
using iSoft.Common.ExtensionMethods;
using iSoft.Database.Entities;
using iSoft.Database.Repository;

namespace SourceBaseBE.Database.Repository
{
    public class BaseTagRepository : BaseCRUDRepository<TagEntity>
    {
        public BaseTagRepository(CommonDBContext dbContext)
            : base(dbContext)
        {
        }
        public override string TableName
        {
            get { return "Tags"; }
        }
        public override string GetRepositoryName()
        {
            return nameof(BaseTagRepository);
        }
        public override object GetDisplayField(TagEntity entity)
        {
            
            
            return entity.Id;
        }
        public override List<FormSelectOptionModel> GetSelectData(string entityName, EnumAttributeRelationshipType category)
        {
            List<FormSelectOptionModel> rs = GetListWithNoInclude().Select(x => new FormSelectOptionModel(x.Id, GetDisplayField(x))).ToList();
            return rs;
        }

        /// <summary>
        /// GetById (@GenCRUD)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="DBException"></exception>
        public override TagEntity? GetById(long id, bool isTracking = false)
        {
            try
            {
                TagEntity? result = null;
                var dataSet = _context.Set<TagEntity>();
                IQueryable<TagEntity> queryable;
                if (!isTracking)
                {
                    queryable = dataSet.AsNoTracking().AsQueryable();
                }
                else
                {
                    queryable = dataSet.AsQueryable();
                }
                queryable = queryable
                      .Include(entity => entity.ItemDataBlock)
                      .Include(entity => entity.ListDataBlock)
                      
                      .Where(entity => entity.DeletedFlag != true && entity.Id == id);

                //queryable = queryable.Select(entity => new TagEntity()
                //{
                //    Id = entity.Id,
                //    Order = entity.Order,
                //    DeletedFlag = entity.DeletedFlag,
                //    CreatedAt = entity.CreatedAt,
                //    CreatedBy = entity.CreatedBy,
                //    UpdatedAt = entity.UpdatedAt,
                //    UpdatedBy = entity.UpdatedBy,
                //                         Name = entity.Name,
//                         Type = entity.Type,
//                         DataBlockId = entity.DataBlockId,
//                         Offset = entity.Offset,
//                         ItemDataBlock = entity.ItemDataBlock,
//                         ListDataBlock = entity.ListDataBlock == null ? null : entity.ListDataBlock.Where(x => x.DeletedFlag != true).ToList(),
                        
                //});
                var entity = queryable.FirstOrDefault();
                if (entity != null)
                {
                    entity.ListDataBlock = entity.ListDataBlock == null ? null : entity.ListDataBlock.Where(x => x.DeletedFlag != true).ToList();
                        
                };
                result = entity;

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
        public override List<TagEntity> GetList(PagingRequestModel pagingReq = null, bool isDirect = false, bool isTracking = false)
        {
            try
            {
                List<TagEntity>? result = null;
                string cacheKey = $"{CacheKeyList}";
                if (pagingReq != null)
                {
                    cacheKey = $"{CacheKeyList}:{pagingReq.GetKeyCache()}";
                }
                if (!isDirect && !isTracking)
                {
                    result = this.GetRedisData<List<TagEntity>>(cacheKey, null);
                }

                if (result == null)
                {
                    var dataSet = _context.Set<TagEntity>();
                    IQueryable<TagEntity> queryable;
                    if (!isTracking)
                    {
                        queryable = dataSet.AsNoTracking().AsQueryable();
                    }
                    else
                    {
                        queryable = dataSet.AsQueryable();
                    }
                    queryable = queryable
                                .Include(entity => entity.ItemDataBlock)
                                .Include(entity => entity.ListDataBlock)
                                
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

                    //queryable = queryable.Select(entity => new TagEntity()
                    //{
                    //    Id = entity.Id,
                    //    Order = entity.Order,
                    //    DeletedFlag = entity.DeletedFlag,
                    //    CreatedAt = entity.CreatedAt,
                    //    CreatedBy = entity.CreatedBy,
                    //    UpdatedAt = entity.UpdatedAt,
                    //    UpdatedBy = entity.UpdatedBy,
                    //                             Name = entity.Name,
//                             Type = entity.Type,
//                             DataBlockId = entity.DataBlockId,
//                             Offset = entity.Offset,
//                             ItemDataBlock = entity.ItemDataBlock,
//                             ListDataBlock = entity.ListDataBlock == null ? null : entity.ListDataBlock.Where(x => x.DeletedFlag != true).ToList(),
                            
                    //});
                    result = queryable.ToList();
                    foreach (var entity in result)
                    {
                        entity.ListDataBlock = entity.ListDataBlock == null ? null : entity.ListDataBlock.Where(x => x.DeletedFlag != true).ToList();
                            
                    }

                    this.AddEntityCacheKey(GetRepositoryName(), cacheKey, true);
                    this.SetRedisData(cacheKey, result, ConstCommon.ConstGetListCacheExpiredTimeInSeconds);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new DBException(ex);
            }
        }
        /// <summary>
        /// GetListFilter (@GenCRUD)
        /// </summary>
        /// <param name="pagingReq"></param>
        /// <returns></returns>
        /// <exception cref="DBException"></exception>
        public virtual (List<TagEntity> listTag, long totalRecord) GetListFilter(
            PagingFilterRequestModel request,
            bool isDirect = false,
            bool isTracking = false)
        {
            try
            {
                Dictionary<string, object> filterParams = StringToDictionaryHelper.ToStringAndObj(request.FilterStr);
                Dictionary<string, string> searchParams = StringToDictionaryHelper.ToDicOrString2(request.SearchStr, true);
                Dictionary<string, long> sortParams = StringToDictionaryHelper.ToStringLongTest(request.SortStr);

                List<TagEntity>? result = null;
                long totalRecord = 0;

                string cacheKey = $"{CacheKeyList}_ListFilter:";
                if (request != null)
                {
                    cacheKey += $"{request.GetKeyCache()}";
                }

                if (!isDirect && !isTracking)
                {
                    (result, totalRecord) = this.GetRedisData<(List<TagEntity>, long)>(cacheKey, (null, 0));
                }

                if (result == null)
                {
                    var dataSet = _context.Set<TagEntity>();
                    IQueryable<TagEntity> queryable;
                    IQueryable<TagEntity> queryableCountTotal;
                    if (!isTracking)
                    {
                        queryable = dataSet.AsNoTracking().AsQueryable();
                    }
                    else
                    {
                        queryable = dataSet.AsQueryable();
                    }
                    queryable = queryable
                                .Include(entity => entity.ItemDataBlock)
                                .Include(entity => entity.ListDataBlock)
                                
                                .Where(entity => entity.DeletedFlag != true)
                                ;

                    if (request.DateFrom != null)
                    {
                        queryable = queryable.Where(entity => entity.UpdatedAt != null && entity.UpdatedAt.Value >= request.DateFrom);
                    }
                    if (request.DateTo != null)
                    {
                        queryable = queryable.Where(entity => entity.UpdatedAt != null && entity.UpdatedAt.Value <= request.DateTo);
                    }

                    var responseModel = new TagResponseModel();

                    queryable = responseModel.PrepareWhereQueryFilter(
                        queryable,
                        filterParams,
                        responseModel.GetFieldAttributes,
                        responseModel.GetType());

                    queryable = responseModel.PrepareWhereQuerySearch(
                        queryable,
                        searchParams,
                        responseModel.GetFieldAttributes,
                        responseModel.GetFieldAttributesSearchAll,
                        responseModel.GetType());

                    queryableCountTotal = queryable;
                    totalRecord = queryableCountTotal.LongCount();

                    queryable = queryable.OrderBy(entity => entity.Order).ThenBy(entity => entity.Id);

                    queryable = responseModel.PrepareQuerySort(
                        queryable,
                        sortParams,
                        responseModel.GetFieldAttributes,
                        responseModel.GetType());

                    if (request != null)
                    {
                        queryable = queryable
                                .Skip(request.GetSkip()).Take(request.GetLimit());
                    }
                    else
                    {
                        queryable = queryable
                                .Skip(0).Take(ConstCommon.ConstSelectListMaxRecord);
                    }

                    //queryable = queryable.Select(entity => new TagEntity()
                    //{
                    //    Id = entity.Id,
                    //    Order = entity.Order,
                    //    DeletedFlag = entity.DeletedFlag,
                    //    CreatedAt = entity.CreatedAt,
                    //    CreatedBy = entity.CreatedBy,
                    //    UpdatedAt = entity.UpdatedAt,
                    //    UpdatedBy = entity.UpdatedBy,
                    //                             Name = entity.Name,
//                             Type = entity.Type,
//                             DataBlockId = entity.DataBlockId,
//                             Offset = entity.Offset,
//                             ItemDataBlock = entity.ItemDataBlock,
//                             ListDataBlock = entity.ListDataBlock == null ? null : entity.ListDataBlock.Where(x => x.DeletedFlag != true).ToList(),
                            
                    //});
                    result = queryable.ToList();
                    foreach (var entity in result)
                    {
                        entity.ListDataBlock = entity.ListDataBlock == null ? null : entity.ListDataBlock.Where(x => x.DeletedFlag != true).ToList();
                            
                    }

                    this.AddEntityCacheKey(GetRepositoryName(), cacheKey, true);
                    this.SetRedisData(cacheKey, (result, totalRecord), ConstCommon.ConstGetListCacheExpiredTimeInSeconds);
                }
                return (result, totalRecord);
            }
            catch (Exception ex)
            {
                throw new DBException(ex);
            }
        }
        public override List<TagEntity> GetListByListIds(List<long> Ids, bool isDirect = false, bool isTracking = false)
        {
            try
            {
                List<TagEntity>? result = null;
                string cacheKey = $"{CacheKeyListByListIds}:{EncodeUtil.MD5(string.Join(",", Ids))}";

                if (!isDirect && !isTracking)
                {
                    result = this.GetRedisData<List<TagEntity>>(cacheKey, null);
                }

                if (result == null)
                {
                    var dataSet = _context.Set<TagEntity>();
                    IQueryable<TagEntity> queryable;
                    if (!isTracking)
                    {
                        queryable = dataSet.AsNoTracking().AsQueryable();
                    }
                    else
                    {
                        queryable = dataSet.AsQueryable();
                    }
                    queryable = queryable
                                .Include(entity => entity.ItemDataBlock)
                                .Include(entity => entity.ListDataBlock)
                                
                                .Where(entity => entity.DeletedFlag != true && Ids.Contains(entity.Id))
                                .OrderBy(entity => entity.Order).ThenBy(entity => entity.Id);

                    //queryable = queryable.Select(entity => new TagEntity()
                    //{
                    //    Id = entity.Id,
                    //    Order = entity.Order,
                    //    DeletedFlag = entity.DeletedFlag,
                    //    CreatedAt = entity.CreatedAt,
                    //    CreatedBy = entity.CreatedBy,
                    //    UpdatedAt = entity.UpdatedAt,
                    //    UpdatedBy = entity.UpdatedBy,
                    //                             Name = entity.Name,
//                             Type = entity.Type,
//                             DataBlockId = entity.DataBlockId,
//                             Offset = entity.Offset,
//                             ItemDataBlock = entity.ItemDataBlock,
//                             ListDataBlock = entity.ListDataBlock == null ? null : entity.ListDataBlock.Where(x => x.DeletedFlag != true).ToList(),
                            
                    //});
                    result = queryable.ToList();
                    foreach (var entity in result)
                    {
                        entity.ListDataBlock = entity.ListDataBlock == null ? null : entity.ListDataBlock.Where(x => x.DeletedFlag != true).ToList();
                            
                    }

                    this.AddEntityCacheKey(GetRepositoryName(), cacheKey, true);
                    this.SetRedisData(cacheKey, result, ConstCommon.ConstGetListCacheExpiredTimeInSeconds);
                }
                return result;
            }
            catch (Exception dbEx)
            {
                throw new DBException(dbEx);
            }
        }
        public void Upsert(TagEntity entity, List<DataBlockEntity> dataBlockChildren, long? userId = null)
        {
            try
            {
                if (entity.Id <= 0)
                {
                    // Insert
                    Insert(entity, dataBlockChildren, userId);
                }
                else
                {
                    // Update
                    Update(entity, dataBlockChildren, userId);
                }
                return;
            }
            catch (Exception ex)
            {
                throw new DBException(ex);
            }
        }
        public void Insert(TagEntity entity, List<DataBlockEntity> dataBlockChildren, long? userId = null)
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
                    entity.CreatedAt = DateTime.UtcNow;
                    entity.UpdatedBy = entity.CreatedBy;
                    entity.UpdatedAt = entity.CreatedAt;
                    entity.DeletedFlag = false;
                    entity.ListDataBlock = MergeChildrenEntity(entity.ListDataBlock, dataBlockChildren);
                    
                    _context.Set<TagEntity>().Add(entity);
                }
                var result = _context.SaveChanges();
                ClearCachedData();
                return;
            }
            catch (Exception ex)
            {
                throw new DBException(ex);
            }
        }
        public void Update(TagEntity entity, List<DataBlockEntity> dataBlockChildren, long? userId = null)
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
                    entity.UpdatedAt = DateTime.UtcNow;
                    entity.ListDataBlock = MergeChildrenEntity(entity.ListDataBlock, dataBlockChildren);
                    
                    _context.Set<TagEntity>().Update(entity);
                }
                var result = _context.SaveChanges();
                ClearCachedData();
                return;
            }
            catch (Exception ex)
            {
                throw new DBException(ex);
            }
        }
    }
}
