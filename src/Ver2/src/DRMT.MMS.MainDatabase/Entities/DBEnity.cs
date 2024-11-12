using iSoft.Database.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRMT.MMS.MainDatabase.Entities
{
  [Table("DB")]
  public class DBEnity
  {
    public int ID_DB { get; set; }
    public string Name_DB { get; set; }

    [NotMapped]
    public List<long>? PLC_DBEnityID { get; set; } = new List<long>();

    [ListEntityAttribute(nameof(PLC_DBEnity), nameof(PLC_DBEnityID), iSoft.Common.Enums.EnumAttributeRelationshipType.One2Many)]
    public List<PLC_DBEnity>? ListPLC_DB { get; set; } = new();
  }
}
