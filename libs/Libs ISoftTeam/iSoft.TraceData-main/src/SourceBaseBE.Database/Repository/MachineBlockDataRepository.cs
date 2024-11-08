using iSoft.Common;
using iSoft.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using SourceBaseBE.Database.DBContexts;
using SourceBaseBE.Database.Entities;
using iSoft.Common.Utils;
using SourceBaseBE.Database.Models.RequestModels;
using iSoft.Common.ExtensionMethods;
using PRPO.Database.Helpers;
using SourceBaseBE.Database.Models.ResponseModels;
using SourceBaseBE.Database.Enums;


namespace SourceBaseBE.Database.Repository
{
    public class MachineBlockDataRepository : BaseMachineBlockDataRepository
    {
        public MachineBlockDataRepository(CommonDBContext dbContext)
            : base(dbContext)
        {
        }
        public override bool IsCacheData
        {
            get { return false; }
        }
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
                    //queryable = queryable.Select(entity => new MachineBlockDataEntity()
                    //{
                    //    Id = entity.Id,
                    //    Order = entity.Order,
                    //    DeletedFlag = entity.DeletedFlag,
                    //    CreatedAt = entity.CreatedAt,
                    //    CreatedBy = entity.CreatedBy,
                    //    UpdatedAt = entity.UpdatedAt,
                    //    UpdatedBy = entity.UpdatedBy,
                    //    LineId = entity.LineId,
                    //    MachineStatus = entity.MachineStatus,
                    //    StartDateTime = entity.StartDateTime,
                    //    EndDateTime = entity.EndDateTime,
                    //    DurationInMiliSeconds = entity.DurationInMiliSeconds,
                    //    BlockCountIn = entity.BlockCountIn,
                    //    BlockGoodCount = entity.BlockGoodCount,
                    //    BlockNGCount = entity.BlockNGCount,
                    //    LastCountIn = entity.LastCountIn,
                    //    LastGoodCount = entity.LastGoodCount,
                    //    LastNGCount = entity.LastNGCount,
                    //    LastMessageId = entity.LastMessageId,
                    //    LastReceivedTime = entity.LastReceivedTime,
                    //    MachineLossId = entity.MachineLossId,
                    //    ItemMachineLoss = entity.ItemMachineLoss,
                    //    MachineLossPositionId = entity.MachineLossPositionId,
                    //    ItemMachineLossPosition = entity.ItemMachineLossPosition,
                    //    MachineLossDescribeId = entity.MachineLossDescribeId,
                    //    ItemMachineLossDescribe = entity.ItemMachineLossDescribe,
                    //    OeePointId = entity.OeePointId,
                    //    ItemOeePoint = entity.ItemOeePoint,

