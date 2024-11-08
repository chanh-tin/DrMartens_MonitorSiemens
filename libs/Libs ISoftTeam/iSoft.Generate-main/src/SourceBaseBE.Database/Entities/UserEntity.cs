using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iSoft.Common;
using iSoft.Common.Enums;
using iSoft.Database.Entities;
using iSoft.Database.Extensions;
using Newtonsoft.Json;
using SourceBaseBE.Database.Enums;
using SourceBaseBE.Database.Interfaces;
using static iSoft.Common.ConstCommon;

namespace SourceBaseBE.Database.Entities
{
    [Table("Users")]
    public class UserEntity : BaseCRUDEntity
    {
        [DisplayField]
        [FormDataTypeAttributeText(EnumFormDataType.Textbox, isRequired: true, maxLen: 50)]
        [StringLength(50)]
        public string Username { get; set; }


        [JsonIgnore]
        [StringLength(255)]
        [FormDataTypeAttributeText(EnumFormDataType.Password, isRequired: true, maxLen: 50)]
        public string Password { get; set; }


        [FormDataTypeAttributeText(EnumFormDataType.Textarea, maxLen: 255)]
        [MaxLength(255)]
        public string? DisplayName { get; set; }


        [FormDataTypeAttributeText(EnumFormDataType.Textbox, maxLen: 255)]
        [StringLength(255)]
        public string? FirstName { get; set; }


        [FormDataTypeAttributeText(EnumFormDataType.Textbox, maxLen: 255)]
        [StringLength(255)]
        public string? MiddleName { get; set; }


        [FormDataTypeAttributeText(EnumFormDataType.Textbox)]
        [StringLength(255)]
        public string? LastName { get; set; }


        [FormDataTypeAttributeSelect(EnumFormDataType.Select, new object[] { (int)EnumGender.Male, (int)EnumGender.Female, (int)EnumGender.Other }, isRequired: true, defaultVal: (int)EnumGender.Male)]
        public EnumGender? Gender { get; set; }


        [FormDataTypeAttributeText(EnumFormDataType.PhoneNumber)]
        [StringLength(25)]
        public string? PhoneNumber { get; set; }


        [FormDataTypeAttributeText(EnumFormDataType.Email, maxLen: 255)]
        [StringLength(255)]
        public string? Email { get; set; }


        [FormDataTypeAttributeText(EnumFormDataType.Textarea, maxLen: 255)]
        [StringLength(255)]
        public string? Address { get; set; }


        [FormDataTypeAttributeDatetime(EnumFormDataType.DateOnly, defaultVal: "{CURRENT}")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Birthday { get; set; }


        [FormDataTypeAttributeText(EnumFormDataType.Textbox, maxLen: 255)]
        [StringLength(255)]
        public string? CompanyName { get; set; }


        [FormDataTypeAttributeImage(EnumFormDataType.Image, 120, 120)]
        [StringLength(255)]
        public string? Avatar { get; set; }


        //[NotFormData]
        [ForeignKey(nameof(LanguageEntity))]
        public long? LanguageId { get; set; }
        public LanguageEntity? ItemLanguage { get; set; }


        //[NotFormData]
        [ForeignKey(nameof(CountryEntity))]
        public long? CountryId { get; set; }
        public CountryEntity? ItemCountry { get; set; }


        [StringLength(10)]
        public string Role { get; set; }


        [StringLength(255)]
        public string? License { get; set; }


        public EnumEnableFlag? EnableFlag { get; set; } = EnumEnableFlag.Enabled;


        [FormDataTypeAttributeDatetime(EnumFormDataType.DateOnly, min: "1900-01-01", max: "{CURRENT}", "{CURRENT}", isReadonly: true)]
        public DateTime? LastLogin { get; set; }


        [FormDataTypeAttributeText(EnumFormDataType.Textarea, maxLen: 510)]
        [MaxLength(510)]
        public string? Notes { get; set; }


        [NotMapped]
        public List<long>? ISoftProjectIds { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(ISoftProjectEntity), nameof(ISoftProjectIds), EnumAttributeRelationshipType.Many2Many)]
        public List<ISoftProjectEntity>? ListISoftProject { get; set; } = new();


        [NotMapped]
        public List<long>? PermissionIds { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(PermissionEntity), nameof(PermissionIds), EnumAttributeRelationshipType.Many2Many)]
        public List<PermissionEntity>? ListPermission { get; set; } = new();


        [NotMapped]
        public List<long>? UserGroupIds { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(UserGroupEntity), nameof(UserGroupIds), EnumAttributeRelationshipType.Many2Many)]
        public List<UserGroupEntity>? ListUserGroup { get; set; } = new();

    }
}

