using iSoft.Common;
using iSoft.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using SourceBaseBE.Database.DBContexts;
using SourceBaseBE.Database.Entities;
using iSoft.Common.Utils;
using SourceBaseBE.Database.Models.RequestModels;
using SourceBaseBE.Database.Models.ResponseModels;
using iSoft.Common.ExtensionMethods;

namespace SourceBaseBE.Database.Repository
{
    public class MachineLossRepository : BaseMachineLossRepository
    {
        public MachineLossRepository(CommonDBContext dbContext)
            : base(dbContext)
        {
        }

        public List<MachineLossDataModel> GetLossMachine(MachineLossManagementRequestModel request = null, int numberTopLoss = 0, bool isDirect = false)
        {
            try
            {
                List<MachineLossDataModel>? result = null;
                string cacheKey = $"GetLossMachine_{numberTopLoss}";
                if (request != null)
                {
                    cacheKey = $"{cacheKeyList}_{numberTopLoss}_{isDirect}_GetLossMachine";
                    cacheKey += EncodeUtil.MD5(request.ToJson());
                }

                if (!isDirect)
                {
                    result = this.GetRedisData<List<MachineLossDataModel>>(cacheKey, null);
                }

                if (result == null)
                {

                    var dataSet = _context.Set<MachineLossEntity>()
                        .Include(entity => entity.ListMachineBlockData)
                        .Where(entity => entity.DeletedFlag != true);
                     
                    var queryable = dataSet
                        .AsEnumerable()
                        .Select(entity => new
                        {
                            DurationInMiliSeconds = entity.ListMachineBlockData == null
                                ? 0
                                : entity.ListMachineBlockData
                                            .Where(x => request.OeePointId == null || x.OeePointId == request.OeePointId)
                                            .Where(entity => entity.EndDateTime != null && entity.EndDateTime.Value >= request.StartTime)
                                            .Where(entity => entity.StartDateTime != null && entity.StartDateTime.Value <= request.EndTime)
                                            .Sum(x => x.DurationInMiliSeconds),

                            MachineLossData = new MachineLossModel
                            {
                                LossName = entity.Name,
                                LossReason = entity.LossReason,
                                Note = entity.Note,
                                Order = entity.Order
                            }
                        })
                        .OrderByDescending(x => x.DurationInMiliSeconds)
                        .Take(numberTopLoss)
                        .ToList();

                    result = queryable.Select(x => new MachineLossDataModel
                    {
                        DurationInMiliSeconds = x.DurationInMiliSeconds,
                        MachineLossData = x.MachineLossData
                    }).ToList();



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
