using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using iSoft.Common.Enums;
using iSoft.Database.Entities;
using iSoft.Database.Extensions;
using SourceBaseBE.Database.Interfaces;

namespace SourceBaseBE.Database.Entities
{
	[Table("JobTitles")]
	public class JobTitleEntity : BaseCRUDEntity
	{
		[Column("name")]
		[DisplayName("Name")]
		[FormDataTypeAttributeText(EnumFormDataType.Textbox, isRequired: true)]
		public string? Name { get; set; }
		[DisplayName("LossDescription")]
		[Column("description")]
		[FormDataTypeAttributeText(EnumFormDataType.Textbox, isRequired: false)]
		public string? Description { get; set; }
		[FormDataTypeAttributeText(EnumFormDataType.Textbox, isRequired: false)]
		[Column("notes")]
		[DisplayName("Note")]
		public string? Notes { get; set; }
		[FormDataTypeAttributeText(EnumFormDataType.Hidden, isRequired: false)]
		public List<EmployeeEntity>? Employees { get; set; }
	}
}
