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
    public class MachineBlockDataResponseModel : BaseMachineBlockDataResponseModel
    {
        public string? Line { get; set; }
        public string? LossName { get; set; }
        public string? LossPosition { get; set; }
        public string? LossDescription { get; set; }
        public string? Note { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public long? Duration { get; set; }
        public string? LossGroup { get; set; }
        public long? MachineLossGroupId { get; set; }
        public string? OeePoint { get; set; }

        public override MachineBlockDataResponseModel SetData(MachineBlockDataEntity entity)
        {
            this.Id = entity?.Id;
            this.Line = entity?.ItemOeePoint?.ItemLine?.Name;
            this.LossName = entity?.ItemMachineLoss?.Name;
            this.LossDescription = entity?.ItemMachineLossDescribe?.Name;
            this.LossPosition = entity?.ItemMachineLossPosition?.Name;
            this.Note = entity?.ItemMachineLoss?.Note;
            this.StartDateTime = entity?.StartDateTime;
            this.EndDateTime = entity?.EndDateTime;
            this.Duration = entity?.DurationInMiliSeconds;
            this.LossGroup = entity?.ItemMachineLoss?.ItemMachineLossGroup?.Name;
            this.OeePoint = entity?.ItemOeePoint?.Name;

            return this;
        }
        public override List<object> SetData(List<MachineBlockDataEntity> listEntity)
        {
            List<Object> listRS = new List<object>();
            foreach (MachineBlockDataEntity entity in listEntity)
            {
                listRS.Add(new MachineBlockDataResponseModel().SetData(entity));
            }
            return listRS;
        }
        public IQueryable<MachineBlockDataEntity> PrepareWhereQueryFilter(
            IQueryable<MachineBlockDataEntity> query,
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
                
                if (inputFieldKey == nameof(MachineBlockDataResponseModel.Line))
                {
                    query = query.Where(x => x.LineId == (long)searchValue);
                }
                else if (inputFieldKey == nameof(MachineBlockDataResponseModel.OeePoint))
                {
                    query = query.Where(x => x.ItemOeePoint != null && x.ItemOeePoint.Id == (long)searchValue);
                }
                else if (inputFieldKey == nameof(MachineBlockDataResponseModel.LossPosition))
                {
                    query = query.Where(x => x.ItemMachineLossPosition != null && x.ItemMachineLossPosition.Id == (long)searchValue);
                }
                else if (inputFieldKey == nameof(MachineBlockDataResponseModel.LossDescription))
                {
                    query = query.Where(x => x.ItemMachineLossDescribe != null && x.ItemMachineLossDescribe.Id == (long)searchValue);
                }
                else if (inputFieldKey == nameof(MachineBlockDataResponseModel.LossName))
                {
                    query = query.Where(x => x.ItemMachineLoss != null && x.ItemMachineLoss.Id == (long)searchValue);
                }
                if (inputFieldKey == nameof(MachineBlockDataResponseModel.LossGroup))
                {
                    query = query.Where(x => x.ItemMachineLoss != null && x.ItemMachineLoss.ItemMachineLossGroup != null
                                          && x.ItemMachineLoss.ItemMachineLossGroup.Id == (long)searchValue);
                }

                //if (inputFieldKey == nameof(MachineBlockDataResponseModel.MachineLossGroupId)
                //  || inputFieldKey == nameof(MachineBlockDataResponseModel.OeePoint))
                //{
                //    continue;
                //}
                //else
                //{
                //    query = query.WherePropertyEquals(inputFieldKey, searchValue);
                //}
            }
            return query;
        }

        public IQueryable<MachineBlockDataEntity> PrepareQuerySort(
            IQueryable<MachineBlockDataEntity> query,
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
        public IQueryable<MachineBlockDataEntity> PrepareWhereQuerySearch(
            IQueryable<MachineBlockDataEntity> query,
            Dictionary<string, string> dicInputFieldKey2Value,
            Func<Dictionary<string, string>> getFieldAttributes,
            Func<Dictionary<string, string>> getFieldAttributesSearchAll,
            Type responseType)
        {
            var dicFieldAttr = getFieldAttributes();
            var dicFieldAttrSearchAll = getFieldAttributesSearchAll();
            var predicate = LinqKit.PredicateBuilder.New<MachineBlockDataEntity>(true);
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

        public override ExpressionStarter<MachineBlockDataEntity> GetPredicate(ExpressionStarter<MachineBlockDataEntity> predicate, string inputFieldKey, string searchValue)
        {
            if (inputFieldKey == nameof(MachineBlockDataResponseModel.LossName))
            {
                predicate = predicate.Or(x => Microsoft.EntityFrameworkCore.EF.Functions.Unaccent(x.ItemMachineLoss.Name.ToLower()).Contains(EF.Functions.Unaccent($"{searchValue.ToLower()}")));
            }
            else if (inputFieldKey == nameof(MachineBlockDataResponseModel.LossDescription))
            {
                predicate = predicate.Or(x => Microsoft.EntityFrameworkCore.EF.Functions.Unaccent(x.ItemMachineLossDescribe.Name.ToLower()).Contains(EF.Functions.Unaccent($"{searchValue.ToLower()}")));
            }
            else if (inputFieldKey == nameof(MachineBlockDataResponseModel.LossGroup))
            {
                predicate = predicate.Or(x => Microsoft.EntityFrameworkCore.EF.Functions.Unaccent(x.ItemMachineLoss.ItemMachineLossGroup.Name.ToLower()).Contains(EF.Functions.Unaccent($"{searchValue.ToLower()}")));
            }
            else if (inputFieldKey == nameof(MachineBlockDataResponseModel.LossPosition))
            {
                predicate = predicate.Or(x => Microsoft.EntityFrameworkCore.EF.Functions.Unaccent(x.ItemMachineLossPosition.Name.ToLower()).Contains(EF.Functions.Unaccent($"{searchValue.ToLower()}")));
            }
            else if (inputFieldKey == nameof(MachineBlockDataResponseModel.OeePoint))
            {
                predicate = predicate.Or(x => Microsoft.EntityFrameworkCore.EF.Functions.Unaccent(x.ItemOeePoint.Name.ToLower()).Contains(EF.Functions.Unaccent($"{searchValue.ToLower()}")));
            }
            else if (inputFieldKey == nameof(MachineBlockDataResponseModel.Note))
            {
                predicate = predicate.Or(x => Microsoft.EntityFrameworkCore.EF.Functions.Unaccent(x.ItemMachineLoss.Note.ToLower()).Contains(EF.Functions.Unaccent($"{searchValue.ToLower()}")));
            }

            return predicate;
        }

        public override Dictionary<string, string> GetFieldAttributes()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(nameof(MachineBlockDataResponseModel.Line), typeof(long).ToString());
            dic.Add(nameof(MachineBlockDataResponseModel.OeePoint), typeof(long).ToString());
            dic.Add(nameof(MachineBlockDataResponseModel.LossPosition), typeof(long).ToString());
            dic.Add(nameof(MachineBlockDataResponseModel.LossDescription), typeof(long).ToString());
            dic.Add(nameof(MachineBlockDataResponseModel.LossName), typeof(long).ToString());
            dic.Add(nameof(MachineBlockDataResponseModel.LossGroup), typeof(long).ToString());
            dic.Add(nameof(MachineBlockDataResponseModel.Note), null);

            return dic;
        }

        public virtual Dictionary<string, string> GetFieldAttributesSearchAll()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(nameof(MachineBlockDataResponseModel.OeePoint), typeof(string).ToString());
            dic.Add(nameof(MachineBlockDataResponseModel.LossPosition), typeof(string).ToString());
            dic.Add(nameof(MachineBlockDataResponseModel.LossDescription), typeof(string).ToString());
            dic.Add(nameof(MachineBlockDataResponseModel.LossName), typeof(string).ToString());
            dic.Add(nameof(MachineBlockDataResponseModel.LossGroup), typeof(string).ToString());
            dic.Add(nameof(MachineBlockDataResponseModel.Note), typeof(string).ToString());

            return dic;
        }

        public override List<ColumnResponseModel> GetColumnAttribute()
        {
            List<ColumnResponseModel> columns = new List<ColumnResponseModel>();

            columns.Add(new ColumnResponseModel(
                nameof(MachineBlockDataResponseModel.Line),
                nameof(MachineBlockDataResponseModel.Line),
                false, true, true
                ));

            columns.Add(new ColumnResponseModel(
                nameof(MachineBlockDataResponseModel.OeePoint),
                nameof(MachineBlockDataResponseModel.OeePoint),
                true, true, true
                ));

            columns.Add(new ColumnResponseModel(
               nameof(MachineBlockDataResponseModel.StartDateTime),
               "Start Time",
               false, false, true
               ));

            columns.Add(new ColumnResponseModel(
                nameof(MachineBlockDataResponseModel.EndDateTime),
                "End Time",
                false, false, true
                ));

            columns.Add(new ColumnResponseModel(
                nameof(MachineBlockDataResponseModel.Duration),
                nameof(MachineBlockDataResponseModel.Duration),
                false, false, true
                ));


            columns.Add(new ColumnResponseModel(
                nameof(MachineBlockDataResponseModel.LossName),
                "Loss Name",
                true, true, true
                ));

            columns.Add(new ColumnResponseModel(
                nameof(MachineBlockDataResponseModel.LossPosition),
                "Loss Position",
                true, true, true
                ));

            columns.Add(new ColumnResponseModel(
                nameof(MachineBlockDataResponseModel.LossDescription),
                "Loss Description",
                true, true, true
                ));

            columns.Add(new ColumnResponseModel(
                nameof(MachineBlockDataResponseModel.LossGroup),
                "Loss Group",
                true, true, true
                ));

            columns.Add(new ColumnResponseModel(
                nameof(MachineBlockDataResponseModel.Note),
                nameof(MachineBlockDataResponseModel.Note),
                false, true, true
                ));

            return columns;
        }
    }
}
