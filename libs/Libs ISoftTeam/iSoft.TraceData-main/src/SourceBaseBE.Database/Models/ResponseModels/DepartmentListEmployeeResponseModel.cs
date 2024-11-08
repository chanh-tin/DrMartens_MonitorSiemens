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

namespace SourceBaseBE.Database.Models.ResponseModels
{
	public class DepartmentListEmployeeResponseModel
	{
		[DisplayName("Id")]
		[JsonProperty("Id")]
		public long? Id { get; set; }

		[DisplayName("Name")]
		[JsonProperty("name")]
		public string? Name { get; set; }
		[DisplayName("Employee Code")]
		[JsonProperty("employeecode")]
		public string? EmployeeCode { get; set; }
		[DisplayName("Employee Machine Code")]
		[JsonProperty("employeemachinecode")]
		public string? EmployeeMachineCode { get; set; }
		[DisplayName("Phone Number")]
		[JsonProperty("phone")]
		public string? Phone { get; set; }
		[DisplayName("Department")]
		[JsonProperty("department")]
		public string? Department { get; set; }

		[JsonProperty("jobtitle")]
		[DisplayName("JobTitle")]
		public string? JobTitle { get; set; }

		[JsonProperty("email")]
		[DisplayName("Email")]
		public string? Email { get; set; }



		public DepartmentListEmployeeResponseModel SetData(EmployeeEntity entity)
		{
			if (entity == null) throw new ArgumentNullException("Param Entity DashboardResponseModel Null");
			this.Name = entity?.Name;
			this.EmployeeCode = entity?.EmployeeCode;
			this.EmployeeMachineCode = entity?.EmployeeMachineCode;
			this.Phone = entity?.PhoneNumber;
			this.Department = entity?.Department?.Name;
			this.JobTitle = entity?.JobTitle?.Name;
			this.Email = entity?.Email;
			this.Id = entity?.Id;
			return this;
		}

		public static List<ColumnResponseModel> AddKeySearchFilterable(List<ColumnResponseModel> datas)
		{
			var properties = JsonPropertyHelper<DepartmentListEmployeeResponseModel>.GetJsonPropertyNames();

			foreach (var data in datas)
			{
				if (properties.Contains(data.Key))
				{
					string key = data.Key.ToLower();
					//* add flag_filterable
					if (key == nameof(DepartmentListEmployeeResponseModel.JobTitle).ToLower()
					  || key == nameof(DepartmentListEmployeeResponseModel.Department).ToLower())
						data.Filterable = true;
					//* add flag_searchable
					if (key == nameof(DepartmentListEmployeeResponseModel.Name).ToLower()
					  || key == nameof(DepartmentListEmployeeResponseModel.Department).ToLower()
					  || key == nameof(DepartmentListEmployeeResponseModel.JobTitle).ToLower()
					  || key == nameof(DepartmentListEmployeeResponseModel.EmployeeCode).ToLower())
						data.Searchable = true;
				}
			}

			return datas;
		}

		//* prepare query list with where clause
		public static IQueryable<EmployeeEntity> PrepareWhereQueryFilter(IQueryable<EmployeeEntity> query, Dictionary<string, object> param)
		{
			var properties = JsonPropertyHelper<DepartmentListEmployeeResponseModel>.GetJsonPropertyNames();
			properties.RemoveAll(p => p == null);

			var predicate = LinqKit.PredicateBuilder.New<EmployeeEntity>(true); // Sử dụng thư viện linqkit
			foreach (var property in properties)
			{
				string key = property.ToLower();
				if (param.ContainsKey(key))
				{
					if (key == "jobtitle")
					{
						query = query.Where(x => x.JobTitleId == long.Parse(param[key].ToString()));
					}
				}
			}
			return query.Where(predicate);
		}

