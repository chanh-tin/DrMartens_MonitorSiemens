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
    public class FormDataTypeAttributeNumber : FormDataTypeAttribute
    {
        public FormDataTypeAttributeNumber(EnumFormDataType typeName,
            bool isRequired = false,
            object? min = null,
            object? max = null,
            object? defaultVal = null,
            string? unit = null,
            bool isReadonly = false,
            EnumLevelDatetime minLevelDateTime = EnumLevelDatetime.None,
            EnumLevelDatetime maxLevelDateTime = EnumLevelDatetime.None)
        {
            TypeName = typeName;
            IsRequired = isRequired;
            Min = min;
            Max = max;
            DefaultVal = defaultVal;
            Unit = unit;
            IsReadonly = isReadonly;
            MinLevelDateTime = minLevelDateTime;
            MaxLevelDateTime = maxLevelDateTime;
            PrepareData();
        }
    }
}
