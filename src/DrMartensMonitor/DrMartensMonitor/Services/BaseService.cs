using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrMartensMonitor.Services
{
  public class BaseService
  {
    public virtual BaseService CreateNew()
    {
      return new BaseService();
    }
  }
}
