using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iSoft.Common;
using iSoft.Common.Enums;
using iSoft.Database.Entities;
using iSoft.Database.Extensions;
using Newtonsoft.Json;
using SourceBaseBE.Database.Interfaces;
using iSoft.Common.Enums;
using static iSoft.Common.ConstCommon;

namespace SourceBaseBE.Database.Entities
{
	[Table("HolidaySchedule")]
	public class HolidayScheduleEntity : BaseCRUDEntity, IEntityUpdatedAt, IEntityUpdatedBy, IEnityCreatedAt, IEnityCreatedBy
	{
		[Column("name")]
    [DisplayName("Name")]
    //[FormDataTypeAttributeText(EnumFormDataType.Textbox)]
    public string Name { get; set; }

    [DisplayName("Start Date")]
    [Column("start_date")]
    //[FormDataTypeAttributeText("Start Date", EnumFormDataType.Datetime, true)]
    public DateTime StartDate { get; set; }

		[Column("end_date")]
    //[FormDataTypeAttributeText("End Date", EnumFormDataType.Datetime, true)]
    [DisplayName("End Date")]
    public DateTime EndDate { get; set; }

		[Column("description")]
    //[FormDataTypeAttributeText(EnumFormDataType.Textbox)]
    [DisplayName("LossDescription")]
    public string? Description { get; set; }

		[Column("notes")]
    [DisplayName("Note")]
    public string? Note { get; set; }


    [Column("holiday_Type")]
    [DisplayName("Holiday Type")]
    //[FormDataTypeAttributeText("Holiday Type", EnumFormDataType.Select, true)]
    public EnumHolidayCode? HolidayType { get; set; }

    public ICollection<HolidayWorkingTypeEntity> HolidayWorkingTypes { get; set; }
	}
}

