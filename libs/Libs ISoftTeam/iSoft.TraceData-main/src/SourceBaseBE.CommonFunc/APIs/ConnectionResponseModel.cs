using iMag.Oee.Models.RequestModels;
using iSoft.Common.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.Common.ResponseObjectNS
{
    public class ConnectionResponseModel
    {
        public long TotalRecord { get; set; }
        public List<ConnectionDataModel> ListData { get; set; }
    }
}
