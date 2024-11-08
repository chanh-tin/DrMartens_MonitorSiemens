using iSoft.Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrMartensMonitor.Enities
{
  public class DBDataEntity : BaseCRUDEntity
  {




    [ForeignKey(nameof(DBDataMonitorEnity))]
    public long? DBDataMonitorID { get; set; }
    public DBDataMonitorEnity? ItemDBDataMonitor { get; set; }
  }
}
