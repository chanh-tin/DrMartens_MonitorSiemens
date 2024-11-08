using iSoft.Common.Utils;
using SourceBaseBE.Database.Entities;
using static iSoft.Common.ConstCommon;
using iSoft.Common.Enums;
using SourceBaseBE.Database.Enums;
using iSoft.Common.Models.ResponseModels;
using SourceBaseBE.Database.ExtensionMethods;
using iSoft.Common.Models.RequestModels;

using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace SourceBaseBE.Database.Models.ResponseModels
{
    public class OeePointResponseModel : BaseOeePointResponseModel
    {
        public string? OeePoint { get; set; }
        public override string? Note { get; set; }
        public override string? Description { get; set; }
        public override string? TagNames { get; set; }
        public string? Line { get; set; }
        public long? LineId { get; set; }
        public List<long>? MachineIds { get; set; }
        public long? TimeOperation { get; set; }
        public long? PlanedDownTime { get; set; }
        public long? UnplanedDownTime { get; set; }
        public override int? IdealCycleTime { get; set; }
        public override int? IdealRunRate { get; set; }
        public string? PerformanceFormula { get; set; }
        public string? AvaiableFormula { get; set; }
        public string? QualityFormula { get; set; } 
        public string? TotalCountTag { get; set; }
        public string? TotalNGTag { get; set; } 
        public string? OeeTag { get; set; }
        public string? PerformanceTag { get; set; }
        public string? AvaiableTag { get; set; }
        public string? QualityTag { get; set; }

        public OeePointResponseModel SetData(OeePointEntity entity, PagingFilterRequestModel request)
        {
            this.Id = entity.Id;
            this.OeePoint = entity.Name;
            this.Note = entity.Note;
            this.Description = entity.Description;
            this.TagNames = entity.TagNames;
            this.Line = entity.ItemLine?.Name;
            this.LineId = entity.LineId;

            if(entity.ListMachine != null)
            {
                this.MachineIds = entity.ListMachine.Select(x => x.Id).ToList();
            }

            this.IdealRunRate = entity.IdealRunRate;
            this.IdealCycleTime = entity.IdealCycleTime;
            this.AvaiableFormula = "((TotalRunTime - TotalDowntime) / TotalRunTime) * 100";
            this.PerformanceFormula = "(TotalCountInTag / TotalRunTime) / IdealRunRate * 100";
            this.QualityFormula = "(TotalGoodCountTag / TotalCountInTag) * 100";

            if(entity.ItemOeePointConfig != null)
            {
                this.TotalCountTag = entity.ItemOeePointConfig.TotalCountInTag;
                this.TotalNGTag = entity.ItemOeePointConfig.TotalNGCountTag;
                this.OeeTag = entity.ItemOeePointConfig.OeeTag;
                this.PerformanceTag = entity.ItemOeePointConfig.PerformanceTag;
                this.AvaiableTag = entity.ItemOeePointConfig.AvaiableTag;
                this.QualityTag = entity.ItemOeePointConfig.QualityTag;
            }
            var startDateTime = request.DateFrom;
            var endDateTime = request.DateTo;

            long duration = 0;
            long timeOperation = 0;
            long unplannedDownTime = 0;
            long plannedDownTime = 0;

            foreach (var block in entity?.ListMachineBlockData?.Where(x => x.StartDateTime != null && x.DeletedFlag != null))
            {
                var blockStart = block.StartDateTime.Value;
                var blockEnd = block.EndDateTime;
                if (block.EndDateTime == null)
                {
                    //Todo : check lại check null End Datetime
                }
                if (blockEnd < startDateTime || blockStart > endDateTime)
                {
                    continue;
                }

                // Trừ thời gian dư nếu cần
                var durationInMiliSeconds = block.DurationInMiliSeconds;

                if (blockStart < startDateTime)
                {
                    var extraTime = (startDateTime - block.StartDateTime)?.TotalMilliseconds ?? 0;
                    durationInMiliSeconds -= (long)extraTime;
                }
                if (blockEnd > endDateTime)
                {
                    var extraTime = (block.EndDateTime - endDateTime)?.TotalMilliseconds ?? 0;
                    durationInMiliSeconds -= (long)extraTime;
                }

                // Cập nhật thời gian dựa trên biến durationInMiliSeconds đã điều chỉnh
                duration = durationInMiliSeconds.Value > 0 ? durationInMiliSeconds.Value : 0;

                if (block.MachineStatus == EnumMachineStatus.RunGood || block.MachineStatus == EnumMachineStatus.RunNG)
                {
                    timeOperation += (long)duration;
                }
                else if (block.MachineStatus == EnumMachineStatus.StopUnplanned)
                {
                    unplannedDownTime += (long)duration;
                }
                else if (block.MachineStatus == EnumMachineStatus.StopPlanned)
                {
                    plannedDownTime += (long)duration;
                }
            }

            this.TimeOperation = timeOperation;
            this.UnplanedDownTime = unplannedDownTime;
            this.PlanedDownTime = plannedDownTime;

            return this;
        }

        public List<object> SetData(List<OeePointEntity> listEntity, PagingFilterRequestModel request)
        {
            List<Object> listRS = new List<object>();
            // TODO: result.Copy()???
            //foreach (OeePointEntity entity in listEntity)
            //{
            //    var result = SetData(entity, request);
            //    listRS.Add(result.Copy());
            //}
            return listRS;
        }
        public IQueryable<OeePointEntity> PrepareWhereQueryFilter(
            IQueryable<OeePointEntity> query,
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

                if (inputFieldKey == nameof(OeePointResponseModel.Line))
                {
                    query = query.Where(x => x.LineId == (long)searchValue);
                }

               
                //var propertyInfo = responseType.GetProperty(inputFieldKey);
                //if (propertyInfo == null)
                //{
                //    continue;
                //}

                //query = query.WherePropertyEquals(inputFieldKey, searchValue);
            }
            return query;
        }

        public IQueryable<OeePointEntity> PrepareQuerySort(
            IQueryable<OeePointEntity> query,
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
        public IQueryable<OeePointEntity> PrepareWhereQuerySearch(
            IQueryable<OeePointEntity> query,
            Dictionary<string, string> dicInputFieldKey2Value,
            Func<Dictionary<string, string>> getFieldAttributes,
            Func<Dictionary<string, string>> getFieldAttributesSearchAll,
            Type responseType)
        {
            var dicFieldAttr = getFieldAttributes();
            var dicFieldAttrSearchAll = getFieldAttributesSearchAll();
            var predicate = LinqKit.PredicateBuilder.New<OeePointEntity>(true);
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

                    //var propertyInfo = responseType.GetProperty(inputFieldKey);
                    //if (propertyInfo == null)
                    //{
                    //    continue;
                    //}

                    predicate = this.GetPredicate(predicate, inputFieldKey, searchValue);
                }
            }
            return query.Where(predicate);
        }

        public override ExpressionStarter<OeePointEntity> GetPredicate(ExpressionStarter<OeePointEntity> predicate, string inputFieldKey, string searchValue)
        {
            if (inputFieldKey == nameof(OeePointResponseModel.OeePoint))
            {
                predicate = predicate.Or(x => EF.Functions.Unaccent(x.Name.ToLower()).Contains(EF.Functions.Unaccent($"{searchValue.ToLower()}")));
            }
            else if (inputFieldKey == nameof(OeePointResponseModel.Line))
            {
                predicate = predicate.Or(x => EF.Functions.Unaccent(x.ItemLine.Name.ToLower()).Contains(EF.Functions.Unaccent($"{searchValue.ToLower()}")));
            }
            else if (inputFieldKey == nameof(OeePointResponseModel.Note))
            {
                predicate = predicate.Or(x => EF.Functions.Unaccent(x.Note.ToLower()).Contains(EF.Functions.Unaccent($"{searchValue.ToLower()}")));
            }
            else if (inputFieldKey == nameof(OeePointResponseModel.Description))
            {
                predicate = predicate.Or(x => EF.Functions.Unaccent(x.Description.ToLower()).Contains(EF.Functions.Unaccent($"{searchValue.ToLower()}")));
            }
            else if (inputFieldKey == nameof(OeePointResponseModel.TagNames))
            {
                predicate = predicate.Or(x => EF.Functions.Unaccent(x.TagNames.ToLower()).Contains(EF.Functions.Unaccent($"{searchValue.ToLower()}")));
            }
            else if (inputFieldKey == nameof(OeePointResponseModel.TotalCountTag))
            {
                predicate = predicate.Or(x => EF.Functions.Unaccent(x.ItemOeePointConfig.TotalCountInTag.ToLower()).Contains(EF.Functions.Unaccent($"{searchValue.ToLower()}")));
            }
            else if (inputFieldKey == nameof(OeePointResponseModel.TotalNGTag))
            {
                predicate = predicate.Or(x => EF.Functions.Unaccent(x.ItemOeePointConfig.TotalNGCountTag.ToLower()).Contains(EF.Functions.Unaccent($"{searchValue.ToLower()}")));
            }
            return predicate;
        }

        public override Dictionary<string, string> GetFieldAttributes()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(nameof(OeePointResponseModel.Line), typeof(long).ToString());
            dic.Add(nameof(OeePointResponseModel.OeePoint), typeof(string).ToString());
            dic.Add(nameof(OeePointResponseModel.TagNames), typeof(string).ToString());
            dic.Add(nameof(OeePointResponseModel.TotalCountTag), typeof(string).ToString());
            dic.Add(nameof(OeePointResponseModel.TotalNGTag), typeof(string).ToString());
            dic.Add(nameof(OeePointResponseModel.IdealRunRate), typeof(long).ToString());
            dic.Add(nameof(OeePointResponseModel.Description), typeof(string).ToString());
            return dic;
        }

        public override Dictionary<string, string> GetFieldAttributesSearchAll()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(nameof(OeePointResponseModel.Line), typeof(string).ToString());
            dic.Add(nameof(OeePointResponseModel.OeePoint), typeof(string).ToString());
            dic.Add(nameof(OeePointResponseModel.TagNames), typeof(string).ToString());
            dic.Add(nameof(OeePointResponseModel.TotalNGTag), typeof(string).ToString());
            dic.Add(nameof(OeePointResponseModel.TotalCountTag), typeof(string).ToString());

            return dic;
        }

        public override List<ColumnResponseModel> GetColumnAttribute()
        {
            List<ColumnResponseModel> columns = new List<ColumnResponseModel>();

            columns.Add(new ColumnResponseModel(
                nameof(OeePointResponseModel.Line),
                nameof(OeePointResponseModel.Line),
                true, true, true
                ));


            columns.Add(new ColumnResponseModel(
                nameof(OeePointResponseModel.OeePoint),
                nameof(OeePointResponseModel.OeePoint),
                true, false, true
                ));

            columns.Add(new ColumnResponseModel(
                nameof(OeePointResponseModel.Note),
                nameof(OeePointResponseModel.Note),
                true, false, true
                ));
           
            columns.Add(new ColumnResponseModel(
                nameof(OeePointResponseModel.TagNames),
                "Tag Names",
                true, false, true
                ));

            columns.Add(new ColumnResponseModel(
               nameof(OeePointResponseModel.Description),
               nameof(OeePointResponseModel.Description),
               true, false, true
               ));

            columns.Add(new ColumnResponseModel(
                nameof(OeePointResponseModel.IdealRunRate),
                "Ideal RunRate",
                false, false, true
                ));

            columns.Add(new ColumnResponseModel(
               nameof(OeePointResponseModel.TotalCountTag),
               nameof(OeePointResponseModel.TotalCountTag),
               false, false, true
               ));

            columns.Add(new ColumnResponseModel(
                nameof(OeePointResponseModel.TotalNGTag),
                nameof(OeePointResponseModel.TotalNGTag),
                false, false, true
                ));

            return columns;
        }
    }
}
