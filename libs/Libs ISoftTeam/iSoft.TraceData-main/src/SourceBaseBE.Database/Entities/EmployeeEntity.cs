using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using iSoft.Database.Entities;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using iSoft.Database.Extensions;
using SourceBaseBE.Database.Enums;
using iSoft.Common.Utils;
using iSoft.Common.Enums;

namespace SourceBaseBE.Database.Entities
{
    [Table("Employees")]
    public class EmployeeEntity : BaseCRUDEntity
    {
        [Required]
        [Column("employee_code")]
        //[FormDataTypeAttributeText("Employee Code", EnumFormDataType.Textbox, true)]
        public string? EmployeeCode { get; set; }
        [Column("employee_machine_code")]
        [Required]
        [Browsable(false)]
        //[FormDataTypeAttributeText("Employee Machine Code", EnumFormDataType.Textbox, true)]
        public string EmployeeMachineCode { get; set; }
        //[FormDataTypeAttributeText("LossName", EnumFormDataType.Textbox, true)]
        [Column("name")]
        public string? Name { get; set; }

        [Column("full_name")]
        public string? FullName { get; set; }

        [Column("first_name")]
        public string? FirstName { get; set; }

        [Column("middle_name")]
        public string? MiddleName { get; set; }

        [Column("last_name")]
        public string? LastName { get; set; }

        [Column("display_name")]
        public string? DisplayName { get; set; }

        [Column("Email")]
        public string? Email { get; set; }

        [Column("phone_number")]
        //[FormDataTypeAttributeText("Phone Number", EnumFormDataType.Textbox, false)]
        public string? PhoneNumber { get; set; }

        [Column("gender")]
        //[FormDataTypeAttributeText("Gender", EnumFormDataType.Select, false)]
        public iSoft.Common.Enums.EnumGender? Gender { get; set; }

        [Column("address")]
        //[FormDataTypeAttributeText("Address", EnumFormDataType.Textbox, false)]
        public string? Address { get; set; }
        //[FormDataTypeAttributeText("Birthday", EnumFormDataType.DateOnly, false)]

        [Column("birdthday")]
        public DateTime? Birthday { get; set; }
        [Column("joining_date")]
        //[FormDataTypeAttributeText("Joining Date", EnumFormDataType.DateOnly, false)]
        public DateTime? JoiningDate { get; set; }

        [Column("avatar")]
        public string? Avatar { get; set; }

        [ForeignKey(nameof(DepartmentEntity))]
        //[FormDataTypeAttributeText("Department", EnumFormDataType.Select, true)]
        public long? DepartmentId { get; set; }
        public DepartmentEntity? Department { get; set; }

        [ForeignKey(nameof(JobTitleEntity))]
        //[FormDataTypeAttributeText("JobTitle", EnumFormDataType.Select, true)]
        public long? JobTitleId { get; set; }
        public JobTitleEntity? JobTitle { get; set; }

        [Column("employee_status")]
        public EnumEmployeeStatus? EmployeeStatus { get; set; }

        public ICollection<TimeSheetEntity>? ListTimeSheets { get; set; }
        public ICollection<WorkingDayEntity>? WorkingDayEntitys { get; set; }


        [NotMapped]
        public List<long>? UserIds { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(UserEntity), nameof(UserIds), EnumAttributeRelationshipType.One2Many)]
        public List<UserEntity>? ListUser { get; set; } = new();


        public string GetShowName()
        {
            if (!string.IsNullOrEmpty(this.FullName))
            {
                return this.FullName;
            }
            if (!string.IsNullOrEmpty(this.Name))
            {
                return this.Name;
            }
            if (!string.IsNullOrEmpty(this.DisplayName))
            {
                return this.DisplayName;
            }
            return this.EmployeeCode;
        }
    }
}