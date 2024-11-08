using iSoft.Common;
using iSoft.Common.Enums;
using iSoft.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iSoft.Common.ConstCommon;

namespace iSoft.Database.Extensions
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DisplayFieldAttribute : Attribute
    {
        public DisplayFieldAttribute()
        {
        }
    }
}
