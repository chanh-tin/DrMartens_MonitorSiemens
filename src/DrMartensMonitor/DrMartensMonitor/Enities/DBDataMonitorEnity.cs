using iSoft.Common.Enums;
using iSoft.Database.Entities;
using iSoft.Database.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrMartensMonitor.Enities
{
  [Table("DBDataMonitor")]
  public class DBDataMonitorEnity : BaseCRUDEntity
  {
    public string NameMachine { get; set; }

    

    [NotMapped]
    public List<long>? RoiDataIds { get; set; } = new List<long>();
    [ListEntityAttribute(nameof(DBDataEntity), nameof(RoiDataIds), EnumAttributeRelationshipType.One2Many)]
    public List<DBDataEntity>? _listDatadb { get; set; } = new List<DBDataEntity>();
  }
}
