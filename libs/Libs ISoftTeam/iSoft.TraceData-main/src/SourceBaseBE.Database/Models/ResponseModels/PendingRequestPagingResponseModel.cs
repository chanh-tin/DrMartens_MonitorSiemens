using iSoft.Common.Models.ResponseModel;
using Newtonsoft.Json;
using SourceBaseBE.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceBaseBE.Database.Models.ResponseModels
{
    public class PendingRequestPagingResponseModel : PagingWithColumnsResponseModel
	{
		[JsonProperty("listData", NullValueHandling = NullValueHandling.Ignore)]
		public new List<WorkingdayUpdateDTO> ListData { get; set; }
	}
	public class PersonalPendingRequestPagingResponseModel : PagingWithColumnsResponseModel
	{
		[JsonProperty("listData", NullValueHandling = NullValueHandling.Ignore)]
		public new List<DetailWorkingdayUpdateDTO> ListData { get; set; }
		[JsonProperty("employeeInfo", NullValueHandling = NullValueHandling.Ignore)]
		public List<Dictionary<string, object>> EmployeeInformation { get; set; }
	}
}
