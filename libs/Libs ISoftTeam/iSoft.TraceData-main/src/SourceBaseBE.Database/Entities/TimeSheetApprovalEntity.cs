using System.ComponentModel.DataAnnotations.Schema;
using iSoft.Common.Enums;
using iSoft.Database.Entities;
using SourceBaseBE.Database.Interfaces;

namespace SourceBaseBE.Database.Entities
{
  [Table("TimeSheetApprovals")]
  public class TimeSheetApprovalEntity : BaseCRUDEntity
  {
    [Column("in_out_type")]
    public EnumInOutTypeStatus? InOutType { get; set; }

    [Column("recorded_time")]
    public DateTime? RecordedTime { get; set; }

    [Column("update_reason")]
    public string? UpdateReason { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

    [ForeignKey(nameof(EmployeeEntity))]
    public long? EmployeeId { get; set; }
    public EmployeeEntity? ItemEmployee { get; set; }
  }
}
