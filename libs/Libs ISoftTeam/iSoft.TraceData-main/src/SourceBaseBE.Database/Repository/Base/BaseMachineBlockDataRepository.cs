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
    public class BaseMachineBlockDataRepository : BaseCRUDRepository<MachineBlockDataEntity>
    {
        public BaseMachineBlockDataRepository(CommonDBContext dbContext)
            : base(dbContext)
        {
        }
        public override string TableName
        {
            get { return "MachineBlockDatas"; }
        }
        public override string GetName()
        {
            return nameof(BaseMachineBlockDataRepository);
        }
        public override object GetDisplayField(MachineBlockDataEntity entity)
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
        public override MachineBlockDataEntity? GetById(long id, bool isTracking = false)
        {
            try
            {
                MachineBlockDataEntity? result = null;
                var dataSet = _context.Set<MachineBlockDataEntity>();
                IQueryable<MachineBlockDataEntity> queryable;
                if (!isTracking)
                {
                    queryable = dataSet.AsNoTracking().AsQueryable();
                }
                else
                {
                    queryable = dataSet.AsQueryable();
                }
                queryable = queryable
                      .Include(entity => entity.ItemMachineLoss)
                      .Include(entity => entity.ItemMachineLossPosition)
                      .Include(entity => entity.ItemMachineLossDescribe)
                      .Include(entity => entity.ItemOeePoint)
                      
                      .Where(entity => entity.DeletedFlag != true && entity.Id == id);

                if (!isTracking)
                {
                    queryable = queryable.Select(entity => new MachineBlockDataEntity()
                    {
                        Id = entity.Id,
                        Order = entity.Order,
                        DeletedFlag = entity.DeletedFlag,
                        CreatedAt = entity.CreatedAt,
                        CreatedBy = entity.CreatedBy,
                        UpdatedAt = entity.UpdatedAt,
                        UpdatedBy = entity.UpdatedBy,
                        LineId = entity.LineId,
                        MachineStatus = entity.MachineStatus,
                        StartDateTime = entity.StartDateTime,
                        EndDateTime = entity.EndDateTime,
                        DurationInMiliSeconds = entity.DurationInMiliSeconds,
                        BlockCountIn = entity.BlockCountIn,
                        BlockGoodCount = entity.BlockGoodCount,
                        BlockNGCount = entity.BlockNGCount,
                        LastCountIn = entity.LastCountIn,
                        LastGoodCount = entity.LastGoodCount,
                        LastNGCount = entity.LastNGCount,
                        LastMessageId = entity.LastMessageId,
                        LastReceivedTime = entity.LastReceivedTime,
                        MachineLossId = entity.MachineLossId,
                        ItemMachineLoss = entity.ItemMachineLoss,
                        MachineLossPositionId = entity.MachineLossPositionId,
                        ItemMachineLossPosition = entity.ItemMachineLossPosition,
                        MachineLossDescribeId = entity.MachineLossDescribeId,
                        ItemMachineLossDescribe = entity.ItemMachineLossDescribe,
                        OeePointId = entity.OeePointId,
                        ItemOeePoint = entity.ItemOeePoint,
                        
                    });
                    result = queryable.FirstOrDefault();
                }
                else
                {
                    var entity = queryable.FirstOrDefault();
                    if (entity != null)
                    {
                        
                        
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
        public override List<MachineBlockDataEntity> GetList(PagingRequestModel pagingReq = null, bool isDirect = false, bool isTracking = false)
        {
            try
            {
                List<MachineBlockDataEntity>? result = null;
                string cacheKey = $"{cacheKeyList}";
                if (pagingReq != null)
                {
                    cacheKey = $"{cacheKeyList}:{pagingReq.GetKeyCache()}";
                }
                if (!isDirect && !isTracking)
                {
                    result = this.GetRedisData<List<MachineBlockDataEntity>>(cacheKey, null);
                }

                if (result == null)
                {
                    var dataSet = _context.Set<MachineBlockDataEntity>();
                    IQueryable<MachineBlockDataEntity> queryable;
                    if (!isTracking)
                    {
                        queryable = dataSet.AsNoTracking().AsQueryable();
                    }
                    else
                    {
                        queryable = dataSet.AsQueryable();
                    }
                    queryable = queryable
                                .Include(entity => entity.ItemMachineLoss)
                                .Include(entity => entity.ItemMachineLossPosition)
                                .Include(entity => entity.ItemMachineLossDescribe)
                                .Include(entity => entity.ItemOeePoint)
                                
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
                        queryable = queryable.Select(entity => new MachineBlockDataEntity()
                        {
                            Id = entity.Id,
                            Order = entity.Order,
                            DeletedFlag = entity.DeletedFlag,
                            CreatedAt = entity.CreatedAt,
                            CreatedBy = entity.CreatedBy,
                            UpdatedAt = entity.UpdatedAt,
                            UpdatedBy = entity.UpdatedBy,
                            LineId = entity.LineId,
                            MachineStatus = entity.MachineStatus,
                            StartDateTime = entity.StartDateTime,
                            EndDateTime = entity.EndDateTime,
                            DurationInMiliSeconds = entity.DurationInMiliSeconds,
                            BlockCountIn = entity.BlockCountIn,
                            BlockGoodCount = entity.BlockGoodCount,
                            BlockNGCount = entity.BlockNGCount,
                            LastCountIn = entity.LastCountIn,
                            LastGoodCount = entity.LastGoodCount,
                            LastNGCount = entity.LastNGCount,
                            LastMessageId = entity.LastMessageId,
                            LastReceivedTime = entity.LastReceivedTime,
                            MachineLossId = entity.MachineLossId,
                            ItemMachineLoss = entity.ItemMachineLoss,
                            MachineLossPositionId = entity.MachineLossPositionId,
                            ItemMachineLossPosition = entity.ItemMachineLossPosition,
                            MachineLossDescribeId = entity.MachineLossDescribeId,
                            ItemMachineLossDescribe = entity.ItemMachineLossDescribe,
                            OeePointId = entity.OeePointId,
                            ItemOeePoint = entity.ItemOeePoint,
                            
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
        /// <summary>
        /// GetListFilter (@GenCRUD)
        /// </summary>
        /// <param name="pagingReq"></param>
        /// <returns></returns>
        /// <exception cref="DBException"></exception>
        public (List<MachineBlockDataEntity> listMachineBlockData, long totalRecord) GetListFilter(
            PagingFilterRequestModel request,
            bool isDirect = false,
            bool isTracking = false)
        {
            try
            {
                Dictionary<string, object> filterParams = StringToDictionaryHelper.ToStringAndObj(request.FilterStr);
                Dictionary<string, string> searchParams = StringToDictionaryHelper.ToDicOrString2(request.SearchStr, true);
                Dictionary<string, long> sortParams = StringToDictionaryHelper.ToStringLongTest(request.SortStr);

                List<MachineBlockDataEntity>? result = null;
                long totalRecord = 0;

                string cacheKey = $"{cacheKeyList}_ListFilter:";
                if (request != null)
                {
                    cacheKey += $"{request.GetKeyCache()}";
                }

                if (!isDirect && !isTracking)
                {
                    (result, totalRecord) = this.GetRedisData<(List<MachineBlockDataEntity>, long)>(cacheKey, (null, 0));
                }

                if (result == null)
                {
                    var dataSet = _context.Set<MachineBlockDataEntity>();
                    IQueryable<MachineBlockDataEntity> queryable;
                    IQueryable<MachineBlockDataEntity> queryableCountTotal;
                    if (!isTracking)
                    {
                        queryable = dataSet.AsNoTracking().AsQueryable();
                    }
                    else
                    {
                        queryable = dataSet.AsQueryable();
                    }
                    queryable = queryable
                                .Include(entity => entity.ItemMachineLoss)
                                .Include(entity => entity.ItemMachineLossPosition)
                                .Include(entity => entity.ItemMachineLossDescribe)
                                .Include(entity => entity.ItemOeePoint)
                                
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

                    var responseModel = new BaseMachineBlockDataResponseModel();

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
                        queryable = queryable.Select(entity => new MachineBlockDataEntity()
                        {
                            Id = entity.Id,
                            Order = entity.Order,
                            DeletedFlag = entity.DeletedFlag,
                            CreatedAt = entity.CreatedAt,
                            CreatedBy = entity.CreatedBy,
                            UpdatedAt = entity.UpdatedAt,
                            UpdatedBy = entity.UpdatedBy,
                            LineId = entity.LineId,
                            MachineStatus = entity.MachineStatus,
                            StartDateTime = entity.StartDateTime,
                            EndDateTime = entity.EndDateTime,
                            DurationInMiliSeconds = entity.DurationInMiliSeconds,
                            BlockCountIn = entity.BlockCountIn,
                            BlockGoodCount = entity.BlockGoodCount,
                            BlockNGCount = entity.BlockNGCount,
                            LastCountIn = entity.LastCountIn,
                            LastGoodCount = entity.LastGoodCount,
                            LastNGCount = entity.LastNGCount,
                            LastMessageId = entity.LastMessageId,
                            LastReceivedTime = entity.LastReceivedTime,
                            MachineLossId = entity.MachineLossId,
                            ItemMachineLoss = entity.ItemMachineLoss,
                            MachineLossPositionId = entity.MachineLossPositionId,
                            ItemMachineLossPosition = entity.ItemMachineLossPosition,
                            MachineLossDescribeId = entity.MachineLossDescribeId,
                            ItemMachineLossDescribe = entity.ItemMachineLossDescribe,
                            OeePointId = entity.OeePointId,
                            ItemOeePoint = entity.ItemOeePoint,
                            
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
                    this.SetRedisData(cacheKey, (result, totalRecord), ConstCommon.ConstGetListCacheExpiredTimeInSeconds);
                }
                return (result, totalRecord);
            }
            catch (Exception ex)
            {
                throw new DBException(ex);
            }
        }
        public override List<MachineBlockDataEntity> GetListByListIds(List<long> Ids, bool isDirect = false, bool isTracking = false)
        {
            try
            {
                List<MachineBlockDataEntity>? result = null;
                string cacheKey = $"{cacheKeyListByListIds}:{EncodeUtil.MD5(string.Join(",", Ids))}";

                if (!isDirect && !isTracking)
                {
                    result = this.GetRedisData<List<MachineBlockDataEntity>>(cacheKey, null);
                }

                if (result == null)
                {
                    var dataSet = _context.Set<MachineBlockDataEntity>();
                    IQueryable<MachineBlockDataEntity> queryable;
                    if (!isTracking)
                    {
                        queryable = dataSet.AsNoTracking().AsQueryable();
                    }
                    else
                    {
                        queryable = dataSet.AsQueryable();
                    }
                    queryable = queryable
                                .Include(entity => entity.ItemMachineLoss)
                                .Include(entity => entity.ItemMachineLossPosition)
                                .Include(entity => entity.ItemMachineLossDescribe)
                                .Include(entity => entity.ItemOeePoint)
                                
                                .Where(entity => entity.DeletedFlag != true && Ids.Contains(entity.Id))
                                .OrderBy(entity => entity.Order).ThenBy(entity => entity.Id);

                    if (!isTracking)
                    {
                        queryable = queryable.Select(entity => new MachineBlockDataEntity()
                        {
                            Id = entity.Id,
                            Order = entity.Order,
                            DeletedFlag = entity.DeletedFlag,
                            CreatedAt = entity.CreatedAt,
                            CreatedBy = entity.CreatedBy,
                            UpdatedAt = entity.UpdatedAt,
                            UpdatedBy = entity.UpdatedBy,
                            LineId = entity.LineId,
                            MachineStatus = entity.MachineStatus,
                            StartDateTime = entity.StartDateTime,
                            EndDateTime = entity.EndDateTime,
                            DurationInMiliSeconds = entity.DurationInMiliSeconds,
                            BlockCountIn = entity.BlockCountIn,
                            BlockGoodCount = entity.BlockGoodCount,
                            BlockNGCount = entity.BlockNGCount,
                            LastCountIn = entity.LastCountIn,
                            LastGoodCount = entity.LastGoodCount,
                            LastNGCount = entity.LastNGCount,
                            LastMessageId = entity.LastMessageId,
                            LastReceivedTime = entity.LastReceivedTime,
                            MachineLossId = entity.MachineLossId,
                            ItemMachineLoss = entity.ItemMachineLoss,
                            MachineLossPositionId = entity.MachineLossPositionId,
                            ItemMachineLossPosition = entity.ItemMachineLossPosition,
                            MachineLossDescribeId = entity.MachineLossDescribeId,
                            ItemMachineLossDescribe = entity.ItemMachineLossDescribe,
                            OeePointId = entity.OeePointId,
                            ItemOeePoint = entity.ItemOeePoint,
                            
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
            catch (Exception dbEx)
            {
                throw new DBException(dbEx);
            }
        }
        public void Upsert(MachineBlockDataEntity entity, long? userId = null)
        {
            try
            {
                if (entity.Id <= 0)
                {
                    // Insert
                    Insert(entity, userId);
                }
                else
                {
                    // Update
                    Update(entity, userId);
                }
                return;
            }
            catch (Exception ex)
            {
                throw new DBException(ex);
            }
        }
        public void Insert(MachineBlockDataEntity entity, long? userId = null)
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
                    
                    
                    _context.Set<MachineBlockDataEntity>().Add(entity);
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
        public void Update(MachineBlockDataEntity entity, long? userId = null)
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
                    
                    
                    _context.Set<MachineBlockDataEntity>().Update(entity);
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
