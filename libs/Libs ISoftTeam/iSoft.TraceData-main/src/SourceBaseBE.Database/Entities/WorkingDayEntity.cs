using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using iSoft.Database.Entities;
using System.Runtime.CompilerServices;
using SourceBaseBE.Database.Enums;
using iSoft.Common.Enums;

using iSoft.Database.Extensions;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using SourceBaseBE.Database.Attribute;

namespace SourceBaseBE.Database.Entities
{
    public class WorkingDayEntity : BaseCRUDEntity
    {
        [Key]
        [FormDataTypeAttributeNumber(EnumFormDataType.Hidden, true, defaultVal: null)]
        [JsonPropertyName("WorkingDayId")]
        public long? WorkingDayId => this.Id > 0 ? this.Id : null;
        public WorkingDayEntity()
        {
            //ProcessInOutState();
        }
        private bool isNeedCalculate = true;
        [Column("WorkingDate")]
        //[FormDataTypeAttributeText("Working Date", EnumFormDataType.DateOnly, true)]
        public DateTime? WorkingDate { get; set; }
        private DateTime? time_in;
        //[FormDataTypeAttributeText("Time In", EnumFormDataType.Datetime, false)]
        [Column("time_in")]
        public DateTime? Time_In { get { return time_in; } set { time_in = value;/* ProcessInOutState()*/;/* CalculateDeviation();*/ } }

        private DateTime? time_out;
        [Column("time_out")]
        //[FormDataTypeAttributeText("Time Out", EnumFormDataType.Datetime, false)]
        public DateTime? Time_Out { get { return time_out; } set { time_out = value;/* ProcessInOutState()*/; } }

        [Column("time_deviation")]
        //[FormDataTypeAttributeText("Time Deviation", EnumFormDataType.TimeOnly, false)]
        public double? TimeDeviation { get; set; }

        [Column("working_day_status")]
        //[FormDataTypeAttributeText("Working EnableFlag", EnumFormDataType.Select, false)]
        public EnumWorkingDayStatus? WorkingDayStatus { get; set; }
        public EnumInOutTypeStatus InOutState
        {
            get;
            set;
        }

        [Column("notes")]
        //[FormDataTypeAttributeText("Notes", EnumFormDataType.Textarea, false)]
        public string? Notes { get; set; }
        //[FormDataTypeAttributeText("Employee LossName", EnumFormDataType.Textbox, false, true)]
        public string? EmployeeName => Employee?.Name;
        [ForeignKey(nameof(EmployeeEntity))]
        //[FormDataTypeAttributeText("EmployeeId", EnumFormDataType.Hidden, true)]
        public long? EmployeeEntityId { get; set; }
        public EmployeeEntity? Employee { get; set; }

        [ForeignKey(nameof(WorkingTypeEntity))]
        [Column("working_type_id")]
        //[FormDataTypeAttributeText("Working Type", EnumFormDataType.Select, false)]
        public long? WorkingTypeEntityId { get; set; }
        [Column("working_type")]
        public virtual WorkingTypeEntity? WorkingType { get; set; }
        //[NotMapped]
        [Column("recommendtype")]
        [Filterable("recommendtype", "recommendtype", false)]
        public string? RecommendType { get; set; }
        public virtual ICollection<WorkingDayUpdateEntity>? WorkingDayUpdates { get; set; }
        public virtual ICollection<WorkingDayApprovalEntity>? WorkingDayApprovals { get; set; }
        public virtual ICollection<TimeSheetEntity>? TimeSheets { get; set; }

        public override string ToString()
        {
            return $"{this.Id}:{this.WorkingDate}:{this.WorkingDayStatus.ToString()}:{this.RecommendType}";
        }
    }
}