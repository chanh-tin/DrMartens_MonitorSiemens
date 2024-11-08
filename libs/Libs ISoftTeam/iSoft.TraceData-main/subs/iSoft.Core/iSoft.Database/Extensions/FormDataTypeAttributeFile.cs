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
    public class FormDataTypeAttributeFile : FormDataTypeAttribute
    {

        public FormDataTypeAttributeFile(EnumFormDataType typeName, string[] fileExtension, int maxFileSizeInMB = 500, bool isRequired = false, bool isReadonly = false)
        {
            TypeName = typeName;
            IsRequired = isRequired;
            IsReadonly = isReadonly;
            FileExtension = fileExtension;
            MaxFileSizeInMB = maxFileSizeInMB;
            PrepareData();
        }
    }
}
