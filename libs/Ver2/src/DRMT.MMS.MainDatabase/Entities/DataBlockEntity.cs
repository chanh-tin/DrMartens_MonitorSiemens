using iSoft.Common.Enums;
using iSoft.Database.Entities;
using iSoft.Database.Extensions;
using SourceBaseBE.Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceBaseBE.Database.Entities
{
  [Table("DataBlocks")]
  public class DataBlockEntity : BaseCRUDEntity
  {
    public string Name { get; set; }

    [NotMapped]
    public List<long>? TagIds { get; set; } = new List<long>();
    [ListEntityAttribute(nameof(TagEntity), nameof(TagIds), EnumAttributeRelationshipType.One2Many)]
    //[NotFormData]
    public List<TagEntity>? ListTag { get; set; } = new();

    [NotMapped]
    public List<long>? PlcIds { get; set; } = new List<long>();
    [ListEntityAttribute(nameof(PlcEntity), nameof(PlcIds), EnumAttributeRelationshipType.Many2Many)]
    //[NotFormData]
    public List<PlcEntity>? ListPlc { get; set; } = new();
  }
}
