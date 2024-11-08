using iSoft.Common.Utils;
using SourceBaseBE.Database.Entities;
using static iSoft.Common.ConstCommon;
using iSoft.Common.Enums;
using SourceBaseBE.Database.Enums;
using Newtonsoft.Json;
using PRPO.Database.Helpers;
using PRPO.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using iSoft.Common.Models.ResponseModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using System.Linq.Dynamic.Core;

using System;
using LinqKit;
using iSoft.Common.Enums;
using SourceBaseBE.Database.Models.RequestModels;

using System.ComponentModel;
using System.Dynamic;
using SourceBaseBE.Database.Attribute;

using System.Data.Entity;
using System.Xml.Linq;
namespace SourceBaseBE.Database.Models.ResponseModels
{
    public class DetailWorkingdayUpdateDTO
    {
        [JsonProperty("workingdayupdateid")]
        public long? Id { get; set; }
        [DisplayName("Date")]
        [JsonProperty("date")]
        public DateTime? Date { get; set; }
        [DisplayName("Requester")]
        [JsonProperty("requester")]
        [FilterableAttribute("Requester", "requester", true)]
        public string? Requester { get; set; }
        [JsonProperty("time_in")]
        [DisplayName("Time In")]
        public string? TimeIn { get; set; }
        [JsonProperty("origin_time_in")]
        public string? OriginTimeIn { get; set; }
        [JsonProperty("time_out")]
        [DisplayName("TimeOut")]
        public string? TimeOut { get; set; }
        [JsonProperty("origin_time_out")]
        public string? OriginTimeOut { get; set; }
        [JsonProperty("time_deviation")]
        [DisplayName("Time Deviation")]
        public long? TimeDeviation { get; set; }
        [JsonProperty("origin_time_deviation")]
        public double? OriginTimeDeviation { get; set; }
        [JsonProperty("origin_status")]
        public string? OriginWorkingDayStatus { get; set; }
        [JsonProperty("origin_type")]
        public string? OriginWorkingType { get; set; }
        [JsonProperty("status")]
        [DisplayName("EnableFlag")]
        public string? Status { get; set; }
        [JsonProperty("type")]
        [DisplayName("Type")]
        public string? Type { get; set; }
        [JsonProperty("note")]
        [DisplayName("Note")]
        public string? Note { get; set; }
        [JsonProperty("requestat")]
        [DisplayName("Request At")]
        public DateTime? RequestAt { get; set; }
        [FilterableAttribute("Request EnableFlag", "request_status")]
        [JsonProperty("request_status")]
        public EnumApproveStatus? ApproveStatus { get; set; }
        public static DetailWorkingdayUpdateDTO SetData(WorkingDayUpdateEntity workingDayUpdate)
        {
            if (workingDayUpdate == null) throw new ArgumentNullException("DetailWorkingdayUpdateDTO Input Parameter  Null");
            var dto = new DetailWorkingdayUpdateDTO();
            dto.Date = workingDayUpdate.WorkingDate;
            dto.TimeDeviation = workingDayUpdate.Time_Deviation;
            dto.TimeIn = workingDayUpdate.Time_In?.ToString("MM/dd/yyyy HH:mm:ss");
            dto.TimeOut = workingDayUpdate.Time_Out?.ToString("MM/dd/yyyy HH:mm:ss");
            dto.Requester = workingDayUpdate?.Editer?.ItemEmployee?.Name;
            dto.Status = workingDayUpdate.WorkingDayStatus.ToString();
            dto.Type = workingDayUpdate.WorkingType?.Name;
            dto.OriginTimeIn = workingDayUpdate?.OriginTimeIn?.ToString("MM/dd/yyyy HH:mm:ss");
            dto.OriginTimeOut = workingDayUpdate?.OriginTimeOut?.ToString("MM/dd/yyyy HH:mm:ss");
            dto.OriginTimeDeviation = workingDayUpdate?.OriginTimeDeviation;
            dto.OriginWorkingDayStatus = workingDayUpdate?.OriginWorkingDayStatus?.ToString();
            dto.OriginWorkingType = workingDayUpdate.OriginWorkingType?.Name;
            dto.Note = workingDayUpdate.Notes;
            dto.Id = workingDayUpdate.Id;
            dto.ApproveStatus = workingDayUpdate.WorkingDayApprovals.OrderByDescending(x => x.UpdatedAt).FirstOrDefault()?.ApproveStatus;
            dto.RequestAt = workingDayUpdate.CreatedAt.GetValueOrDefault();
            return dto;
        }
        public static List<ColumnResponseModel> AddKeySearchFilterable(List<ColumnResponseModel> datas)
        {
            foreach (var data in datas)
            {
                string key = data.Key.ToLower();
                //* add flag_filterable
                if (key == "request_status"
                        || key == nameof(DetailWorkingdayUpdateDTO.Requester).ToLower()
                    )
                    data.Filterable = true;
                //* add flag_searchable
                if (key == nameof(DetailWorkingdayUpdateDTO.Type).ToLower()
              || key == nameof(DetailWorkingdayUpdateDTO.Note).ToLower()
                || key == "request_status"
                || key == nameof(DetailWorkingdayUpdateDTO.Requester).ToLower()
                )
                    data.Searchable = true;
            }

            return datas;
        }

