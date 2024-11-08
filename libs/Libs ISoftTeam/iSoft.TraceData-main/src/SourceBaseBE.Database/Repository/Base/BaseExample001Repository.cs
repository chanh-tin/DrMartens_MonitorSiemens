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
    public class BaseExample001Repository : BaseCRUDRepository<Example001Entity>
    {
        public BaseExample001Repository(CommonDBContext dbContext)
            : base(dbContext)
        {
        }
        public override string TableName
        {
            get { return "Example001s"; }
        }
        public override string GetName()
        {
            return nameof(BaseExample001Repository);
        }
        public override object GetDisplayField(Example001Entity entity)
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
        public override Example001Entity? GetById(long id, bool isTracking = false)
        {
            try
            {
                Example001Entity? result = null;
                var dataSet = _context.Set<Example001Entity>();
                IQueryable<Example001Entity> queryable;
                if (!isTracking)
                {
                    queryable = dataSet.AsNoTracking().AsQueryable();
                }
                else
                {
                    queryable = dataSet.AsQueryable();
                }
                queryable = queryable
                      .Include(entity => entity.ItemExample002)
                      .Include(entity => entity.ListExample003)
                      
                      .Where(entity => entity.DeletedFlag != true && entity.Id == id);

                if (!isTracking)
                {
                    queryable = queryable.Select(entity => new Example001Entity()
                    {
                        Id = entity.Id,
                        Order = entity.Order,
                        DeletedFlag = entity.DeletedFlag,
                        CreatedAt = entity.CreatedAt,
                        CreatedBy = entity.CreatedBy,
                        UpdatedAt = entity.UpdatedAt,
                        UpdatedBy = entity.UpdatedBy,
                        Name = entity.Name,
                        NameReadonly = entity.NameReadonly,
                        Username = entity.Username,
                        Description = entity.Description,
                        Label1 = entity.Label1,
                        Password1 = entity.Password1,
                        Password2 = entity.Password2,
                        Email1 = entity.Email1,
                        PhoneNumber1 = entity.PhoneNumber1,
                        StartDate = entity.StartDate,
                        StartDateTime = entity.StartDateTime,
                        TimeOnlyData = entity.TimeOnlyData,
                        RefreshTime1 = entity.RefreshTime1,
                        RefreshTime2 = entity.RefreshTime2,
                        RefreshTime3 = entity.RefreshTime3,
                        RefreshTime4 = entity.RefreshTime4,
                        RefreshTime5 = entity.RefreshTime5,
                        Price = entity.Price,
                        Gender = entity.Gender,
                        Enable = entity.Enable,
                        CheckBoxValues = entity.CheckBoxValues,
                        Avatar = entity.Avatar,
                        ListImage1 = entity.ListImage1,
                        File1 = entity.File1,
                        ListFile1 = entity.ListFile1,
                        Example002Id = entity.Example002Id,
                        ItemExample002 = entity.ItemExample002,
                        ListExample003 = entity.ListExample003 == null ? null : entity.ListExample003.Where(x => x.DeletedFlag != true).ToList(),
                        
                    });
                    result = queryable.FirstOrDefault();
                }
                else
                {
                    var entity = queryable.FirstOrDefault();
                    if (entity != null)
                    {
                        entity.ListExample003 = entity.ListExample003 == null ? null : entity.ListExample003.Where(x => x.DeletedFlag != true).ToList();
                        
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
        public override List<Example001Entity> GetList(PagingRequestModel pagingReq = null, bool isDirect = false, bool isTracking = false)
        {
            try
            {
                List<Example001Entity>? result = null;
                string cacheKey = $"{cacheKeyList}";
                if (pagingReq != null)
                {
                    cacheKey = $"{cacheKeyList}:{pagingReq.GetKeyCache()}";
                }
                if (!isDirect && !isTracking)
                {
                    result = this.GetRedisData<List<Example001Entity>>(cacheKey, null);
                }

                if (result == null)
                {
                    var dataSet = _context.Set<Example001Entity>();
                    IQueryable<Example001Entity> queryable;
                    if (!isTracking)
                    {
                        queryable = dataSet.AsNoTracking().AsQueryable();
                    }
                    else
                    {
                        queryable = dataSet.AsQueryable();
                    }
                    queryable = queryable
                                .Include(entity => entity.ItemExample002)
                                .Include(entity => entity.ListExample003)
                                
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
                        queryable = queryable.Select(entity => new Example001Entity()
                        {
                            Id = entity.Id,
                            Order = entity.Order,
                            DeletedFlag = entity.DeletedFlag,
                            CreatedAt = entity.CreatedAt,
                            CreatedBy = entity.CreatedBy,
                            UpdatedAt = entity.UpdatedAt,
                            UpdatedBy = entity.UpdatedBy,
                            Name = entity.Name,
                            NameReadonly = entity.NameReadonly,
                            Username = entity.Username,
                            Description = entity.Description,
                            Label1 = entity.Label1,
                            Password1 = entity.Password1,
                            Password2 = entity.Password2,
                            Email1 = entity.Email1,
                            PhoneNumber1 = entity.PhoneNumber1,
                            StartDate = entity.StartDate,
                            StartDateTime = entity.StartDateTime,
                            TimeOnlyData = entity.TimeOnlyData,
                            RefreshTime1 = entity.RefreshTime1,
                            RefreshTime2 = entity.RefreshTime2,
                            RefreshTime3 = entity.RefreshTime3,
                            RefreshTime4 = entity.RefreshTime4,
                            RefreshTime5 = entity.RefreshTime5,
                            Price = entity.Price,
                            Gender = entity.Gender,
                            Enable = entity.Enable,
                            CheckBoxValues = entity.CheckBoxValues,
                            Avatar = entity.Avatar,
                            ListImage1 = entity.ListImage1,
                            File1 = entity.File1,
                            ListFile1 = entity.ListFile1,
                            Example002Id = entity.Example002Id,
                            ItemExample002 = entity.ItemExample002,
                            ListExample003 = entity.ListExample003 == null ? null : entity.ListExample003.Where(x => x.DeletedFlag != true).ToList(),
                            
                        });
                        result = queryable.ToList();
                    }
                    else
                    {
                        result = queryable.ToList();
                        foreach (var entity in result)
                        {
                            entity.ListExample003 = entity.ListExample003 == null ? null : entity.ListExample003.Where(x => x.DeletedFlag != true).ToList();
                            
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
        public (List<Example001Entity> listExample001, long totalRecord) GetListFilter(
            PagingFilterRequestModel request,
            bool isDirect = false,
            bool isTracking = false)
        {
            try
            {
                Dictionary<string, object> filterParams = StringToDictionaryHelper.ToStringAndObj(request.FilterStr);
                Dictionary<string, string> searchParams = StringToDictionaryHelper.ToDicOrString2(request.SearchStr, true);
                Dictionary<string, long> sortParams = StringToDictionaryHelper.ToStringLongTest(request.SortStr);

                List<Example001Entity>? result = null;
                long totalRecord = 0;

                string cacheKey = $"{cacheKeyList}_ListFilter:";
                if (request != null)
                {
                    cacheKey += $"{request.GetKeyCache()}";
                }

                if (!isDirect && !isTracking)
                {
                    (result, totalRecord) = this.GetRedisData<(List<Example001Entity>, long)>(cacheKey, (null, 0));
                }

                if (result == null)
                {
                    var dataSet = _context.Set<Example001Entity>();
                    IQueryable<Example001Entity> queryable;
                    IQueryable<Example001Entity> queryableCountTotal;
                    if (!isTracking)
                    {
                        queryable = dataSet.AsNoTracking().AsQueryable();
                    }
                    else
                    {
                        queryable = dataSet.AsQueryable();
                    }
                    queryable = queryable
                                .Include(entity => entity.ItemExample002)
                                .Include(entity => entity.ListExample003)
                                
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

                    var responseModel = new BaseExample001ResponseModel();

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
                        queryable = queryable.Select(entity => new Example001Entity()
                        {
                            Id = entity.Id,
                            Order = entity.Order,
                            DeletedFlag = entity.DeletedFlag,
                            CreatedAt = entity.CreatedAt,
                            CreatedBy = entity.CreatedBy,
                            UpdatedAt = entity.UpdatedAt,
                            UpdatedBy = entity.UpdatedBy,
                            Name = entity.Name,
                            NameReadonly = entity.NameReadonly,
                            Username = entity.Username,
                            Description = entity.Description,
                            Label1 = entity.Label1,
                            Password1 = entity.Password1,
                            Password2 = entity.Password2,
                            Email1 = entity.Email1,
                            PhoneNumber1 = entity.PhoneNumber1,
                            StartDate = entity.StartDate,
                            StartDateTime = entity.StartDateTime,
                            TimeOnlyData = entity.TimeOnlyData,
                            RefreshTime1 = entity.RefreshTime1,
                            RefreshTime2 = entity.RefreshTime2,
                            RefreshTime3 = entity.RefreshTime3,
                            RefreshTime4 = entity.RefreshTime4,
                            RefreshTime5 = entity.RefreshTime5,
                            Price = entity.Price,
                            Gender = entity.Gender,
                            Enable = entity.Enable,
                            CheckBoxValues = entity.CheckBoxValues,
                            Avatar = entity.Avatar,
                            ListImage1 = entity.ListImage1,
                            File1 = entity.File1,
                            ListFile1 = entity.ListFile1,
                            Example002Id = entity.Example002Id,
                            ItemExample002 = entity.ItemExample002,
                            ListExample003 = entity.ListExample003 == null ? null : entity.ListExample003.Where(x => x.DeletedFlag != true).ToList(),
                            
                        });

                        result = queryable.ToList();
                    }
                    else
                    {
                        result = queryable.ToList();
                        foreach (var entity in result)
                        {
                            entity.ListExample003 = entity.ListExample003 == null ? null : entity.ListExample003.Where(x => x.DeletedFlag != true).ToList();
                            
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
        public override List<Example001Entity> GetListByListIds(List<long> Ids, bool isDirect = false, bool isTracking = false)
        {
            try
            {
                List<Example001Entity>? result = null;
                string cacheKey = $"{cacheKeyListByListIds}:{EncodeUtil.MD5(string.Join(",", Ids))}";
                if (!isDirect && !isTracking)
                {
                    result = this.GetRedisData<List<Example001Entity>>(cacheKey, null);
                }

                if (result == null)
                {
                    var dataSet = _context.Set<Example001Entity>();
                    IQueryable<Example001Entity> queryable;
                    if (!isTracking)
                    {
                        queryable = dataSet.AsNoTracking().AsQueryable();
                    }
                    else
                    {
                        queryable = dataSet.AsQueryable();
                    }
                    queryable = queryable
                                .Include(entity => entity.ItemExample002)
                                .Include(entity => entity.ListExample003)
                                
                                .Where(entity => entity.DeletedFlag != true && Ids.Contains(entity.Id))
                                .OrderBy(entity => entity.Order).ThenBy(entity => entity.Id);

                    if (!isTracking)
                    {
                        queryable = queryable.Select(entity => new Example001Entity()
                        {
                            Id = entity.Id,
                            Order = entity.Order,
                            DeletedFlag = entity.DeletedFlag,
                            CreatedAt = entity.CreatedAt,
                            CreatedBy = entity.CreatedBy,
                            UpdatedAt = entity.UpdatedAt,
                            UpdatedBy = entity.UpdatedBy,
                            Name = entity.Name,
                            NameReadonly = entity.NameReadonly,
                            Username = entity.Username,
                            Description = entity.Description,
                            Label1 = entity.Label1,
                            Password1 = entity.Password1,
                            Password2 = entity.Password2,
                            Email1 = entity.Email1,
                            PhoneNumber1 = entity.PhoneNumber1,
                            StartDate = entity.StartDate,
                            StartDateTime = entity.StartDateTime,
                            TimeOnlyData = entity.TimeOnlyData,
                            RefreshTime1 = entity.RefreshTime1,
                            RefreshTime2 = entity.RefreshTime2,
                            RefreshTime3 = entity.RefreshTime3,
                            RefreshTime4 = entity.RefreshTime4,
                            RefreshTime5 = entity.RefreshTime5,
                            Price = entity.Price,
                            Gender = entity.Gender,
                            Enable = entity.Enable,
                            CheckBoxValues = entity.CheckBoxValues,
                            Avatar = entity.Avatar,
                            ListImage1 = entity.ListImage1,
                            File1 = entity.File1,
                            ListFile1 = entity.ListFile1,
                            Example002Id = entity.Example002Id,
                            ItemExample002 = entity.ItemExample002,
                            ListExample003 = entity.ListExample003 == null ? null : entity.ListExample003.Where(x => x.DeletedFlag != true).ToList(),
                            
                        });
                        result = queryable.ToList();
                    }
                    else
                    {
                        result = queryable.ToList();
                        foreach (var entity in result)
                        {
                            entity.ListExample003 = entity.ListExample003 == null ? null : entity.ListExample003.Where(x => x.DeletedFlag != true).ToList();
                            
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
        public void Upsert(Example001Entity entity, List<Example003Entity> example003Children, long? userId = null)
        {
            try
            {
                if (entity.Id <= 0)
                {
                    // Insert
                    Insert(entity, example003Children, userId);
                }
                else
                {
                    // Update
                    Update(entity, example003Children, userId);
                }
                return;
            }
            catch (Exception ex)
            {
                throw new DBException(ex);
            }
        }
        public void Insert(Example001Entity entity, List<Example003Entity> example003Children, long? userId = null)
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
                    entity.ListExample003 = MergeChildrenEntity(entity.ListExample003, example003Children);
                    
                    _context.Set<Example001Entity>().Add(entity);
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
        public void Update(Example001Entity entity, List<Example003Entity> example003Children, long? userId = null)
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
                    entity.ListExample003 = MergeChildrenEntity(entity.ListExample003, example003Children);
                    
                    _context.Set<Example001Entity>().Update(entity);
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
