using iSoft.Common.Enums;
using iSoft.Database.Extensions;
using SourceBaseBE.Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRMT.MMS.MainDatabase.Entities
{
  [Table("PLC")]
  public class PLCEnity
  {
    public int ID_PLC { get; set; }
    public string Name_PLC { get; set; }
    public string IP_PLC { get; set; }
    public int Port {  get; set; }


    [NotMapped]
    public List<long>? PLC_DBEnityID { get; set; } = new List<long>();

    [ListEntityAttribute(nameof(PLC_DBEnity), nameof(PLC_DBEnityID), EnumAttributeRelationshipType.One2Many)]
    public List<PLC_DBEnity>? ListPLC_DB { get; set; } = new();
  }
}
