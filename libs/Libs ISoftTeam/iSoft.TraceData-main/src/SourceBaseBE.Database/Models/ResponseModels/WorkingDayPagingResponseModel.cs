using iSoft.Common.Models.ResponseModel;
using Newtonsoft.Json;
using SourceBaseBE.Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceBaseBE.Database.Models.ResponseModels
{
    public class WorkingDayPagingResponseModel : PagingWithColumnsResponseModel
	{
		[JsonProperty("listData", NullValueHandling = NullValueHandling.Ignore)]
		public new List<DashboardResponseModel> ListData { get; set; }
	}
	public class DetailAttendancePagingResponseModel : PagingWithColumnsResponseModel
	{
		[JsonProperty("listData", NullValueHandling = NullValueHandling.Ignore)]
		public new List<DetailAttendanceResponse> ListData { get; set; }
		[JsonProperty("listIncommingData", NullValueHandling = NullValueHandling.Ignore)]
		public new List<DetailAttendanceResponse> ListIncomingData { get; set; }
		[JsonIgnore]
		[Browsable(false)]
		public List<WorkingDayEntity> rawDatas { get; set; }
	}
	public class EmployeePagingResponseModel : PagingWithColumnsResponseModel
	{
		[JsonProperty("listData", NullValueHandling = NullValueHandling.Ignore)]
		public new List<EmployeeListAttendanceResponseModel> ListData { get; set; }
		[JsonProperty("rawDatas", NullValueHandling = NullValueHandling.Ignore)]
		public List<EmployeeEntity> rawDatas { get; set; }
	}
	public class EmployeeDepartmentPagingResponseModel : PagingWithColumnsResponseModel
	{
		[JsonProperty("listData", NullValueHandling = NullValueHandling.Ignore)]
		public new List<DepartmentListEmployeeResponseModel> ListData { get; set; }
	}

	public class AdminDepartmentPagingResponseModel : PagingWithColumnsResponseModel
	{
		[JsonProperty("listData", NullValueHandling = NullValueHandling.Ignore)]
		public new List<DepartmentAdminListResponseModel> ListData { get; set; }
	}
	public class WorkingTypePagingResponseModel : PagingWithColumnsResponseModel
	{
		[JsonProperty("listData", NullValueHandling = NullValueHandling.Ignore)]
		public new List<WorkingTypeListReponseModel> ListData { get; set; }
	}

	public class UserDepartmentPagingResponseModel : PagingWithColumnsResponseModel
	{
		[JsonProperty("listData", NullValueHandling = NullValueHandling.Ignore)]
		public new List<UserDepartmentResponseModel> ListData { get; set; }
	}

}
