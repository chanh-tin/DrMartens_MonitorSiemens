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
    public class BaseMachineBlockDataResponseModel : BaseCRUDResponseModel<MachineBlockDataEntity>
    {
        public virtual long? LineId { get; set; }
        public virtual EnumMachineStatus? MachineStatus { get; set; }
        public virtual DateTime? StartDateTime { get; set; }
        public virtual DateTime? EndDateTime { get; set; }
        public virtual long? DurationInMiliSeconds { get; set; }
        public virtual long? BlockCountIn { get; set; }
        public virtual long? BlockGoodCount { get; set; }
        public virtual long? BlockNGCount { get; set; }
        public virtual long? LastCountIn { get; set; }
        public virtual long? LastGoodCount { get; set; }
        public virtual long? LastNGCount { get; set; }
        public virtual string? LastMessageId { get; set; }
        public virtual DateTime? LastReceivedTime { get; set; }
        public virtual long? MachineLossId { get; set; }
        public virtual MachineLossEntity? ItemMachineLoss { get; set; }
        public virtual long? MachineLossPositionId { get; set; }
        public virtual MachineLossPositionEntity? ItemMachineLossPosition { get; set; }
        public virtual long? MachineLossDescribeId { get; set; }
        public virtual MachineLossDescribeEntity? ItemMachineLossDescribe { get; set; }
        public virtual long? OeePointId { get; set; }
        public virtual OeePointEntity? ItemOeePoint { get; set; }
        
        public override object SetData(MachineBlockDataEntity entity)
        {
            base.SetData(entity);
            this.LineId = entity.LineId;
            this.MachineStatus = entity.MachineStatus;
            this.StartDateTime = entity.StartDateTime;
            this.EndDateTime = entity.EndDateTime;
            this.DurationInMiliSeconds = entity.DurationInMiliSeconds;
            this.BlockCountIn = entity.BlockCountIn;
            this.BlockGoodCount = entity.BlockGoodCount;
            this.BlockNGCount = entity.BlockNGCount;
            this.LastCountIn = entity.LastCountIn;
            this.LastGoodCount = entity.LastGoodCount;
            this.LastNGCount = entity.LastNGCount;
            this.LastMessageId = entity.LastMessageId;
            this.LastReceivedTime = entity.LastReceivedTime;
            this.MachineLossId = entity.MachineLossId;
            this.ItemMachineLoss = entity.ItemMachineLoss;
            this.MachineLossPositionId = entity.MachineLossPositionId;
            this.ItemMachineLossPosition = entity.ItemMachineLossPosition;
            this.MachineLossDescribeId = entity.MachineLossDescribeId;
            this.ItemMachineLossDescribe = entity.ItemMachineLossDescribe;
            this.OeePointId = entity.OeePointId;
            this.ItemOeePoint = entity.ItemOeePoint;
        
            return this;
        }
        public override List<object> SetData(List<MachineBlockDataEntity> listEntity)
        {
            List<Object> listRS = new List<object>();
            foreach (MachineBlockDataEntity entity in listEntity)
            {
                listRS.Add(new BaseMachineBlockDataResponseModel().SetData(entity));
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

                query = query.WherePropertyEquals(inputFieldKey, searchValue);
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

        public virtual ExpressionStarter<MachineBlockDataEntity> GetPredicate(ExpressionStarter<MachineBlockDataEntity> predicate, string inputFieldKey, string searchValue)
        {
            if (inputFieldKey == nameof(BaseMachineBlockDataResponseModel.Id))
            {
                predicate = predicate.Or(x => EF.Functions.Unaccent(x.Id.ToString().ToLower()).Contains(EF.Functions.Unaccent($"{searchValue.ToLower()}")));
            }
            else if (inputFieldKey == nameof(BaseMachineBlockDataResponseModel.Order))
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
                nameof(BaseMachineBlockDataResponseModel.Id),
                nameof(BaseMachineBlockDataResponseModel.Id),
                true, true, true
                ));

            return columns;
        }
    }
}
