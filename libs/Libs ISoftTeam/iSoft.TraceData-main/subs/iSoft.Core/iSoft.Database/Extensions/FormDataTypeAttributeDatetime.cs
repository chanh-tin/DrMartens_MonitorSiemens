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
    public class FormDataTypeAttributeDatetime : FormDataTypeAttribute
    {
        // ex: FormDataTypeAttributeDatetime(EnumFormDataType.Datetime, false, min: "2023-01-10T23:58:00.000", max: "2024-12-10T23:58:00.000", "2024-01-01T23:58:00.000", null)
        // ex: FormDataTypeAttributeDatetime(EnumFormDataType.DateOnly, false, min: "1920-01-01", max: "{CURRENT}", "{CURRENT}", null)
        public FormDataTypeAttributeDatetime(EnumFormDataType typeName, object? min = null, object? max = null, object? defaultVal = null, string? unit = null, bool isRequired = false, bool isReadonly = false)
        {
            TypeName = typeName;
            IsRequired = isRequired;
            Min = min;
            Max = max;
            DefaultVal = defaultVal;
            Unit = unit;
            IsReadonly = isReadonly;
            PrepareData();
        }
    }
}
