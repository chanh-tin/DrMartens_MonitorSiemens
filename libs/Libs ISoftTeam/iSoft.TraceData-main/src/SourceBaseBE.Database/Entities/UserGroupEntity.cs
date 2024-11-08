using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iSoft.Common.Enums;
using iSoft.Database.Entities;
using iSoft.Database.Extensions;
using SourceBaseBE.Database.Enums;

namespace SourceBaseBE.Database.Entities
{
    public class UserGroupEntity : BaseCRUDEntity
    {
        [DisplayField]
        public string? Name { get; set; }

        public string? Description { get; set; }


        [NotMapped]
        public List<long>? UserIds { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(UserEntity), nameof(UserIds), EnumAttributeRelationshipType.Many2Many)]
        public List<UserEntity>? ListUser { get; set; } = new();


        [NotMapped]
        public List<long>? PermissionIds { get; set; } = new List<long>();
        [ListEntityAttribute(nameof(PermissionEntity), nameof(PermissionIds), EnumAttributeRelationshipType.Many2Many)]
        public List<PermissionEntity>? ListPermission { get; set; } = new();
    }
}