        //* prepare query list with where clause
        public static IQueryable<WorkingDayUpdateEntity> PrepareWhereQueryFilter(IQueryable<WorkingDayUpdateEntity> query, Dictionary<string, object> param)
        {
            var properties = JsonPropertyHelper<DetailWorkingdayUpdateDTO>.GetJsonPropertyNames();
            var predicate = LinqKit.PredicateBuilder.New<WorkingDayUpdateEntity>(true); // Sử dụng thư viện linqkit
            foreach (var property in properties)
            {
                string key = property.ToLower();
                if (!param.ContainsKey(key)) continue;
                if (key == JsonPropertyHelper<DetailWorkingdayUpdateDTO>.GetJsonPropertyName(nameof(DetailWorkingdayUpdateDTO.Type))?.ToLower())
                {
                    predicate.And(x => x.WorkingTypeId == long.Parse(param[key].ToString()));
                }
                else if (key == JsonPropertyHelper<DetailWorkingdayUpdateDTO>.GetJsonPropertyName(nameof(DetailWorkingdayUpdateDTO.Requester))?.ToLower())
                {
                    predicate.And(x => x.Editer.EmployeeId == long.Parse(param[key].ToString()));
                }
                else if (key == JsonPropertyHelper<DetailWorkingdayUpdateDTO>.GetJsonPropertyName(nameof(DetailWorkingdayUpdateDTO.ApproveStatus))?.ToLower())
                {
                    var value = param[key].ToString()?.Split(",");
                    var predicateStatus = LinqKit.PredicateBuilder.New<WorkingDayUpdateEntity>(true); // Sử dụng thư viện linqkit
                    if (value.Length > 0)
                    {
                        foreach (var val in value)
                        {
                            predicateStatus.Or(x => x.WorkingDayApprovals.FirstOrDefault().ApproveStatus == (EnumApproveStatus)(int.Parse(val.ToString())));
                        }
                    }
                    predicate.And(predicateStatus);
                }
            }

            return query.Where(predicate);
        }

        public static IQueryable<WorkingDayUpdateEntity> PrepareWhereQuerySearch(IQueryable<WorkingDayUpdateEntity> query, SearchModel searchModel)
        {
            var predicate = LinqKit.PredicateBuilder.New<WorkingDayUpdateEntity>(true); // Sử dụng thư viện linqkit
            var dicSearch = searchModel.DicSearch;
            var searchKey = searchModel.SearchStr?.ToLower()?.Trim();
            foreach (var search in dicSearch)
            {
                string key = search.Key.ToLower();
                var searchVal = dicSearch[key]?.ToLower()?.Trim();
                if (key == JsonPropertyHelper<DetailWorkingdayUpdateDTO>.GetJsonPropertyName(nameof(DetailWorkingdayUpdateDTO.Type)).ToLower())
                {
                    predicate = predicate.And(x => EF.Functions.Unaccent(x.WorkingType.Name.ToLower()).Contains(EF.Functions.Unaccent($"{searchVal}")));
                }
                else if (key == JsonPropertyHelper<DetailWorkingdayUpdateDTO>.GetJsonPropertyName(nameof(DetailWorkingdayUpdateDTO.Note)).ToLower())
                {
                    predicate = predicate.And(x => EF.Functions.Unaccent(x.Notes.ToLower()).Contains(EF.Functions.Unaccent($"{searchVal}")));
                }
                else if (key == JsonPropertyHelper<DetailWorkingdayUpdateDTO>.GetJsonPropertyName(nameof(DetailWorkingdayUpdateDTO.ApproveStatus)).ToLower())
                {
                    predicate = predicate.And(x => EF.Functions.Unaccent(x.WorkingDayApprovals.FirstOrDefault().ApproveStatus.ToString().ToLower()).Contains(EF.Functions.Unaccent($"{searchVal}")));
                }
                else if (key == JsonPropertyHelper<DetailWorkingdayUpdateDTO>.GetJsonPropertyName(nameof(DetailWorkingdayUpdateDTO.Requester)).ToLower())
                {
                    predicate = predicate.And(x => EF.Functions.Unaccent(x.Editer.ItemEmployee.Name.ToLower()).Contains(EF.Functions.Unaccent($"{searchVal}")));

                }
                if (key == "all")
                {
                    predicate = predicate.Or(x => EF.Functions.Unaccent(x.WorkingType.Name.ToLower()).Contains(EF.Functions.Unaccent($"{searchVal}")));
                    predicate = predicate.Or(x => EF.Functions.Unaccent(x.Notes.ToLower()).Contains(EF.Functions.Unaccent($"{searchVal}")));
                    predicate = predicate.Or(x => EF.Functions.Unaccent(x.WorkingDayStatus.ToString().ToLower()).Contains(EF.Functions.Unaccent($"{searchVal}")));
                    predicate = predicate.Or(x => EF.Functions.Unaccent(x.Editer.ItemEmployee.Name.ToLower()).Contains(EF.Functions.Unaccent($"{searchVal}")));
                }
            }
            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                predicate = predicate.Or(x => EF.Functions.Unaccent(x.WorkingType.Name.ToLower()).Contains(EF.Functions.Unaccent($"{searchKey}")));
                predicate = predicate.Or(x => EF.Functions.Unaccent(x.Notes.ToLower()).Contains(EF.Functions.Unaccent($"{searchKey}")));
                predicate = predicate.Or(x => EF.Functions.Unaccent(x.WorkingDayStatus.ToString().ToLower()).Contains(EF.Functions.Unaccent($"{searchKey}")));
                predicate = predicate.Or(x => EF.Functions.Unaccent(x.Editer.ItemEmployee.Name.ToLower()).Contains(EF.Functions.Unaccent($"{searchKey}")));
            }
            return query.Where(predicate);
        }

