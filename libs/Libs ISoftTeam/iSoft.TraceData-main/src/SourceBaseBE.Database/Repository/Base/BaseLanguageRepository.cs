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
using UserEntity = SourceBaseBE.Database.Entities.UserEntity;
using ISoftProjectEntity = SourceBaseBE.Database.Entities.ISoftProjectEntity;
using iSoft.Common.Models.ConfigModel.Subs;
using iSoft.Common.Utils;

using iSoft.Common.Enums;
using System.Collections.Generic;
using SourceBaseBE.Database.Models.RequestModels;
using SourceBaseBE.Database.Models.ResponseModels;
using PRPO.Database.Helpers;
using iSoft.Common.ExtensionMethods;
using iSoft.Database.Entities;

namespace SourceBaseBE.Database.Repository
{
    public class BaseLanguageRepository : BaseCRUDRepository<LanguageEntity>
    {
        public BaseLanguageRepository(CommonDBContext dbContext)
            : base(dbContext)
        {
        }
        public override string TableName
        {
            get { return "Languages"; }
        }
        public override string GetName()
        {
            return nameof(BaseLanguageRepository);
        }
        public override object GetDisplayField(LanguageEntity entity)
        {
            return entity.Name;
            
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
        public override LanguageEntity? GetById(long id, bool isTracking = false)
        {
            try
            {
                LanguageEntity? result = null;
                var dataSet = _context.Set<LanguageEntity>();
                IQueryable<LanguageEntity> queryable;
                if (!isTracking)
                {
                    queryable = dataSet.AsNoTracking().AsQueryable();
                }
                else
                {
                    queryable = dataSet.AsQueryable();
                }
                queryable = queryable
                      .Include(entity => entity.ListTranslateLanguage)
                      .Include(entity => entity.ListCountry)
                      .Include(entity => entity.ListUser)
                      
                      .Where(entity => entity.DeletedFlag != true && entity.Id == id);

                if (!isTracking)
                {
                    queryable = queryable.Select(entity => new LanguageEntity()
                    {
                        Id = entity.Id,
                        Order = entity.Order,
                        DeletedFlag = entity.DeletedFlag,
                        CreatedAt = entity.CreatedAt,
                        CreatedBy = entity.CreatedBy,
                        UpdatedAt = entity.UpdatedAt,
                        UpdatedBy = entity.UpdatedBy,
                        Key = entity.Key,
                        Name = entity.Name,
                        LanguageCode = entity.LanguageCode,
                        ListTranslateLanguage = entity.ListTranslateLanguage == null ? null : entity.ListTranslateLanguage.Where(x => x.DeletedFlag != true).ToList(),
                        ListCountry = entity.ListCountry == null ? null : entity.ListCountry.Where(x => x.DeletedFlag != true).ToList(),
                        ListUser = entity.ListUser == null ? null : entity.ListUser.Where(x => x.DeletedFlag != true).ToList(),
                        
                    });
                    result = queryable.FirstOrDefault();
                }
                else
                {
                    var entity = queryable.FirstOrDefault();
                    if (entity != null)
                    {
                        entity.ListTranslateLanguage = entity.ListTranslateLanguage == null ? null : entity.ListTranslateLanguage.Where(x => x.DeletedFlag != true).ToList();
                        entity.ListCountry = entity.ListCountry == null ? null : entity.ListCountry.Where(x => x.DeletedFlag != true).ToList();
                        entity.ListUser = entity.ListUser == null ? null : entity.ListUser.Where(x => x.DeletedFlag != true).ToList();
                        
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
        /// <summary>
        /// GetList (@GenCRUD)
        /// </summary>
        /// <param name="pagingReq"></param>
        /// <returns></returns>
        /// <exception cref="DBException"></exception>
        public override List<LanguageEntity> GetList(PagingRequestModel pagingReq = null, bool isDirect = false, bool isTracking = false)
        {
            try
            {
                List<LanguageEntity>? result = null;
                string cacheKey = $"{cacheKeyList}";
                if (pagingReq != null)
                {
                    cacheKey = $"{cacheKeyList}:{pagingReq.GetKeyCache()}";
                }
                if (!isDirect && !isTracking)
                {
                    result = this.GetRedisData<List<LanguageEntity>>(cacheKey, null);
                }

                if (result == null)
                {
                    var dataSet = _context.Set<LanguageEntity>();
                    IQueryable<LanguageEntity> queryable;
                    if (!isTracking)
                    {
                        queryable = dataSet.AsNoTracking().AsQueryable();
                    }
                    else
                    {
                        queryable = dataSet.AsQueryable();
                    }
                    queryable = queryable
                                .Include(entity => entity.ListTranslateLanguage)
                                .Include(entity => entity.ListCountry)
                                .Include(entity => entity.ListUser)
                                
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
                        queryable = queryable.Select(entity => new LanguageEntity()
                        {
                            Id = entity.Id,
                            Order = entity.Order,
                            DeletedFlag = entity.DeletedFlag,
                            CreatedAt = entity.CreatedAt,
                            CreatedBy = entity.CreatedBy,
                            UpdatedAt = entity.UpdatedAt,
                            UpdatedBy = entity.UpdatedBy,
                            Key = entity.Key,
                            Name = entity.Name,
                            LanguageCode = entity.LanguageCode,
                            ListTranslateLanguage = entity.ListTranslateLanguage == null ? null : entity.ListTranslateLanguage.Where(x => x.DeletedFlag != true).ToList(),
                            ListCountry = entity.ListCountry == null ? null : entity.ListCountry.Where(x => x.DeletedFlag != true).ToList(),
                            ListUser = entity.ListUser == null ? null : entity.ListUser.Where(x => x.DeletedFlag != true).ToList(),
                            
                        });
                        result = queryable.ToList();
                    }
                    else
                    {
                        result = queryable.ToList();
                        foreach (var entity in result)
                        {
                            entity.ListTranslateLanguage = entity.ListTranslateLanguage == null ? null : entity.ListTranslateLanguage.Where(x => x.DeletedFlag != true).ToList();
                            entity.ListCountry = entity.ListCountry == null ? null : entity.ListCountry.Where(x => x.DeletedFlag != true).ToList();
                            entity.ListUser = entity.ListUser == null ? null : entity.ListUser.Where(x => x.DeletedFlag != true).ToList();
                            
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
        /// <summary>
        /// GetListFilter (@GenCRUD)
        /// </summary>
        /// <param name="pagingReq"></param>
        /// <returns></returns>
        /// <exception cref="DBException"></exception>
        public (List<LanguageEntity> listLanguage, long totalRecord) GetListFilter(
            PagingFilterRequestModel request,
            bool isDirect = false,
            bool isTracking = false)
        {
            try
            {
                Dictionary<string, object> filterParams = StringToDictionaryHelper.ToStringAndObj(request.FilterStr);
                Dictionary<string, string> searchParams = StringToDictionaryHelper.ToDicOrString2(request.SearchStr, true);
                Dictionary<string, long> sortParams = StringToDictionaryHelper.ToStringLongTest(request.SortStr);

                List<LanguageEntity>? result = null;
                long totalRecord = 0;

                string cacheKey = $"{cacheKeyList}_ListFilter:";
                if (request != null)
                {
                    cacheKey += $"{request.GetKeyCache()}";
                }

                if (!isDirect && !isTracking)
                {
                    (result, totalRecord) = this.GetRedisData<(List<LanguageEntity>, long)>(cacheKey, (null, 0));
                }

                if (result == null)
                {
                    var dataSet = _context.Set<LanguageEntity>();
                    IQueryable<LanguageEntity> queryable;
                    IQueryable<LanguageEntity> queryableCountTotal;
                    if (!isTracking)
                    {
                        queryable = dataSet.AsNoTracking().AsQueryable();
                    }
                    else
                    {
                        queryable = dataSet.AsQueryable();
                    }
                    queryable = queryable
                                .Include(entity => entity.ListTranslateLanguage)
                                .Include(entity => entity.ListCountry)
                                .Include(entity => entity.ListUser)
                                
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

                    var responseModel = new BaseLanguageResponseModel();

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

                    if (!isTracking)
                    {
                        queryable = queryable.Select(entity => new LanguageEntity()
                        {
                            Id = entity.Id,
                            Order = entity.Order,
                            DeletedFlag = entity.DeletedFlag,
                            CreatedAt = entity.CreatedAt,
                            CreatedBy = entity.CreatedBy,
                            UpdatedAt = entity.UpdatedAt,
                            UpdatedBy = entity.UpdatedBy,
                            Key = entity.Key,
                            Name = entity.Name,
                            LanguageCode = entity.LanguageCode,
                            ListTranslateLanguage = entity.ListTranslateLanguage == null ? null : entity.ListTranslateLanguage.Where(x => x.DeletedFlag != true).ToList(),
                            ListCountry = entity.ListCountry == null ? null : entity.ListCountry.Where(x => x.DeletedFlag != true).ToList(),
                            ListUser = entity.ListUser == null ? null : entity.ListUser.Where(x => x.DeletedFlag != true).ToList(),
                            
                        });

                        result = queryable.ToList();
                    }
                    else
                    {
                        result = queryable.ToList();
                        foreach (var entity in result)
                        {
                            entity.ListTranslateLanguage = entity.ListTranslateLanguage == null ? null : entity.ListTranslateLanguage.Where(x => x.DeletedFlag != true).ToList();
                            entity.ListCountry = entity.ListCountry == null ? null : entity.ListCountry.Where(x => x.DeletedFlag != true).ToList();
                            entity.ListUser = entity.ListUser == null ? null : entity.ListUser.Where(x => x.DeletedFlag != true).ToList();
                            
                        }
                    }

                    this.AddEntityCacheKey(GetName(), cacheKey, true);
                    this.SetRedisData(cacheKey, (result, totalRecord), ConstCommon.ConstGetListCacheExpiredTimeInSeconds);
                }
                return (result, totalRecord);
            }
            catch (Exception ex)
            {
                throw new DBException(ex);
            }
        }
        public override List<LanguageEntity> GetListByListIds(List<long> Ids, bool isDirect = false, bool isTracking = false)
        {
            try
            {
                List<LanguageEntity>? result = null;
                string cacheKey = $"{cacheKeyListByListIds}:{EncodeUtil.MD5(string.Join(",", Ids))}";
                if (!isDirect && !isTracking)
                {
                    result = this.GetRedisData<List<LanguageEntity>>(cacheKey, null);
                }

                if (result == null)
                {
                    var dataSet = _context.Set<LanguageEntity>();
                    IQueryable<LanguageEntity> queryable;
                    if (!isTracking)
                    {
                        queryable = dataSet.AsNoTracking().AsQueryable();
                    }
                    else
                    {
                        queryable = dataSet.AsQueryable();
                    }
                    queryable = queryable
                                .Include(entity => entity.ListTranslateLanguage)
                                .Include(entity => entity.ListCountry)
                                .Include(entity => entity.ListUser)
                                
                                .Where(entity => entity.DeletedFlag != true && Ids.Contains(entity.Id))
                                .OrderBy(entity => entity.Order).ThenBy(entity => entity.Id);

                    if (!isTracking)
                    {
                        queryable = queryable.Select(entity => new LanguageEntity()
                        {
                            Id = entity.Id,
                            Order = entity.Order,
                            DeletedFlag = entity.DeletedFlag,
                            CreatedAt = entity.CreatedAt,
                            CreatedBy = entity.CreatedBy,
                            UpdatedAt = entity.UpdatedAt,
                            UpdatedBy = entity.UpdatedBy,
                            Key = entity.Key,
                            Name = entity.Name,
                            LanguageCode = entity.LanguageCode,
                            ListTranslateLanguage = entity.ListTranslateLanguage == null ? null : entity.ListTranslateLanguage.Where(x => x.DeletedFlag != true).ToList(),
                            ListCountry = entity.ListCountry == null ? null : entity.ListCountry.Where(x => x.DeletedFlag != true).ToList(),
                            ListUser = entity.ListUser == null ? null : entity.ListUser.Where(x => x.DeletedFlag != true).ToList(),
                            
                        });
                        result = queryable.ToList();
                    }
                    else
                    {
                        result = queryable.ToList();
                        foreach (var entity in result)
                        {
                            entity.ListTranslateLanguage = entity.ListTranslateLanguage == null ? null : entity.ListTranslateLanguage.Where(x => x.DeletedFlag != true).ToList();
                            entity.ListCountry = entity.ListCountry == null ? null : entity.ListCountry.Where(x => x.DeletedFlag != true).ToList();
                            entity.ListUser = entity.ListUser == null ? null : entity.ListUser.Where(x => x.DeletedFlag != true).ToList();
                            
                        }
                    }

                    this.AddEntityCacheKey(GetName(), cacheKey, true);
                    this.SetRedisData(cacheKey, result, ConstCommon.ConstGetListCacheExpiredTimeInSeconds);
                }
                return result;
            }
            catch (Exception dbEx)
            {
                throw new DBException(dbEx);
            }
        }
        public void Upsert(LanguageEntity entity, List<TranslateLanguageEntity> translateLanguageChildren, List<CountryEntity> countryChildren, List<UserEntity> userChildren, long? userId = null)
        {
            try
            {
                if (entity.Id <= 0)
                {
                    // Insert
                    Insert(entity, translateLanguageChildren, countryChildren, userChildren, userId);
                }
                else
                {
                    // Update
                    Update(entity, translateLanguageChildren, countryChildren, userChildren, userId);
                }
                return;
            }
            catch (Exception ex)
            {
                throw new DBException(ex);
            }
        }
        public void Insert(LanguageEntity entity, List<TranslateLanguageEntity> translateLanguageChildren, List<CountryEntity> countryChildren, List<UserEntity> userChildren, long? userId = null)
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
                    entity.ListTranslateLanguage = MergeChildrenEntity(entity.ListTranslateLanguage, translateLanguageChildren);

                    entity.ListCountry = MergeChildrenEntity(entity.ListCountry, countryChildren);

                    entity.ListUser = MergeChildrenEntity(entity.ListUser, userChildren);
                    
                    _context.Set<LanguageEntity>().Add(entity);
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
        public void Update(LanguageEntity entity, List<TranslateLanguageEntity> translateLanguageChildren, List<CountryEntity> countryChildren, List<UserEntity> userChildren, long? userId = null)
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
                    entity.ListTranslateLanguage = MergeChildrenEntity(entity.ListTranslateLanguage, translateLanguageChildren);

                    entity.ListCountry = MergeChildrenEntity(entity.ListCountry, countryChildren);

                    entity.ListUser = MergeChildrenEntity(entity.ListUser, userChildren);
                    
                    _context.Set<LanguageEntity>().Update(entity);
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
