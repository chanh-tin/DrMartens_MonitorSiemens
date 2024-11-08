using iSoft.Common.Enums;
using iSoft.Database.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SourceBaseBE.Database.Entities
{
	[Table("WorkingDayUpdates")]
	public class WorkingDayUpdateEntity : BaseCRUDEntity
	{
		// [FormDataTypeAttributeSelect(EnumFormDataType.Select, false)]
		[DisplayName("Employee")]
		public long? EmployeeId { get; set; }
		[ForeignKey(nameof(WorkingDayEntity))]
		// [FormDataTypeAttributeNumber(EnumFormDataType.Hidden, false)]
		[DisplayName("WorkingDayId")]
		public long? WorkingDayId { get; set; }
		public WorkingDayEntity? WorkingDay { get; set; }
		[ForeignKey(nameof(UserEntity))]
		// [FormDataTypeAttributeNumber(EnumFormDataType.Hidden, true)]
		[Browsable(false)]
		[DisplayName("EditerId")]
		public long? EditerId { get; set; }
		public UserEntity? Editer { get; set; }
		[Column("WorkingDate")]
		// [FormDataTypeAttributeDatetime(EnumFormDataType.Datetime, false)]
		[DisplayName("WorkingDate")]
		public DateTime? WorkingDate { get; set; }
		[DisplayName("Time In")]
		// [FormDataTypeAttributeDatetime(EnumFormDataType.Datetime, false)]
		public DateTime? Time_In { get; set; }
		[DisplayName("Time out")]
		// [FormDataTypeAttributeDatetime(EnumFormDataType.Datetime, false)]
		public DateTime? Time_Out { get; set; }
		[Column("time_deviation")]
		// [FormDataTypeAttributeNumber(EnumFormDataType.IntegerNumber, false, Unit = "s")]
		[DisplayName("Time Deviation")]
		public long? Time_Deviation { get; set; }
		[DisplayName("EnableFlag")]
		// [FormDataTypeAttributeSelect(EnumFormDataType.Select, false)]
		public EnumWorkingDayStatus? WorkingDayStatus { get; set; }
		[ForeignKey(nameof(WorkingTypeEntity))]
		[DisplayName("Type")]
		// [FormDataTypeAttributeSelect(EnumFormDataType.Select, false)]
		public long? WorkingTypeId { get; set; }
		public WorkingTypeEntity? WorkingType { get; set; }
		// [FormDataTypeAttributeText(EnumFormDataType.Textarea)]
		[DisplayName("Update Reason")]
		public string? Update_Reason { get; set; }
		[Column("notes")]
		// [FormDataTypeAttributeText(EnumFormDataType.Textarea)]
		[DisplayName("Notes")]
		public string? Notes { get; set; }
		public DateTime? OriginalWorkDate { get; set; }

		public DateTime? OriginTimeIn { get; set; }

		public DateTime? OriginTimeOut { get; set; }

		public double? OriginTimeDeviation { get; set; }

		public EnumWorkingDayStatus? OriginWorkingDayStatus { get; set; }

		[ForeignKey("Origin" + nameof(WorkingTypeEntity))]
		public long? OriginWorkingTypeId { get; set; }

		public virtual WorkingTypeEntity? OriginWorkingType { get; set; }
		public virtual ICollection<WorkingDayApprovalEntity>? WorkingDayApprovals { get; set; }
	}
}
