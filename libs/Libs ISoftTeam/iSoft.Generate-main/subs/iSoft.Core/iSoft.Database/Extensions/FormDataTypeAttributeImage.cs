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
    public class FormDataTypeAttributeImage : FormDataTypeAttribute
    {

        // ex: FormDataTypeAttributeImage(EnumFormDataType.Image, false, "120px", "120px")
        public FormDataTypeAttributeImage(EnumFormDataType typeName, int width, int height, bool isRequired = false, bool isReadonly = false)
        {
            TypeName = typeName;
            IsRequired = isRequired;
            Width = width;
            Height = height;
            IsReadonly = isReadonly;
            FileExtension = new string[] { "png", "jpg", "jpeg", "gif"};
            MaxFileSizeInMB = 100;
            PrepareData();
        }
    }
}
