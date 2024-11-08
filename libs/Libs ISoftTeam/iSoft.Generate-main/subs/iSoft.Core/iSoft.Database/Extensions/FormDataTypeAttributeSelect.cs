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
    public class FormDataTypeAttributeSelect : FormDataTypeAttribute
    {
        // ex: FormDataTypeAttributeSelect(EnumFormDataType.Select, new object[] { 1, 5, 10, 20 }, 5, null, "Seconds", false, true, false)
        public FormDataTypeAttributeSelect(EnumFormDataType typeName, object[]? options, object? defaultVal = null, string? unit = null, bool isRequired = false, bool searchable = true, bool isReadonly = false)
        {
            TypeName = typeName;
            Options = options?.ToList();
            Searchable = searchable;
            DefaultVal = defaultVal;
            Unit = unit;
            IsRequired = isRequired;
            IsReadonly = isReadonly;
            PrepareData();
        }
    }
}
