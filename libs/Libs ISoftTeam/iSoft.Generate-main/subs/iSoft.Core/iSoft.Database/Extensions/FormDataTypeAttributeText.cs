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
    public class FormDataTypeAttributeText : FormDataTypeAttribute
    {
        public FormDataTypeAttributeText(EnumFormDataType typeName, string placeholder = null, int maxLen = 255, EnumValidationRules[]? validationRules = null, bool isRequired = false, bool isReadonly = false)
        {
            TypeName = typeName;
            Placeholder = placeholder;
            IsRequired = isRequired;
            IsReadonly = isReadonly;
            MaxLen = maxLen;
            //RefField = refField;
            if (validationRules != null && validationRules.Length >= 1)
            {
                ValidationRules = new List<Dictionary<string, string>>();
                foreach (var validationRule in validationRules)
                {
                    ValidationRules.Add(new Dictionary<string, string>
                    {
                        {"name", validationRule.ToString().LowerFirstLetter() }
                    });
                }
            }
            PrepareData();
        }
    }
}
