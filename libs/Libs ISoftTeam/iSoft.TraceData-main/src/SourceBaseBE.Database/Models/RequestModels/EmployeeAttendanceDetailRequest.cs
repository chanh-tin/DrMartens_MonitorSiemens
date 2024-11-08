using iSoft.Common.Enums;
using iSoft.Common.Models.RequestModels;
using Newtonsoft.Json;
using SourceBaseBE.Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SourceBaseBE.Database.Models.RequestModels.Report
{
	public class EmployeeAttendanceDetailRequest : PagingFilterRequestModel
	{
		[Column("employee_id")]
		public long? EmployeeId { get; set; }
	}
	public class ExportDepartmentAttendanceRequest
	{
		[Column("list_department")]
		public string? List_Department { get; set; }
		[JsonProperty("dateFrom")]
		public DateTime DateFrom { get; set; }
		[JsonProperty("dateTo")]
		public DateTime DateTo { get; set; }
		public List<long>? ListDepartmentId => List_Department?.Split(",")?.Select(x => long.Parse(x))?.ToList();
	}
	public class ExportEmployeeAttendanceRequest
	{
		[Column("list_employees")]
		public string? List_Employees { get; set; }
		[JsonProperty("dateFrom")]
		public DateTime DateFrom { get; set; }
		[JsonProperty("dateTo")]
		public DateTime DateTo { get; set; }
		public List<long>? ListEmployeeId => List_Employees?.Split(",")?.Select(x => long.Parse(x))?.ToList();
	}
	public class EmployeeAttendanceEditRequest
	{
		[Column("employee_id")]
		public long? EmployeeId { get; set; }
		[Column("workday_id")]
		public long? WorkdayId { get; set; }
		[Column("date")]
		public DateTime Date { get; set; }
		[Column("time_in")]
		public DateTime? TimeIn { get; set; }
		[Column("time_out")]
		public DateTime? TimeOut { get; set; }
		[Column("time_deviation")]
		public long? TimeDeviation { get; set; }
		[Column("status")]
		public EnumWorkingDayStatus Status { get; set; }
		[Column("working_type_code")]
		public string WorkingTypeCode { get; set; }
		[Column("note")]
		public string? Note { get; set; }
		[Column("update_reason")]
		public string? Update_Reason { get; set; }

	}
}
