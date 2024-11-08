using iSoft.Common.Enums;
using iSoft.Database.Entities;
using iSoft.Database.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace SourceBaseBE.Database.Entities
{
  public class AuthPermissionEntity : BaseCRUDEntity
  {
    // [FormDataTypeAttributeText(EnumFormDataType.Textbox)]
    public string Name { get; set; }


    // [FormDataTypeAttributeText(EnumFormDataType.Textarea)]
    public string? Description { get; set; }


    [NotMapped]
    public List<long>? AuthGroupIds { get; set; } = new List<long>();
    [ListEntityAttribute(nameof(AuthGroupEntity), nameof(AuthGroupIds), EnumAttributeRelationshipType.Many2Many)]
    public List<AuthGroupEntity>? ListAuthGroup { get; set; } = new();


    [NotMapped]
    public List<long>? UserIds { get; set; } = new List<long>();
    [ListEntityAttribute(nameof(UserEntity), nameof(UserIds), EnumAttributeRelationshipType.Many2Many)]
    public List<UserEntity>? ListUser { get; set; } = new();
  }
}