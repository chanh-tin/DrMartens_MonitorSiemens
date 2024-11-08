using System.Collections.Generic;
using SourceBaseBE.Database.Repository;
using iSoft.Common.Models.RequestModels;
using System;
using System.Reflection;
using iSoft.Database.Entities;
using iSoft.Database.Models;
using Microsoft.Extensions.Logging;
using SourceBaseBE.Database.DBContexts;

namespace SourceBaseBE.MainService.Services
{
    public class BaseCRUDService<TEntity> : IDisposable where TEntity : BaseCRUDEntity, new()
    {
        protected CommonDBContext _dbContext;
        protected ILogger<BaseCRUDService<TEntity>> _logger;
        protected BaseCRUDRepository<TEntity> _repositoryCRUD;
        public BaseCRUDService()
        {
        }
        public BaseCRUDService(CommonDBContext dbContext, ILogger<BaseCRUDService<TEntity>> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public virtual List<Dictionary<string, object>> GetFormDataObjElement(TEntity entity)
        {
            return new List<Dictionary<string, object>>();
        }
        public virtual object GetDisplayName(string name, string entityName)
        {
            //var disName = JsonPropertyHelper<TEntity>.GetDisplayName(name);
            var disName = name;
            return disName;
        }
        public virtual List<TEntity> GetList(PagingRequestModel pagingReq = null, bool isDirect = false, bool isTracking = false)
        {
            return _repositoryCRUD.GetList(pagingReq, isDirect, isTracking);
        }
        public virtual List<TEntity> GetAll()
        {
            return _repositoryCRUD.GetAll();
        }
        public virtual TEntity GetById(long id, bool isTracking = false)
        {
            return _repositoryCRUD.GetById(id, isTracking);
        }
        public virtual long GetTotalRecord()
        {
            return _repositoryCRUD.GetTotalRecord();
        }
        public virtual TEntity Upsert(TEntity entity, long? userId = null)
        {
            _repositoryCRUD.Upsert(entity, userId);

            var upsertedEntity = _repositoryCRUD.GetById(entity.Id, false);

            return upsertedEntity;
        }
        public virtual int Delete(TEntity entity, long? userId = null, bool isSoftDelete = true)
        {
            return _repositoryCRUD.Delete(entity, userId, isSoftDelete);
        }
        public virtual int Delete(long id, long? userId = null, bool isSoftDelete = true)
        {
            return _repositoryCRUD.Delete(id, userId, isSoftDelete);
        }
        public virtual List<FormSelectOptionModel> GetListOptionData(List<object> list)
        {
            List<FormSelectOptionModel> rs = new List<FormSelectOptionModel>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    rs.Add(new FormSelectOptionModel(item, item));
                }
            }
            return rs;
        }
        public virtual List<string> GetListValueData(string str)
        {
            List<string> rs = new List<string>();
            if (str != null)
            {
                foreach (var item in str.Split(",", StringSplitOptions.RemoveEmptyEntries))
                {
                    rs.Add(item.ToString());
                }
            }
            return rs;
        }
        public virtual TEntity SetFileURL(TEntity entity, Dictionary<string, string> dicImagePath)
        {
            if (dicImagePath != null && dicImagePath.Count >= 1)
            {
                throw new NotImplementedException($"SetFileURL error");
            }
            return entity;
        }

        private bool _disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    //// TODO: dispose managed state (managed objects)
                    //this._repositoryCRUD.Dispose();

                    var fields = this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
                    foreach (var field in fields)
                    {
                        var type = (field.GetValue(this) as IDisposable);
                        type?.Dispose();
                    }
                }

                _disposedValue = true;
            }
        }
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}