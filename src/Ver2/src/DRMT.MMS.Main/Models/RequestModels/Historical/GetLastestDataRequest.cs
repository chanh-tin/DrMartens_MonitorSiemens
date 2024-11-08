using iSoft.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SourceBaseBE.MainService.Models.RequestModels.Historical
{
    public class GetLastestDataRequest
    {
        public string SearchField { get; set; }

        public override string ToString()
        {
            return $"{SearchField}";
        }
    }
}
