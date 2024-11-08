using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iSoft.Common.Enums;
using iSoft.Database.Entities;
using iSoft.Database.Extensions;
using SourceBaseBE.Database.Enums;

namespace SourceBaseBE.Database.Entities
{
    public class PermissionEntity : BaseCRUDEntity
    {
        [DisplayField]
        public string? Name { get; set; }


        public string? Description { get; set; }


        [FormDataTypeAttributeSelect(EnumFormDataType.Checkbox, null, isReadonly: false)]
        public bool? EnabledFlag { get; set; } = true;


        [NotMapped]
        public List<long>? PermissionDetailIds { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(PermissionDetailEntity), nameof(PermissionDetailIds), EnumAttributeRelationshipType.One2Many)]
        public List<PermissionDetailEntity>? ListPermissionDetail { get; set; } = new();


        [NotMapped]
        public List<long>? UserIds { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(UserEntity), nameof(UserIds), EnumAttributeRelationshipType.Many2Many)]
        [NotFormData]
        public List<UserEntity>? ListUser { get; set; } = new();


        [NotMapped]
        public List<long>? UserGroupIds { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(UserGroupEntity), nameof(UserGroupIds), EnumAttributeRelationshipType.Many2Many)]
        [NotFormData]
        public List<UserGroupEntity>? ListUserGroup { get; set; } = new();
    }
}
