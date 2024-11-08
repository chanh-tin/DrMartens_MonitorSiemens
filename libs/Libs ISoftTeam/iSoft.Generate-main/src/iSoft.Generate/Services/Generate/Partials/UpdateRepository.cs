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
        private void UpdateRepository(string entityName, List<CustomAttr> listAttr, string tableName)
        {
            string filePath = newCRUD_base.filePath_Repository;
            //string filePath = $"./../SourceBaseBE.Database/Repository/{entityName}Repository.txt";
            string fileContent = File.ReadAllText(filePath);

            List<string> listKeyGen = new List<string>()
            {
                "/*[GEN-4]*/",
                "/*[GEN-7]*/",
                "/*[GEN-8]*/",
                "/*[GEN-9]*/",
                "/*[GEN-10]*/",
                "/*[GEN-11]*/",
                "/*[GEN-12]*/",
                "/*[GEN-13]*/",
                "/*[GEN-14]*/",
                "/*[GEN-27]*/",
                "/*[GEN-28]*/",
                "/*[GEN-29]*/",
                "/*[GEN-30]*/",
                "/*[GEN-31]*/",
            };
            foreach (string keyGen in listKeyGen)
            {
                string newStr = GetGenData(entityName, listAttr, keyGen);
                fileContent = fileContent.Replace(keyGen, newStr);
            }

            fileContent = Regex.Replace(fileContent, @"/\*\[GEN\-\d+\]\*/", "");

            fileContent = fileContent.Replace("/*[GEN-Date]*/", DateTimeUtil.GetDateTimeStr(DateTime.Now));

            fileContent = fileContent.Replace("/*[GEN-TableName]*/", tableName);

            File.WriteAllText(filePath, fileContent);
        }

    }
}