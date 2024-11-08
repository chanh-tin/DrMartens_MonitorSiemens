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
using iSoft.Common.Enums;

namespace SourceBaseBE.Database.Repository
{
    public class OrganizationRepository : BaseCRUDRepository<OrganizationEntity>
  {
    public OrganizationRepository(CommonDBContext dbContext)
        : base(dbContext)
    {
    }
    public override string GetName()
    {
      return nameof(OrganizationRepository);
    }
    /// <summary>
    /// GetById (@GenCRUD)
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    /// <exception cref="DBException"></exception>
    public override OrganizationEntity? GetById(long id, bool isTracking = false)
    {
      try
      {
        OrganizationEntity? result = null;
        //string cacheKey = $"{cacheKeyDetail}:{id}";
        //if (!isDirect && !isTracking)
        //{
        //  result = CachedFunc.GetRedisData<OrganizationEntity>(cacheKey, null);
        //}

        //if (result == null)
        //{
          var dataSet = _context.Set<OrganizationEntity>();
          IQueryable<OrganizationEntity> queryable;
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
          //result.Organization2s = result.Organization2s.Select(x => new Organization2Entity() { Id = x.Id, LossName = x.LossName }).ToList();

        //  CachedFunc.AddEntityCacheKey(GetName(), cacheKey, true);
        //  CachedFunc.SetRedisData(cacheKey, result, ConstCommon.ConstGetDetailCacheExpiredTimeInSeconds);
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
    public override List<OrganizationEntity> GetList(PagingRequestModel pagingReq = null, bool isDirect = false, bool isTracking = false)
    {
      try
      {
        string cacheKey = $"{cacheKeyList}";
        if (pagingReq != null)
        {
          cacheKey = $"{cacheKeyList}:{pagingReq.Page}|{pagingReq.PageSize}";
        }
        List<OrganizationEntity>? result = CachedFunc.GetRedisData<List<OrganizationEntity>>(cacheKey, null);
        if (result == null)
        {
          result = new List<OrganizationEntity>();
          if (pagingReq != null)
          {
            result = _context.Set<OrganizationEntity>()
                    .AsNoTracking()
                    .AsQueryable()
                    /*[GEN-11]*/
                    .Where(entity => entity.DeletedFlag != true)
                    .OrderBy(entity => entity.Order).ThenBy(entity => entity.Id)
                    .Skip(pagingReq.GetSkip()).Take(pagingReq.GetLimit())
                    .AsParallel()
                    .ToList();

            //.Select(entity => new OrganizationEntity
            // {
            //   Id = entity.Id,
            //   Organization2s = entity.Organization2s.Select(x => new Organization2Entity { Id = x.Id, LossName = x.LossName }).ToList()
            // })

            for (var i = 0; i < result.Count; i++)
            {

              
/*[GEN-12]*/
            }
          }
          else
          {
            result = _context.Set<OrganizationEntity>()
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
    public OrganizationEntity Upsert(OrganizationEntity entity/*[GEN-8]*/, long? userId = null)
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
    public OrganizationEntity Insert(OrganizationEntity entity/*[GEN-8]*/, long? userId = null)
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
          _context.Set<OrganizationEntity>().Add(entity);
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
    public OrganizationEntity Update(OrganizationEntity entity/*[GEN-8]*/, long? userId = null)
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
          _context.Set<OrganizationEntity>().Update(entity);
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
    public override object GetDisplayField(OrganizationEntity entity)
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
