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

using PRPO.Database.Helpers;
using SourceBaseBE.Database.Models.ResponseModels;
using SourceBaseBE.Database.Models.RequestModels;

using SourceBaseBE.Database.Enums;

namespace SourceBaseBE.Database.Repository
{
    public class OeePointRepository : BaseOeePointRepository
    {
        public OeePointRepository(CommonDBContext dbContext)
            : base(dbContext)
        {
        }


        /// <summary>
        /// GetById (@GenCRUD)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="DBException"></exception>
        public override OeePointEntity? GetById(long id, bool isTracking = false)
        {
            try
            {
                OeePointEntity? result = null;
                var dataSet = _context.Set<OeePointEntity>();
                IQueryable<OeePointEntity> queryable;
                if (!isTracking)
                {
                    queryable = dataSet.AsNoTracking().AsQueryable();
                }
                else
                {
                    queryable = dataSet.AsQueryable();
                }
                queryable = queryable
                      .Include(entity => entity.ItemOeePointConfig)
                      //.Include(entity => entity.ListMachineBlockData)
                      //.Include(entity => entity.ItemLine)
                      .Include(entity => entity.ListMachine)

                      .Where(entity => entity.DeletedFlag != true && entity.Id == id);

                if (!isTracking)
                {
                    queryable = queryable.Select(entity => new OeePointEntity()
                    {
                        Id = entity.Id,
                        Order = entity.Order,
                        DeletedFlag = entity.DeletedFlag,
                        CreatedAt = entity.CreatedAt,
                        CreatedBy = entity.CreatedBy,
                        UpdatedAt = entity.UpdatedAt,
                        UpdatedBy = entity.UpdatedBy,
                        Name = entity.Name,
                        Note = entity.Note,
                        Description = entity.Description,
                        TagNames = entity.TagNames,
                        IdealRunRate = entity.IdealRunRate,
                        IdealCycleTime = entity.IdealCycleTime,
                        OeePointConfigId = entity.OeePointConfigId,
                        ItemOeePointConfig = entity.ItemOeePointConfig,
                        //ListMachineBlockData = entity.ListMachineBlockData == null ? null : entity.ListMachineBlockData.Where(x => x.DeletedFlag != true).ToList(),
                        LineId = entity.LineId,
                        //ItemLine = entity.ItemLine,
                        ListMachine = entity.ListMachine == null ? null : entity.ListMachine.Where(x => x.DeletedFlag != true).ToList(),

                    });
                    result = queryable.FirstOrDefault();
                }
                else
                {
                    var entity = queryable.FirstOrDefault();
                    if (entity != null)
                    {
                        entity.ListMachineBlockData = entity.ListMachineBlockData == null ? null : entity.ListMachineBlockData.Where(x => x.DeletedFlag != true).ToList();
                        entity.ListMachine = entity.ListMachine == null ? null : entity.ListMachine.Where(x => x.DeletedFlag != true).ToList();

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
        public override List<OeePointEntity> GetList(PagingRequestModel pagingReq = null, bool isDirect = false, bool isTracking = false)
        {
            try
            {
                List<OeePointEntity>? result = null;
                string cacheKey = $"{cacheKeyList}";
                if (pagingReq != null)
                {
                    cacheKey = $"{cacheKeyList}:{pagingReq.GetKeyCache()}";
                }
                if (!isDirect && !isTracking)
                {
                    result = CachedFunc.GetRedisData<List<OeePointEntity>>(cacheKey, null);
                }

                if (result == null)
                {
                    var dataSet = _context.Set<OeePointEntity>();
                    IQueryable<OeePointEntity> queryable;
                    if (!isTracking)
                    {
                        queryable = dataSet.AsNoTracking().AsQueryable();
                    }
                    else
                    {
                        queryable = dataSet.AsQueryable();
                    }
                    queryable = queryable
                                .Include(entity => entity.ItemOeePointConfig)
                                //.Include(entity => entity.ListMachineBlockData)
                                //.Include(entity => entity.ItemLine)
                                .Include(entity => entity.ListMachine)

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
                        queryable = queryable.Select(entity => new OeePointEntity()
                        {
                            Id = entity.Id,
                            Order = entity.Order,
                            DeletedFlag = entity.DeletedFlag,
                            CreatedAt = entity.CreatedAt,
                            CreatedBy = entity.CreatedBy,
                            UpdatedAt = entity.UpdatedAt,
                            UpdatedBy = entity.UpdatedBy,
                            Name = entity.Name,
                            Note = entity.Note,
                            Description = entity.Description,
                            TagNames = entity.TagNames,
                            IdealRunRate = entity.IdealRunRate,
                            IdealCycleTime = entity.IdealCycleTime,
                            OeePointConfigId = entity.OeePointConfigId,
                            ItemOeePointConfig = entity.ItemOeePointConfig,
                            //ListMachineBlockData = entity.ListMachineBlockData == null ? null : entity.ListMachineBlockData.Where(x => x.DeletedFlag != true).ToList(),
                            LineId = entity.LineId,
                            //ItemLine = entity.ItemLine,
                            ListMachine = entity.ListMachine == null ? null : entity.ListMachine.Where(x => x.DeletedFlag != true).ToList(),

                        });
                        result = queryable.ToList();
                    }
                    else
                    {
                        result = queryable.ToList();
                        foreach (var entity in result)
                        {
                            entity.ListMachineBlockData = entity.ListMachineBlockData == null ? null : entity.ListMachineBlockData.Where(x => x.DeletedFlag != true).ToList();
                            entity.ListMachine = entity.ListMachine == null ? null : entity.ListMachine.Where(x => x.DeletedFlag != true).ToList();

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
        public override List<OeePointEntity> GetListByListIds(List<long> Ids, bool isDirect = false, bool isTracking = false)
        {
            try
            {
                List<OeePointEntity>? result = null;
                string cacheKey = $"{cacheKeyListByListIds}:{EncodeUtil.MD5(string.Join(",", Ids))}";
                if (!isDirect && !isTracking)
                {
                    result = CachedFunc.GetRedisData<List<OeePointEntity>>(cacheKey, null);
                }

                if (result == null)
                {
                    var dataSet = _context.Set<OeePointEntity>();
                    IQueryable<OeePointEntity> queryable;
                    if (!isTracking)
                    {
                        queryable = dataSet.AsNoTracking().AsQueryable();
                    }
                    else
                    {
                        queryable = dataSet.AsQueryable();
                    }
                    queryable = queryable
                                .Include(entity => entity.ItemOeePointConfig)
                                //.Include(entity => entity.ListMachineBlockData)
                                //.Include(entity => entity.ItemLine)
                                .Include(entity => entity.ListMachine)

                                .Where(entity => entity.DeletedFlag != true && Ids.Contains(entity.Id))
                                .OrderBy(entity => entity.Order).ThenBy(entity => entity.Id);

                    if (!isTracking)
                    {
                        queryable = queryable.Select(entity => new OeePointEntity()
                        {
                            Id = entity.Id,
                            Order = entity.Order,
                            DeletedFlag = entity.DeletedFlag,
                            CreatedAt = entity.CreatedAt,
                            CreatedBy = entity.CreatedBy,
                            UpdatedAt = entity.UpdatedAt,
                            UpdatedBy = entity.UpdatedBy,
                            Name = entity.Name,
                            Note = entity.Note,
                            Description = entity.Description,
                            TagNames = entity.TagNames,
                            IdealRunRate = entity.IdealRunRate,
                            IdealCycleTime = entity.IdealCycleTime,
                            OeePointConfigId = entity.OeePointConfigId,
                            ItemOeePointConfig = entity.ItemOeePointConfig,
                            //ListMachineBlockData = entity.ListMachineBlockData == null ? null : entity.ListMachineBlockData.Where(x => x.DeletedFlag != true).ToList(),
                            LineId = entity.LineId,
                            //ItemLine = entity.ItemLine,
                            ListMachine = entity.ListMachine == null ? null : entity.ListMachine.Where(x => x.DeletedFlag != true).ToList(),

                        });
                        result = queryable.ToList();
                    }
                    else
                    {
                        result = queryable.ToList();
                        foreach (var entity in result)
                        {
                            entity.ListMachineBlockData = entity.ListMachineBlockData == null ? null : entity.ListMachineBlockData.Where(x => x.DeletedFlag != true).ToList();
                            entity.ListMachine = entity.ListMachine == null ? null : entity.ListMachine.Where(x => x.DeletedFlag != true).ToList();

                        }
                    }

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

        public (List<OeePointEntity> listOeePoint, long totalRecord) GetListFilterOeePointManagement(
            OeePointListRequestModel request,
            bool isDirect = false,
            bool isTracking = false)
        {
            try
            {
                Dictionary<string, object> filterParams = StringToDictionaryHelper.ToStringAndObj(request.FilterStr);
                Dictionary<string, string> searchParams = StringToDictionaryHelper.ToDicOrString2(request.SearchStr, true);
                Dictionary<string, long> sortParams = StringToDictionaryHelper.ToStringLongTest(request.SortStr);

                List<OeePointEntity>? result = null;
                long totalRecord = 0;

                string cacheKey = $"{cacheKeyList}_GetListFilterOeePointManagement";
                if (request != null)
                {
                    cacheKey += $"{request.GetKeyCache()}";
                }

                if (!isDirect && !isTracking)
                {
                    (result, totalRecord) = this.GetRedisData<(List<OeePointEntity>, long)>(cacheKey, (null, 0));
                }

                if (result == null)
                {
                    var dataSet = _context.Set<OeePointEntity>();
                    IQueryable<OeePointEntity> queryable;
                    IQueryable<OeePointEntity> queryableCountTotal;
                    if (!isTracking)
                    {
                        queryable = dataSet.AsNoTracking().AsQueryable();
                    }
                    else
                    {
                        queryable = dataSet.AsQueryable();
                    }

                    if(request?.OeePointId != null)
                    {
                        queryable = queryable.Where(entity => entity.Id == request.OeePointId);
                    }

                    queryable = queryable
                                .Where(entity => entity.DeletedFlag != true)
                                .Include(entity => entity.ItemOeePointConfig)
                                .Include(entity => entity.ListMachineBlockData)
                                .ThenInclude(ml => ml.ItemMachineLoss)
                                .Include(entity => entity.ItemLine)
                                .Include(entity => entity.ListMachine)
                                ;

                    if (request.DateFrom != null)
                    {
                        queryable = queryable.Where(x => x.ListMachineBlockData.Any(entity => entity.EndDateTime != null && entity.EndDateTime.Value >= request.DateFrom));
                    }
                    if (request.DateTo != null)
                    {
                        queryable = queryable.Where(x => x.ListMachineBlockData.Any(entity => entity.StartDateTime != null && entity.StartDateTime.Value <= request.DateTo));
                    }

                    var responseModel = new OeePointResponseModel();

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
                        queryable = queryable.Select(entity => new OeePointEntity()
                        {
                            Id = entity.Id,
                            Order = entity.Order,
                            DeletedFlag = entity.DeletedFlag,
                            CreatedAt = entity.CreatedAt,
                            CreatedBy = entity.CreatedBy,
                            UpdatedAt = entity.UpdatedAt,
                            UpdatedBy = entity.UpdatedBy,
                            Name = entity.Name,
                            Note = entity.Note,
                            Description = entity.Description,
                            TagNames = entity.TagNames,
                            IdealRunRate = entity.IdealRunRate,
                            IdealCycleTime = entity.IdealCycleTime,
                            OeePointConfigId = entity.OeePointConfigId,
                            ItemOeePointConfig = entity.ItemOeePointConfig,
                            //ListMachineBlockData = entity.ListMachineBlockData == null ? null : entity.ListMachineBlockData.Where(x => x.DeletedFlag != true).ToList(),
                            LineId = entity.LineId,
                            ItemLine = entity.ItemLine,
                            ListMachine = entity.ListMachine == null ? null : entity.ListMachine.Where(x => x.DeletedFlag != true).ToList(),

                        });

                        result = queryable.ToList();
                    }
                    else
                    {
                        result = queryable.ToList();
                        foreach (var entity in result)
                        {
                            entity.ListMachineBlockData = entity.ListMachineBlockData == null ? null : entity.ListMachineBlockData.Where(x => x.DeletedFlag != true).ToList();
                            entity.ListMachine = entity.ListMachine == null ? null : entity.ListMachine.Where(x => x.DeletedFlag != true).ToList();

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
        public List<OeePointEntity> GetByLineId(long lineId, bool isDirect = false, bool isTracking = false)
        {
            try
            {
                List<OeePointEntity>? result = null;
                string cacheKey = $"{cacheKeyList}_lineId:{lineId}";
                if (!isDirect && !isTracking)
                {
                    result = this.GetRedisData<List<OeePointEntity>>(cacheKey, null);
                }

                if (result == null)
                {
                    var dataSet = _context.Set<OeePointEntity>();
                    IQueryable<OeePointEntity> queryable;
                    if (!isTracking)
                    {
                        queryable = dataSet.AsNoTracking().AsQueryable();
                    }
                    else
                    {
                        queryable = dataSet.AsQueryable();
                    }
                    queryable = queryable
                                .Include(entity => entity.ItemOeePointConfig)
                                //.Include(entity => entity.ListMachineBlockData)
                                //.Include(entity => entity.ItemLine)
                                .Include(entity => entity.ListMachine)

                                .Where(entity => entity.DeletedFlag != true)
                                .Where(entity => entity.LineId == lineId)
                                .OrderBy(entity => entity.Order).ThenBy(entity => entity.Id)
                                ;

                    queryable = queryable
                            .Skip(0).Take(ConstCommon.ConstSelectListMaxRecord);

                    if (!isTracking)
                    {
                        queryable = queryable.Select(entity => new OeePointEntity()
                        {
                            Id = entity.Id,
                            Order = entity.Order,
                            DeletedFlag = entity.DeletedFlag,
                            CreatedAt = entity.CreatedAt,
                            CreatedBy = entity.CreatedBy,
                            UpdatedAt = entity.UpdatedAt,
                            UpdatedBy = entity.UpdatedBy,
                            Name = entity.Name,
                            Note = entity.Note,
                            Description = entity.Description,
                            TagNames = entity.TagNames,
                            IdealRunRate = entity.IdealRunRate,
                            IdealCycleTime = entity.IdealCycleTime,
                            OeePointConfigId = entity.OeePointConfigId,
                            ItemOeePointConfig = entity.ItemOeePointConfig,
                            //ListMachineBlockData = entity.ListMachineBlockData == null ? null : entity.ListMachineBlockData.Where(x => x.DeletedFlag != true).ToList(),
                            LineId = entity.LineId,
                            //ItemLine = entity.ItemLine,
                            ListMachine = entity.ListMachine == null ? null : entity.ListMachine.Where(x => x.DeletedFlag != true).ToList(),

                        });
                        result = queryable.ToList();
                    }
                    else
                    {
                        result = queryable.ToList();
                        foreach (var entity in result)
                        {
                            entity.ListMachineBlockData = entity.ListMachineBlockData == null ? null : entity.ListMachineBlockData.Where(x => x.DeletedFlag != true).ToList();
                            entity.ListMachine = entity.ListMachine == null ? null : entity.ListMachine.Where(x => x.DeletedFlag != true).ToList();

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
