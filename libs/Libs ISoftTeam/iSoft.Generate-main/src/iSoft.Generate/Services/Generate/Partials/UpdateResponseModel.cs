using SourceBaseBE.Database.DBContexts;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using iSoft.Common.Utils;
using iSoft.Database.Extensions;
using iSoft.Common.Enums;
using Microsoft.Extensions.Logging;
using iSoft.Common.Utils;

namespace SourceBaseBE.MainService.Services.Generate
{
    public partial class GenerateSourceService
    {

        private void UpdateResponseModel(string entityName, List<CustomAttr> listAttr)
        {
            string filePath = newCRUD_base.filePath_ResponseModel;
            string fileContent = File.ReadAllText(filePath);

            //string newStr = GetAttrData(listAttr, EnumFileType.ResponseFile);
            //fileContent = Regex.Replace(
            //    fileContent,
            //    @"(class " + entityName + @"ResponseModel [^\{]+\{).*(public override object SetData\(.+)",
            //    "$1\r\n" + newStr + "\r\n        $2",
            //    RegexOptions.Singleline);

            //newStr = GetAttrSetData(listAttr, EnumFileType.ResponseFile);
            //fileContent = Regex.Replace(
            //    fileContent,
            //    @"(base\.SetData\(entity\);).*(return this;.+)",
            //    "$1\r\n" + newStr + "\r\n            $2",
            //    RegexOptions.Singleline);

            string newStr = "";
            List<string> listKeyGen = new List<string>()
            {
                "/*[GEN-20]*/",
                "/*[GEN-21]*/",
                "/*[GEN-33]*/",
                "/*[GEN-34]*/",
            };
            foreach (string keyGen in listKeyGen)
            {
                newStr = GetGenData(entityName, listAttr, keyGen, fileContent);
                fileContent = fileContent.Replace(keyGen, newStr);
            }

            fileContent = Regex.Replace(fileContent, @"/\*\[GEN\-\d+\]\*/", "");

            fileContent = fileContent.Replace("/*[GEN-Date]*/", DateTimeUtil.GetDateTimeStr(DateTime.Now));

            File.WriteAllText(filePath, fileContent);
        }
    }
}