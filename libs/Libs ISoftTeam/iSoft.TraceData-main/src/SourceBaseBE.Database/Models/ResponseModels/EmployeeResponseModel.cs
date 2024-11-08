using iSoft.Common.Utils;
using SourceBaseBE.Database.Entities;
using static iSoft.Common.ConstCommon;
using iSoft.Common.Enums;

namespace SourceBaseBE.Database.Models.ResponseModels
{
	public class EmployeeResponseModel : BaseCRUDResponseModel<EmployeeEntity>
	{
		public string? EmployeeCode { get; set; }
		public string? EmployeeMachineCode { get; set; }
		public string? Name { get; set; }
		public string? FullName { get; set; }
		public string? FirstName { get; set; }
		public string? MiddleName { get; set; }
		public string? LastName { get; set; }
		public string? DisplayName { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public EnumGender? Gender { get; set; }
		public string? Address { get; set; }
		public DateTime? Birdday { get; set; }
		public string? Avatar { get; set; }
		public long? DepartmentId { get; set; }
		public DepartmentEntity? ItemDepartment { get; set; }
		public long? JobTitleId { get; set; }
		public JobTitleEntity? ItemJobTitle { get; set; }
		public List<TimeSheetEntity>? TimeSheets { get; set; }

		public override object SetData(EmployeeEntity entity)
		{
			base.SetData(entity);
			this.Name = entity.Name;
			this.EmployeeMachineCode = entity.EmployeeMachineCode;
			this.EmployeeCode = entity.EmployeeCode;
			this.FullName = entity.FullName;
			this.FirstName = entity.FirstName;
			this.MiddleName = entity.MiddleName;
			this.LastName = entity.LastName;
			this.DisplayName = entity.DisplayName;
			this.Email = entity.Email;
			this.PhoneNumber = entity.PhoneNumber;
			this.Gender = entity.Gender;
			this.Address = entity.Address;
			this.Birdday = entity.Birthday;
			this.Avatar = entity.Avatar;
			this.DepartmentId = entity.DepartmentId;
			this.ItemDepartment = entity.Department;
			this.JobTitleId = entity.JobTitleId;
			this.ItemJobTitle = entity.JobTitle;
			this.TimeSheets = entity?.ListTimeSheets?.ToList();

			return this;
		}
		public override List<object> SetData(List<EmployeeEntity> listEntity)
		{
			List<Object> listRS = new List<object>();
			foreach (EmployeeEntity entity in listEntity)
			{
				listRS.Add(new EmployeeResponseModel().SetData(entity));
			}
			return listRS;
		}
	}
}