                    //});
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
        public List<MachineBlockDataEntity> GetByOeePointIdAndStatusAndTime(
            long oeePointId,
            List<EnumMachineStatus> listMachineStatus,
            DateTime startDateTime,
            DateTime endDateTime,
            bool isDirect = false,
            bool isTracking = false)
        {
            try
            {
                List<MachineBlockDataEntity>? result = null;
                string paramKey = $"{string.Join(",", listMachineStatus)}_{startDateTime.GetDateTimeStr()}_{endDateTime.GetDateTimeStr()}";
                string cacheKey = $"{cacheKeyList}_GetByOeePointIdAndStatus:{oeePointId}_{EncodeUtil.MD5(paramKey)}";

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
                                .Where(entity => entity.OeePointId == oeePointId
                                            && entity.MachineStatus != null
                                            && listMachineStatus.Contains(entity.MachineStatus.Value))
                                .Where(entity => (!(entity.StartDateTime > endDateTime || entity.EndDateTime <= startDateTime)))
                                .OrderBy(entity => entity.Order).ThenBy(entity => entity.Id)
                    ;

                    queryable = queryable
                            .Skip(0).Take(ConstCommon.ConstSelectListMaxRecord);

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

        public MachineBlockDataEntity? GetLast(long oeePointId, bool isTracking = false)
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
                      .Include(entity => entity.ItemOeePoint)

                      .Where(entity => entity.DeletedFlag != true && entity.OeePointId == oeePointId);

                queryable = queryable.OrderByDescending(entity => entity.StartDateTime);

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

        public List<MachineBlockDataEntity> GetListMachineBlockData(
            GetOeeManagementRequestModel request = null, 
            bool isGetDowntimeOnly = true,  
            bool isDirect = false, 
            bool isTracking = false)
        {
            try
            {
                List<MachineBlockDataEntity>? result = null;
                string cacheKey = $"{cacheKeyList}_GetListMachineBlockData:";
                cacheKey += EncodeUtil.MD5(request.ToJson() + isGetDowntimeOnly.ToString());

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

                    queryable = queryable.Include(entity => entity.ItemMachineLoss)
                                            .ThenInclude(entity => entity.ItemMachineLossGroup)
                                         .Include(entity => entity.ItemOeePoint)
                                         .Where(x => x.OeePointId == request.OeePointId)
                                         .Where(x => x.DeletedFlag != true
                                                 && x.StartDateTime != null
                                                 && x.EndDateTime != null);

                    if (isGetDowntimeOnly)
                    {
                        queryable = queryable.Where(entity => entity.MachineStatus == EnumMachineStatus.StopUnplanned
                                                           || entity.MachineStatus == EnumMachineStatus.Unknown);
                    }
                    else
                    {
                       // Get all Status
                    }

                    if (request.StartTime != null && request.EndTime != null)
                    {
                        queryable = queryable.Where(entity => entity.EndDateTime.Value > request.StartTime)
                                             .Where(entity => entity.StartDateTime.Value < request.EndTime)
                                             .OrderBy(entity => entity.Order).ThenBy(entity => entity.Id)
                                             .Skip(0).Take(ConstCommon.ConstSelectListMaxRecord);
                    }

                    if (!isTracking)
                    {
                        queryable = queryable.Select(entity => new MachineBlockDataEntity()
                        {
                            Id = entity.Id,
                            //Order = entity.Order,
                            //DeletedFlag = entity.DeletedFlag,
                            //CreatedAt = entity.CreatedAt,
                            //CreatedBy = entity.CreatedBy,
                            //UpdatedAt = entity.UpdatedAt,
                            //UpdatedBy = entity.UpdatedBy,
                            LineId = entity.LineId,
                            MachineStatus = entity.MachineStatus,
                            StartDateTime = entity.StartDateTime,
                            EndDateTime = entity.EndDateTime,
                            DurationInMiliSeconds = entity.DurationInMiliSeconds,
                            //BlockCountIn = entity.BlockCountIn,
                            //BlockGoodCount = entity.BlockGoodCount,
                            //BlockNGCount = entity.BlockNGCount,
                            //LastCountIn = entity.LastCountIn,
                            //LastGoodCount = entity.LastGoodCount,
                            //LastNGCount = entity.LastNGCount,
                            //LastMessageId = entity.LastMessageId,
                            //LastReceivedTime = entity.LastReceivedTime,
                            MachineLossId = entity.MachineLossId,
                            ItemMachineLoss = entity.ItemMachineLoss,
                            MachineLossPositionId = entity.MachineLossPositionId,
                            ItemMachineLossPosition = entity.ItemMachineLossPosition,
                            MachineLossDescribeId = entity.MachineLossDescribeId,
                            ItemMachineLossDescribe = entity.ItemMachineLossDescribe,
                            OeePointId = entity.OeePointId,
                            //ItemOeePoint = entity.ItemOeePoint,

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

        public List<MachineBlockDataEntity> GetListMachineBlockDataBigLoss(MachineBlockDataRequestModel request = null, bool isDirect = false, bool isTracking = false)
        {
            try
            {
                List<MachineBlockDataEntity>? result = null;
                string cacheKey = $"{cacheKeyList}_GetListMachineBlockData_{request.ToJson()}";

                if (!isDirect)
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

                    queryable = queryable.Include(entity => entity.ItemMachineLoss)
                                            .ThenInclude(entity => entity.ItemMachineLossGroup)
                                         .Include(entity => entity.ItemOeePoint)
                                         .Where(x => x.DeletedFlag != true
                                                  && x.StartDateTime != null
                                                  && x.EndDateTime != null)
                                         .Where(x => request.OeePointId == null || x.OeePointId == request.OeePointId)
                                         .Where(entity=> entity.ItemMachineLoss != null && entity.ItemMachineLoss.ItemMachineLossGroup != null)
                                         .Where(entity => entity.MachineStatus != EnumMachineStatus.RunNG
                                                       && entity.MachineStatus != EnumMachineStatus.RunGood);


                    if (request.StartTime != null && request.EndTime != null)
                    {
                        queryable = queryable.Where(entity => entity.EndDateTime != null && entity.EndDateTime.Value >= request.StartTime)
                                             .Where(entity => entity.StartDateTime != null && entity.StartDateTime.Value <= request.EndTime)
                                             .OrderBy(entity => entity.Order).ThenBy(entity => entity.Id);
                    }

                    queryable = queryable
                            .Skip(0).Take(ConstCommon.ConstSelectListMaxRecord);

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

        public (List<MachineBlockDataEntity> listMachineBlockData, long totalRecord) GetListFilterLossAssigment(
            MachineBlockDataListRequestModel request,
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

                string cacheKey = $"{cacheKeyList}_GetListFilterLossAssigment:";
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
                    //Check point Id
                    //if (request?.PointId != null)
                    //{
                    //    queryable = queryable.Where(entity => entity.OeePointId == request.PointId);
                    //}

                    //Check show loss assigned
                    if (request.IsShowLossAssigned == false)
                    {
                        queryable = queryable.Where(entity => entity.MachineLossId == null);
                    }

                    queryable = queryable
                                .Where(entity => entity.DeletedFlag != true)
                                .Include(entity => entity.ItemMachineLoss)
                                    .ThenInclude(gr => gr.ItemMachineLossGroup)
                                .Include(entity => entity.ItemOeePoint)
                                    .ThenInclude(entity => entity.ItemLine)
                                .Include(entity => entity.ItemOeePoint)
                                    .ThenInclude(entity => entity.ListMachine)
                                .Include(entity => entity.ItemMachineLossDescribe)
                                .Include(entity => entity.ItemMachineLossPosition)
                                .Where(entity => entity.MachineStatus != EnumMachineStatus.RunGood)
                                .Where(x => x.StartDateTime != null);

                    if (request.DateFrom != null)
                    {
                        queryable = queryable.Where(entity => entity.EndDateTime != null && entity.EndDateTime.Value >= request.DateFrom);
                    }
                    if (request.DateTo != null)
                    {
                        queryable = queryable.Where(entity => entity.StartDateTime != null && entity.StartDateTime.Value <= request.DateTo);
                    }

                    var responseModel = new MachineBlockDataResponseModel();

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

                    queryable = queryable.OrderByDescending(entity => entity.StartDateTime);

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
                        //queryable = queryable.Select(entity => new MachineBlockDataEntity()
                        //{
                        //    Id = entity.Id,
                        //    Order = entity.Order,
                        //    DeletedFlag = entity.DeletedFlag,
                        //    CreatedAt = entity.CreatedAt,
                        //    CreatedBy = entity.CreatedBy,
                        //    UpdatedAt = entity.UpdatedAt,
                        //    UpdatedBy = entity.UpdatedBy,
                        //    LineId = entity.LineId,
                        //    MachineStatus = entity.MachineStatus,
                        //    StartDateTime = entity.StartDateTime,
                        //    EndDateTime = entity.EndDateTime,
                        //    DurationInMiliSeconds = entity.DurationInMiliSeconds,
                        //    //BlockCountIn = entity.BlockCountIn,
                        //    //BlockGoodCount = entity.BlockGoodCount,
                        //    //BlockNGCount = entity.BlockNGCount,
                        //    //LastCountIn = entity.LastCountIn,
                        //    //LastGoodCount = entity.LastGoodCount,
                        //    //LastNGCount = entity.LastNGCount,
                        //    //LastMessageId = entity.LastMessageId,
                        //    //LastReceivedTime = entity.LastReceivedTime,
                        //    MachineLossId = entity.MachineLossId,
                        //    ItemMachineLoss = entity.ItemMachineLoss,
                        //    OeePointId = entity.OeePointId,
                        //    ItemOeePoint = entity.ItemOeePoint,

                        //});

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
    }
}
