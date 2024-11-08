using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using iSoft.Common.Enums;
using iSoft.Database.Entities;
using iSoft.Database.Extensions;
using Newtonsoft.Json;
using SourceBaseBE.Database.Enums;
using SourceBaseBE.Database.Interfaces;

namespace SourceBaseBE.Database.Entities
{
	[Table("TimeSheets")]
	public class TimeSheetEntity : BaseCRUDEntity
	{

		[Column("status")]
		[DisplayName("Type")]
		[JsonProperty("status")]
		[FormDataTypeAttributeText(EnumFormDataType.Select, null)]
		public EnumFaceId? Status { get; set; }
		[FormDataTypeAttributeDatetime(EnumFormDataType.Select, true)]

		[Column("recorded_time")]
		[DisplayName("Record Time")]
		[JsonProperty("recordedtime")]
		public DateTime? RecordedTime { get; set; }
		[ForeignKey(nameof(EmployeeEntity))]
		[FormDataTypeAttributeText(EnumFormDataType.Hidden)]
		public long? EmployeeId { get; set; }
		public EmployeeEntity? Employee { get; set; }
		[ForeignKey(nameof(WorkingDay))]
		[FormDataTypeAttributeText(EnumFormDataType.Hidden)]
		public long? WorkingDayId { get; set; }
		public WorkingDayEntity? WorkingDay { get; set; }

    public override string ToString()
    {
			return $"{this.RecordedTime}:{this.Status.ToString()}";
    }
  }
}
