using iSoft.Common.Enums;
using iSoft.Common.Utils;
using iSoft.Database.Entities;
using iSoft.Database.Entities.Interface;
using iSoft.Database.Extensions;
using iSoft.DBLibrary.Entities;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.Json.Serialization;
using static iSoft.Common.ConstCommon;

namespace SourceBaseBE.Database.Entities
{
  [Table("ISoftProjects")]
  public class ISoftProjectEntity : BaseCRUDEntity
  {
    [Required]
    //[FormDataTypeAttributeText(EnumFormDataType.Textbox)]
    [StringLength(255)]
    public string Name { get; set; }


    //[FormDataTypeAttributeText(EnumFormDataType.Textarea)]
    public string? Description { get; set; }


    [NotMapped]
    public List<long>? UserIds { get; set; } = new List<long>();
    [ListEntityAttribute(nameof(UserEntity), nameof(UserIds), EnumAttributeRelationshipType.Many2Many)]
    public List<UserEntity>? ListUser { get; set; } = new();


    //[FormDataTypeAttributeSelect(EnumFormDataType.Checkbox, false)]
    public EnumEnableFlag? EnableFlag { get; set; } = EnumEnableFlag.Enabled;
  }
}