using iSoft.Common.Enums;
using iSoft.Database.Entities;
using iSoft.Database.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace SourceBaseBE.Database.Entities
{
  public class AuthGroupEntity : BaseCRUDEntity
  {

    // [FormDataTypeAttributeText(EnumFormDataType.Textbox)]
    public string Name { get; set; }


    // [FormDataTypeAttributeText(EnumFormDataType.Textarea)]
    public string? Description { get; set; }


    [NotMapped]
    public List<long>? AuthPermissionIds { get; set; } = new List<long>();
    [ListEntityAttribute(nameof(AuthPermissionEntity), nameof(AuthPermissionIds), EnumAttributeRelationshipType.Many2Many)]
    public List<AuthPermissionEntity>? ListAuthPermission { get; set; } = new();


    [NotMapped]
    public List<long>? UserIds { get; set; } = new List<long>();
    [ListEntityAttribute(nameof(UserEntity), nameof(UserIds), EnumAttributeRelationshipType.Many2Many)]
    public List<UserEntity>? ListUser { get; set; } = new();
  }
}
