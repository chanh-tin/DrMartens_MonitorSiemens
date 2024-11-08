﻿using System.ComponentModel.DataAnnotations.Schema;
using iSoft.Common.Enums;
using iSoft.Database.Entities;
using iSoft.Database.Extensions;
using SourceBaseBE.Database.Interfaces;

namespace SourceBaseBE.Database.Entities
{
	[Table("Departments")]
	public class DepartmentEntity : BaseCRUDEntity
	{
		[Column("name")]
		[FormDataTypeAttributeText(EnumFormDataType.Textbox)]
		public string? Name { get; set; }

		[Column("description")]
		public string? Description { get; set; }

		[Column("notes")]
		public string? Notes { get; set; }

		public ICollection<EmployeeEntity>? Employees { get; set; }
		public ICollection<DepartmentAdminEntity> DepartmentAdmins { get; set; }

	}
}