		public static IQueryable<EmployeeEntity> PrepareWhereQuerySearch(IQueryable<EmployeeEntity> query, SearchModel searchModel)
		{
			var predicate = LinqKit.PredicateBuilder.New<EmployeeEntity>(true); // Sử dụng thư viện linqkit
			var dicSearch = searchModel.DicSearch;
			var searchKey = searchModel.SearchStr?.Trim()?.ToLower();
			foreach (var search in dicSearch)
			{
				string key = search.Key.ToLower();
				var searchVal = dicSearch[key].Trim();
				if (key == "name")
				{
					predicate = predicate.And(x => EF.Functions.Unaccent(x.Name.ToLower()).Contains(EF.Functions.Unaccent($"{searchVal}")));
				}
				else if (key == "employeecode")
				{
					predicate = predicate.And(x => EF.Functions.Unaccent(x.EmployeeCode.ToLower()).Contains(EF.Functions.Unaccent($"{searchVal}")));
				}
				else if (key == "employeemachinecode")
				{
					predicate = predicate.And(x => EF.Functions.Unaccent(x.EmployeeMachineCode.ToLower()).Contains(EF.Functions.Unaccent($"{searchVal}")));
				}
				else if (key == "jobtitle")
				{
					predicate = predicate.And(x => EF.Functions.Unaccent(x.JobTitle.Name.ToLower()).Contains(EF.Functions.Unaccent($"{searchVal}")));
				}
				else if (key == "phone")
				{
					predicate = predicate.And(x => EF.Functions.Unaccent(x.PhoneNumber.ToLower()).Contains(EF.Functions.Unaccent($"{searchVal}")));
				}
				if (key == "all")
				{
					predicate = predicate.Or(x => EF.Functions.Unaccent(x.Name.ToLower()).Contains(EF.Functions.Unaccent($"{searchVal.ToLower()}")));
					predicate = predicate.Or(x => EF.Functions.Unaccent(x.EmployeeCode.ToLower()).Contains(EF.Functions.Unaccent($"{searchVal.ToLower()}")));
					predicate = predicate.Or(x => EF.Functions.Unaccent(x.EmployeeMachineCode.ToLower()).Contains(EF.Functions.Unaccent($"{searchVal.ToLower()}")));
					predicate = predicate.Or(x => EF.Functions.Unaccent(x.PhoneNumber.ToLower()).Contains(EF.Functions.Unaccent($"{searchVal.ToLower()}")));
					predicate = predicate.Or(x => EF.Functions.Unaccent(x.Department.Name.ToLower()).Contains(EF.Functions.Unaccent($"{searchVal.ToLower()}")));
					predicate = predicate.Or(x => EF.Functions.Unaccent(x.JobTitle.Name.ToLower()).Contains(EF.Functions.Unaccent($"{searchVal.ToLower()}")));
				}
			}
			if (!string.IsNullOrWhiteSpace(searchKey))
			{
				predicate = predicate.Or(x => EF.Functions.Unaccent(x.Name.ToLower()).Contains(EF.Functions.Unaccent($"{searchKey.ToLower()}")));
				predicate = predicate.Or(x => EF.Functions.Unaccent(x.EmployeeCode.ToLower()).Contains(EF.Functions.Unaccent($"{searchKey.ToLower()}")));
				predicate = predicate.Or(x => EF.Functions.Unaccent(x.EmployeeMachineCode.ToLower()).Contains(EF.Functions.Unaccent($"{searchKey.ToLower()}")));
				predicate = predicate.Or(x => EF.Functions.Unaccent(x.PhoneNumber.ToLower()).Contains(EF.Functions.Unaccent($"{searchKey.ToLower()}")));
				predicate = predicate.Or(x => EF.Functions.Unaccent(x.Department.Name.ToLower()).Contains(EF.Functions.Unaccent($"{searchKey.ToLower()}")));
				predicate = predicate.Or(x => EF.Functions.Unaccent(x.JobTitle.Name.ToLower()).Contains(EF.Functions.Unaccent($"{searchKey.ToLower()}")));
			}
			return query.Where(predicate).AsQueryable();
		}

		public static IQueryable<EmployeeEntity> PrepareQuerySort(IQueryable<EmployeeEntity> query, Dictionary<string, long> param)
		{
			var properties = JsonPropertyHelper<DepartmentListEmployeeResponseModel>.GetJsonPropertyNames();
			properties.RemoveAll(p => p == null);
			foreach (var pa in param)
			{
				if (pa.Key == "department")
				{
					query = pa.Value == -1 ? query.OrderByDescending(x => x.Department.Name) : query.OrderBy(x => x.Department.Name);
				}
				else if (pa.Key == "jobtitle")
				{
					query = pa.Value == -1 ? query.OrderByDescending(x => x.JobTitle.Name) : query.OrderBy(x => x.JobTitle.Name);
				}
				else if (pa.Key == "employeecode")
				{
					query = pa.Value == -1 ? query.OrderByDescending(x => x.EmployeeCode) : query.OrderBy(x => x.EmployeeCode);
				}
				else if (pa.Key == "employeemachinecode")
				{
					query = pa.Value == -1 ? query.OrderByDescending(x => x.EmployeeMachineCode) : query.OrderBy(x => x.EmployeeMachineCode);
				}
				else if (pa.Key == "name")
				{
					query = pa.Value == -1 ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
				}
				else if (pa.Key == "phone")
				{
					query = pa.Value == -1 ? query.OrderByDescending(x => x.PhoneNumber) : query.OrderBy(x => x.PhoneNumber);
				}
			}
			return query;
		}

	}

}
