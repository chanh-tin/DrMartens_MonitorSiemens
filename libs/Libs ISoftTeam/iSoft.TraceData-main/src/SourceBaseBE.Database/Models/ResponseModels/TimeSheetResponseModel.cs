using iSoft.Common.Utils;
using SourceBaseBE.Database.Entities;
using static iSoft.Common.ConstCommon;
using iSoft.Common.Enums;
using Newtonsoft.Json;
using iSoft.Common.Models.ResponseModel;

namespace SourceBaseBE.Database.Models.ResponseModels
{

    public class TimeSheetPagingResponse : PagingWithColumnsResponseModel
	{
		[JsonProperty("listData", NullValueHandling = NullValueHandling.Ignore)]
		public new List<TimesheetListResponseModel> ListData { get; set; }
	}
}
