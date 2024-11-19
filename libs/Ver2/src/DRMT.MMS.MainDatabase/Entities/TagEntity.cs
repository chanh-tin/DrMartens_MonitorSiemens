using iSoft.Common.Enums;
using iSoft.Database.Entities;
using iSoft.Database.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace SourceBaseBE.Database.Entities
{
  [Table("Tags")]
  public class TagEntity : BaseCRUDEntity
  {
    public string Name { get; set; }
    public string? Type { get; set; }
    public double? Offset { get; set; }

    //[NotFormData]
    [ForeignKey(nameof(DataBlockEntity))]
    public long? DataBlockId { get; set; }
    public DataBlockEntity? ItemDataBlock { get; set; }


    [NotMapped]
    public List<long>? DBEnityID { get; set; } = new List<long>();

    [ListEntityAttribute(nameof(DataBlockEntity), nameof(DBEnityID), EnumAttributeRelationshipType.One2Many)]
    public List<DataBlockEntity>? ListDataBlock { get; set; } = new();
  }
}
