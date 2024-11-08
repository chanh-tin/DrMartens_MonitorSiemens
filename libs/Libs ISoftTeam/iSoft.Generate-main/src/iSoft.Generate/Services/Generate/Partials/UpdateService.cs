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

        private void UpdateService(string entityName, List<CustomAttr> listAttr)
        {
            string filePath = newCRUD_base.filePath_Service;
            //string filePath = $"./../SourceBaseBE.MainService/Services/{entityName}Service.txt";
            string fileContent = File.ReadAllText(filePath);

            List<string> listKeyGen = new List<string>()
            {
                "/*[GEN-1]*/",
                "/*[GEN-2]*/",
                "/*[GEN-3]*/",
                "/*[GEN-4]*/",
                "/*[GEN-5]*/",
                "/*[GEN-6]*/",
                "/*[GEN-26]*/",
                "/*[GEN-32]*/",
            };
            foreach (string keyGen in listKeyGen)
            {
                string newStr = GetGenData(entityName, listAttr, keyGen);
                if (!IsExitsFileContent(fileContent, newStr.Replace(keyGen, "")))
                {
                    fileContent = fileContent.Replace(keyGen, newStr);
                }
            }

            fileContent = Regex.Replace(fileContent, @"/\*\[GEN\-\d+\]\*/", "");

            fileContent = fileContent.Replace("/*[GEN-Date]*/", DateTimeUtil.GetDateTimeStr(DateTime.Now));

            File.WriteAllText(filePath, fileContent);
        }
        private void UpdateServiceImp(string entityName, List<CustomAttr> listAttr)
        {
            string filePath = newCRUD.filePath_Service;
            //string filePath = $"./../SourceBaseBE.MainService/Services/{entityName}Service.txt";
            string fileContent = File.ReadAllText(filePath);

            List<string> listKeyGen = new List<string>()
            {
                "/*[GEN-1]*/",
                "/*[GEN-2]*/",
                "/*[GEN-3]*/",
                "/*[GEN-4]*/",
                "/*[GEN-5]*/",
                "/*[GEN-6]*/",
                "/*[GEN-26]*/",
                "/*[GEN-32]*/",
            };
            foreach (string keyGen in listKeyGen)
            {
                string newStr = GetGenData(entityName, listAttr, keyGen);
                if (!IsExitsFileContent(fileContent, newStr.Replace(keyGen, "")))
                {
                    fileContent = fileContent.Replace(keyGen, newStr);
                }
            }

            fileContent = Regex.Replace(fileContent, @"/\*\[GEN\-\d+\]\*/", "");

            fileContent = fileContent.Replace("/*[GEN-Date]*/", DateTimeUtil.GetDateTimeStr(DateTime.Now));

            File.WriteAllText(filePath, fileContent);
        }
    }
}