        public static IQueryable<WorkingDayUpdateEntity> PrepareQuerySort(IQueryable<WorkingDayUpdateEntity> query, Dictionary<string, long> param)
        {
            var properties = JsonPropertyHelper<DetailWorkingdayUpdateDTO>.GetJsonPropertyNames();
            properties.RemoveAll(p => p == null);
            foreach (var pa in param)
            {
                if (pa.Key == JsonPropertyHelper<DetailWorkingdayUpdateDTO>.GetJsonPropertyName(nameof(DetailWorkingdayUpdateDTO.Type)).ToLower())
                {
                    query = pa.Value < 0 ? query.OrderByDescending(x => x.WorkingType.Code) : query.OrderBy(x => x.WorkingType.Code);
                }
                else if (pa.Key == JsonPropertyHelper<DetailWorkingdayUpdateDTO>.GetJsonPropertyName(nameof(DetailWorkingdayUpdateDTO.Note)).ToLower())
                {
                    query = pa.Value < 0 ? query.OrderByDescending(x => x.Notes) : query.OrderBy(x => x.Notes);
                }
                else if (pa.Key == JsonPropertyHelper<DetailWorkingdayUpdateDTO>.GetJsonPropertyName(nameof(DetailWorkingdayUpdateDTO.ApproveStatus)).ToLower())
                {
                    query = pa.Value < 0 ? query.OrderByDescending(x => x.WorkingDayApprovals.FirstOrDefault().ApproveStatus) : query.OrderBy(x => x.WorkingDayApprovals.FirstOrDefault().ApproveStatus);
                }
                else if (pa.Key == JsonPropertyHelper<DetailWorkingdayUpdateDTO>.GetJsonPropertyName(nameof(DetailWorkingdayUpdateDTO.Requester)).ToLower())
                {
                    query = pa.Value < 0 ? query.OrderByDescending(x => x.Editer.ItemEmployee.Name) : query.OrderBy(x => x.Editer.ItemEmployee.Name);
                }
                else if (pa.Key == JsonPropertyHelper<DetailWorkingdayUpdateDTO>.GetJsonPropertyName(nameof(DetailWorkingdayUpdateDTO.TimeIn)).ToLower())
                {
                    query = pa.Value < 0 ? query.OrderByDescending(x => x.Time_In != null ? x.Time_In : x.OriginTimeIn) : query.OrderBy(x => x.Time_In != null ? x.Time_In : x.OriginTimeIn);
                }
                else if (pa.Key == JsonPropertyHelper<DetailWorkingdayUpdateDTO>.GetJsonPropertyName(nameof(DetailWorkingdayUpdateDTO.TimeOut)).ToLower())
                {
                    query = pa.Value < 0 ? query.OrderByDescending(x => x.Time_Out != null ? x.Time_Out : x.OriginTimeOut) : query.OrderBy(x => x.Time_Out != null ? x.Time_Out : x.OriginTimeOut);
                }
                else if (pa.Key == JsonPropertyHelper<DetailWorkingdayUpdateDTO>.GetJsonPropertyName(nameof(DetailWorkingdayUpdateDTO.RequestAt)).ToLower())
                {
                    query = pa.Value < 0 ? query.OrderByDescending(x => x.CreatedAt) : query.OrderBy(x => x.CreatedAt);
                }
            }
            return query;
        }

    }

}
