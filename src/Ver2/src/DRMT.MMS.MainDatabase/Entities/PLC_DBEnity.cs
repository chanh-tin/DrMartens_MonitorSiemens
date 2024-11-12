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

namespace DRMT.MMS.MainDatabase.Entities
{
  [Table("PLC_DB")]
  public class PLC_DBEnity : BaseCRUDEntity
  {    
    public int ID_PLC { get; set; }
    public int ID_DB {  get; set; }

    [NotMapped]
    public List<long>? DBEnityId { get; set; } = new List<long>();
    [ListEntityAttribute(nameof(DBEnity), nameof(DBEnityId), EnumAttributeRelationshipType.One2Many)]
    
    public List<DBEnity>? ListDBEnity { get; set; } = new();


    [NotMapped]
    public List<long>? PLCEnityIds { get; set; } = new List<long>();
    [ListEntityAttribute(nameof(PLCEnity), nameof(PLCEnityIds), EnumAttributeRelationshipType.One2Many)]
    public List<PLCEnity>? ListPLCEnity { get; set; } = new();

  }
}
