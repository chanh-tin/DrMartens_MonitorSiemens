using iSoft.Common.Enums;
using iSoft.Common.Models.RequestModels;
using Newtonsoft.Json;
using SourceBaseBE.Database.Entities;
using SourceBaseBE.Database.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SourceBaseBE.MainService.Models.RequestModels.Report
{
	public class AprroveRequest : PagingFilterRequestModel
	{
		[JsonProperty("departmentId")]
		public int? DepartmentId { get; set; }
	}
	public class PersonalPendingRequest : PagingFilterRequestModel
	{
		[JsonProperty("employeeeId")]
		public long? EmployeeId { get; set; }
	}
	public class EditPersonPendingRequest
	{
		[Column("list_workingdayupdateid")]
		public List<long>? ListWorkingDayUpdateId { get; set; }
		public long? UserId { get; set; }
		[JsonProperty("status")]
		public EnumApproveStatus? Status { get; set; }
		[JsonProperty("note")]
		public string? Note { get; set; }
		[Column("approve_reason")]
		public string? ApproveReason { get; set; }
	}
	public class ExportEmployeePendingRequest : PagingFilterRequestModel
	{
		public long? EmployeeId { get; set; }

	}
}
