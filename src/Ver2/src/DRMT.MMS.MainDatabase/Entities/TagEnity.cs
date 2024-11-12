using iSoft.Common.Enums;
using iSoft.Database.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRMT.MMS.MainDatabase.Entities
{
  [Table("Tag_DB")]
  public class TagEnity
  {
    public int ID_Tag {  get; set; }
    public int ID_DB { get; set; }
    public string NameVariable { get; set; }
    public string Type { get; set; }
    public double Offset { get; set; }

    [NotMapped]
    public List<long>? DBEnityID { get; set; } = new List<long>();

    [ListEntityAttribute(nameof(DBEnity), nameof(DBEnityID), EnumAttributeRelationshipType.One2Many)]
    public List<DBEnity>? _listDBEnity { get; set; } = new();
  }
}
