using iSoft.Common.Enums;
using iSoft.Database.Entities;
using iSoft.Database.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRMT.MMS.MainDatabase.Entities
{
  [Table("DataMonitor")]
  public class DataMonitorEntity : BaseCRUDEntity
  {
    [DisplayField]
    [StringLength(255)]
    [FormDataTypeAttributeText(EnumFormDataType.Textbox, isRequired: true, maxLen: 255)]
    public string NameDB { get; set; }
  }
}
