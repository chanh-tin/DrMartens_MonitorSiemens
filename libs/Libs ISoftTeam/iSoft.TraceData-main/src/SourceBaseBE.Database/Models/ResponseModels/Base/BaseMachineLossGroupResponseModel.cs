// -----------------------------------------------------------------------------
// This file was automatically generated.
// Please do not edit this file manually.
//
// Generated Date: 
//
// -----------------------------------------------------------------------------

using iSoft.Common.Utils;
using SourceBaseBE.Database.Entities;
using Microsoft.EntityFrameworkCore;
using SourceBaseBE.Database.ExtensionMethods;
using iSoft.Common.Models.ResponseModels;
using LinqKit;
using SourceBaseBE.Database.Enums;
using static iSoft.Common.ConstCommon;
using iSoft.Common.Enums;

namespace SourceBaseBE.Database.Models.ResponseModels
{
    public class BaseMachineLossGroupResponseModel : BaseCRUDResponseModel<MachineLossGroupEntity>
    {
        public virtual string? Name { get; set; }
        public virtual string? Description { get; set; }
        public virtual string? Note { get; set; }
        public virtual EnumOeeAPQType? OeeAPQType { get; set; }
        public virtual List<MachineLossEntity>? ListMachineLoss { get; set; }
        
        public override object SetData(MachineLossGroupEntity entity)
        {
            base.SetData(entity);
            this.Name = entity.Name;
            this.Description = entity.Description;
            this.Note = entity.Note;
            this.OeeAPQType = entity.OeeAPQType;
            if (entity.ListMachineLoss != null)
            {
                this.ListMachineLoss = entity.ListMachineLoss.Select(x => x).ToList();
            }
        
            return this;
        }
        public override List<object> SetData(List<MachineLossGroupEntity> listEntity)
        {
            List<Object> listRS = new List<object>();
            foreach (MachineLossGroupEntity entity in listEntity)
            {
                listRS.Add(new BaseMachineLossGroupResponseModel().SetData(entity));
            }
            return listRS;
        }
        public IQueryable<MachineLossGroupEntity> PrepareWhereQueryFilter(
            IQueryable<MachineLossGroupEntity> query,
            Dictionary<string, object> dicInputFieldKey2Value,
            Func<Dictionary<string, string>> getFieldAttributes,
            Type responseType)
        {
            var dicFieldAttr = getFieldAttributes();
            foreach (var keyVal in dicInputFieldKey2Value)
            {
                string inputFieldKey = keyVal.Key;
                if (!dicFieldAttr.ContainsKey(inputFieldKey))
                {
                    continue;
                }

                var fieldType = dicFieldAttr[inputFieldKey];
                object? searchValue = ConvertUtil.ConvertData(keyVal.Value, fieldType);
                if (searchValue == null)
                {
                    continue;
                }

                var propertyInfo = responseType.GetProperty(inputFieldKey);
                if (propertyInfo == null)
                {
                    continue;
                }

                query = query.WherePropertyEquals(inputFieldKey, searchValue);
            }
            return query;
        }

        public IQueryable<MachineLossGroupEntity> PrepareQuerySort(
            IQueryable<MachineLossGroupEntity> query,
            Dictionary<string, long> dicInputFieldKey2SortOrder,
            Func<Dictionary<string, string>> getFieldAttributes,
            Type responseType)
        {
            var dicFieldAttr = getFieldAttributes();
            foreach (var keyVal in dicInputFieldKey2SortOrder)
            {
                string inputFieldKey = keyVal.Key;

                var propertyInfo = responseType.GetProperty(inputFieldKey);
                if (propertyInfo == null)
                {
                    continue;
                }

                if (keyVal.Value == -1)
                {
                    query = query.OrderByPropertyDescending(inputFieldKey);
                }
                else
                {
                    query = query.OrderByPropertyAscending(inputFieldKey);
                }
            }
            return query;
        }
        public IQueryable<MachineLossGroupEntity> PrepareWhereQuerySearch(
            IQueryable<MachineLossGroupEntity> query,
            Dictionary<string, string> dicInputFieldKey2Value,
            Func<Dictionary<string, string>> getFieldAttributes,
            Func<Dictionary<string, string>> getFieldAttributesSearchAll,
            Type responseType)
        {
            var dicFieldAttr = getFieldAttributes();
            var dicFieldAttrSearchAll = getFieldAttributesSearchAll();
            var predicate = LinqKit.PredicateBuilder.New<MachineLossGroupEntity>(true);
            bool searchAllFlag = false;
            foreach (var keyVal in dicInputFieldKey2Value)
            {
                string inputFieldKeyAll = keyVal.Key;
                if (inputFieldKeyAll.Trim().ToUpper() == "ALL")
                {
                    searchAllFlag = true;
                    string searchValue = ConvertUtil.GetString(keyVal.Value);
                    if (searchValue == null)
                    {
                        break;
                    }

                    foreach (var keyVal2 in dicFieldAttrSearchAll)
                    {
                        string inputFieldKey = keyVal2.Key;
                        predicate = this.GetPredicate(predicate, inputFieldKey, searchValue);
                    }
                    break;
                }
            }

            if (!searchAllFlag)
            {
                foreach (var keyVal in dicInputFieldKey2Value)
                {
                    string inputFieldKey = keyVal.Key;
                    if (!dicFieldAttr.ContainsKey(inputFieldKey))
                    {
                        continue;
                    }

                    var fieldType = dicFieldAttr[inputFieldKey];
                    string searchValue = ConvertUtil.GetString(keyVal.Value);
                    if (searchValue == null)
                    {
                        continue;
                    }

                    var propertyInfo = responseType.GetProperty(inputFieldKey);
                    if (propertyInfo == null)
                    {
                        continue;
                    }

                    predicate = this.GetPredicate(predicate, inputFieldKey, searchValue);
                }
            }
            return query.Where(predicate).AsQueryable();
        }

        public virtual ExpressionStarter<MachineLossGroupEntity> GetPredicate(ExpressionStarter<MachineLossGroupEntity> predicate, string inputFieldKey, string searchValue)
        {
            if (inputFieldKey == nameof(BaseMachineLossGroupResponseModel.Id))
            {
                predicate = predicate.Or(x => EF.Functions.Unaccent(x.Id.ToString().ToLower()).Contains(EF.Functions.Unaccent($"{searchValue.ToLower()}")));
            }
            else if (inputFieldKey == nameof(BaseMachineLossGroupResponseModel.Order))
            {
                predicate = predicate.Or(x => EF.Functions.Unaccent(x.Order.ToString().ToLower()).Contains(EF.Functions.Unaccent($"{searchValue.ToLower()}")));
            }
            return predicate;
        }

        public virtual Dictionary<string, string> GetFieldAttributes()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            
                    

            return dic;
        }

        public virtual Dictionary<string, string> GetFieldAttributesSearchAll()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            
                    

            return dic;
        }

        public virtual List<ColumnResponseModel> GetColumnAttribute()
        {
            List<ColumnResponseModel> columns = new List<ColumnResponseModel>();
            columns.Add(new ColumnResponseModel(
                nameof(BaseMachineLossGroupResponseModel.Id),
                nameof(BaseMachineLossGroupResponseModel.Id),
                true, true, true
                ));

            return columns;
        }
    }
}
