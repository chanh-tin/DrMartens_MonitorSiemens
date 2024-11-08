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
using SourceBaseBE.Database.Models.ResponseModels;
using iSoft.Database.Helpers;


namespace SourceBaseBE.Database.Repository
{
    public class Example001Repository : BaseExample001Repository
    {
        public Example001Repository(CommonDBContext dbContext)
            : base(dbContext)
        {
        }
        public override string GetRepositoryName()
        {
            return nameof(Example001Repository);
        }
        /// <summary>
        /// GetListFilterMultiLang (@GenCRUD)
        /// </summary>
        /// <param name="pagingReq"></param>
        /// <returns></returns>
        /// <exception cref="DBException"></exception>
        public virtual (List<Example001Entity> listExample001, long totalRecord) GetListFilterMultiLang(
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

                string cacheKey = $"{CacheKeyList}_ListFilterMultiLang:";
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
                                .Include(entity => entity.ListExample001Trans)
                                //.Include(entity => entity.ItemExample002)
                                //    .ThenInclude(item => item.Example002Trans)
                                //.Include(entity => entity.ListExample003)
                                //    .ThenInclude(list => list.Example003Trans)

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

                    var responseModel = new Example001ResponseModel();

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

                    //queryable = queryable.Select(entity => new Example001Entity()
                    //{
                    //    Id = entity.Id,
                    //    Order = entity.Order,
                    //    DeletedFlag = entity.DeletedFlag,
                    //    CreatedAt = entity.CreatedAt,
                    //    CreatedBy = entity.CreatedBy,
                    //    UpdatedAt = entity.UpdatedAt,
                    //    UpdatedBy = entity.UpdatedBy,
                    //                             Name = entity.Name,
                    //                             NameReadonly = entity.NameReadonly,
                    //                             Username = entity.Username,
                    //                             ListExample001Trans = entity.ListExample001Trans == null ? null : entity.ListExample001Trans.Where(x => x.DeletedFlag != true).ToList(),
                    //                             StartDate = entity.StartDate,
                    //                             StartDateTime = entity.StartDateTime,
                    //                             Gender = entity.Gender,
                    //                             Example002Id = entity.Example002Id,
                    //                             Description = entity.Description,
                    //                             RefreshTime2 = entity.RefreshTime2,
                    //                             RefreshTime3 = entity.RefreshTime3,
                    //                             RefreshTime5 = entity.RefreshTime5,
                    //                             Password2 = entity.Password2,
                    //                             Email1 = entity.Email1,
                    //                             File1 = entity.File1,
                    //                             Price = entity.Price,
                    //                             RefreshTime1 = entity.RefreshTime1,
                    //                             PhoneNumber1 = entity.PhoneNumber1,
                    //                             ItemExample002 = entity.ItemExample002,
                    //                             ListImage1 = entity.ListImage1,
                    //                             Enable = entity.Enable,
                    //                             Label1 = entity.Label1,
                    //                             TimeOnlyData = entity.TimeOnlyData,
                    //                             ListExample003 = entity.ListExample003 == null ? null : entity.ListExample003.Where(x => x.DeletedFlag != true).ToList(),
                    //                             Password1 = entity.Password1,
                    //                             CheckBoxValues = entity.CheckBoxValues,
                    //                             Avatar = entity.Avatar,
                    //                             ListFile1 = entity.ListFile1,
                    //                             RefreshTime4 = entity.RefreshTime4,

                    //});
                    result = queryable.ToList();
                    foreach (var entity in result)
                    {
                        entity.ListExample001Trans = entity.ListExample001Trans == null ? null : entity.ListExample001Trans.Where(x => x.DeletedFlag != true).ToList();
                        entity.ListExample003 = entity.ListExample003 == null ? null : entity.ListExample003.Where(x => x.DeletedFlag != true).ToList();

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
    }
}
