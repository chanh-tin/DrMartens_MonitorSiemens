﻿using iSoft.Common.Models.RequestModels;
using iSoft.Database.Models.ResponseModels;
using SourceBaseBE.Database.Entities;

namespace SourceBaseBE.MainService.Models.RequestModels.Report
{
	public class CRUDReportRequestPayload
	{
		public long? Id { get; set; }
		public long? EmployeeId { get; set; }
	}
	public class CRUDTimesheetRequestPayload
	{
		public long? Id { get; set; }
		public long? WorkingdayId { get; set; }
		public long? EmployeeId { get; set; }
	}
	public class TimeSheetListRequest : PagingFilterRequestModel
	{
		public long? WorkingdayId { get; set; }
    public long? EmployeeId { get; set; }
  }
}
