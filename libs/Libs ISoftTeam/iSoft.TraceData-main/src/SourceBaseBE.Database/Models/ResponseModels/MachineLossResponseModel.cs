using iSoft.Common.Utils;
using SourceBaseBE.Database.Entities;
using static iSoft.Common.ConstCommon;
using iSoft.Common.Enums;
using SourceBaseBE.Database.Enums;
using iSoft.Common.Models.ResponseModels;
using LinqKit;
using SourceBaseBE.Database.ExtensionMethods;
using Microsoft.EntityFrameworkCore;

namespace SourceBaseBE.Database.Models.ResponseModels
{
    public class MachineLossResponseModel : BaseMachineLossResponseModel
    {
        public string? Line { get; set; }
        public override string? Name { get; set; }
        public override string? LossReason { get; set; }
        public override string? Note { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? Duration { get; set; }

        public override object SetData(MachineLossEntity entity)
        {
            base.SetData(entity);

            //todo 
            return this;
        }
        public override List<object> SetData(List<MachineLossEntity> listEntity)
        {
            List<Object> listRS = new List<object>();
            foreach (MachineLossEntity entity in listEntity)
            {
                listRS.Add(new BaseMachineLossResponseModel().SetData(entity));
            }
            return listRS;
        }
        public IQueryable<MachineLossEntity> PrepareWhereQueryFilter(
            IQueryable<MachineLossEntity> query,
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

        public IQueryable<MachineLossEntity> PrepareQuerySort(
            IQueryable<MachineLossEntity> query,
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
        public IQueryable<MachineLossEntity> PrepareWhereQuerySearch(
            IQueryable<MachineLossEntity> query,
            Dictionary<string, string> dicInputFieldKey2Value,
            Func<Dictionary<string, string>> getFieldAttributes,
            Func<Dictionary<string, string>> getFieldAttributesSearchAll,
            Type responseType)
        {
            var dicFieldAttr = getFieldAttributes();
            var dicFieldAttrSearchAll = getFieldAttributesSearchAll();
            var predicate = LinqKit.PredicateBuilder.New<MachineLossEntity>(true);
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

        public override ExpressionStarter<MachineLossEntity> GetPredicate(ExpressionStarter<MachineLossEntity> predicate, string inputFieldKey, string searchValue)
        {
            if (inputFieldKey == nameof(BaseMachineLossResponseModel.Id))
            {
                predicate = predicate.Or(x => Microsoft.EntityFrameworkCore.EF.Functions.Unaccent(x.Id.ToString().ToLower()).Contains(EF.Functions.Unaccent($"{searchValue.ToLower()}")));
            }
            else if (inputFieldKey == nameof(BaseMachineLossResponseModel.Order))
            {
                predicate = predicate.Or(x => Microsoft.EntityFrameworkCore.EF.Functions.Unaccent(x.Order.ToString().ToLower()).Contains(EF.Functions.Unaccent($"{searchValue.ToLower()}")));
            }
            return predicate;
        }

        public virtual Dictionary<string, string> GetFieldAttributes()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(nameof(BaseMachineLossResponseModel.Id), typeof(long).ToString());
            dic.Add(nameof(BaseMachineLossResponseModel.Order), typeof(long).ToString());
            dic.Add(nameof(BaseMachineLossResponseModel.DeletedFlag), typeof(bool).ToString());
            dic.Add(nameof(BaseMachineLossResponseModel.CreatedAt), typeof(DateTime).ToString());
            dic.Add(nameof(BaseMachineLossResponseModel.CreatedUsername), typeof(string).ToString());

            return dic;
        }

        public override Dictionary<string, string> GetFieldAttributesSearchAll()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(nameof(BaseMachineLossResponseModel.Id), typeof(long).ToString());
            dic.Add(nameof(BaseMachineLossResponseModel.Order), typeof(long).ToString());
            dic.Add(nameof(BaseMachineLossResponseModel.DeletedFlag), typeof(bool).ToString());
            dic.Add(nameof(BaseMachineLossResponseModel.CreatedAt), typeof(DateTime).ToString());
            dic.Add(nameof(BaseMachineLossResponseModel.CreatedUsername), typeof(string).ToString());

            return dic;
        }

        public virtual List<ColumnResponseModel> GetColumnAttribute()
        {
            List<ColumnResponseModel> columns = new List<ColumnResponseModel>();
            columns.Add(new ColumnResponseModel(
                nameof(BaseMachineLossResponseModel.Id),
                nameof(BaseMachineLossResponseModel.Id),
                true, true, true
                ));

            return columns;
        }
    }
}
