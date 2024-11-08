using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceBaseBE.Database.Models.RequestModels
{
  public class MasterDataRequestModel
  { 
    public bool? Department { get; set; }
    public bool? JobTitle { get; set; }  
    public bool? Status { get; set; }
  }
}
