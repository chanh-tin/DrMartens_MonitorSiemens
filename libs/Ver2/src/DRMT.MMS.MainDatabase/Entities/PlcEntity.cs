using iSoft.Common.Enums;
using iSoft.Database.Entities;
using iSoft.Database.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace SourceBaseBE.Database.Entities
{
  [Table("PLCs")]
  public class PlcEntity : BaseCRUDEntity
  {
    public string Name { get; set; }
    public string? IpAddress { get; set; }
    public int? Port { get; set; }

    [NotMapped]
    public List<long>? DataBlockIds { get; set; } = new List<long>();
    [ListEntityAttribute(nameof(DataBlockEntity), nameof(DataBlockIds), EnumAttributeRelationshipType.Many2Many)]
    //[NotFormData]
    public List<DataBlockEntity>? ListDataBlock { get; set; } = new();

  }
}
