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
        private void UpdateDBContextFile(string entityName, List<CustomAttr> listAttr, string tableName)
        {
            string generateFolderDBPath = Environment.GetEnvironmentVariable("GENERATE_PATH_DB_FOLDER");
            string filePath = $"{generateFolderDBPath}/DBContexts/CommonDbContext.cs";
            string fileContent = File.ReadAllText(filePath);
            string newStr = $"        public virtual DbSet<{entityName}Entity> {tableName} {{ get; set; }}";
            if (!IsExitsFileContent(fileContent, newStr))
            {
                fileContent = Regex.Replace(
                    fileContent,
                    @"(protected IConfiguration Configuration \{ get; set; \})",
                    "$1\r\n" + newStr);
            }

            List<string> listKeyGen = new List<string>()
            {
                "/*[GEN-15]*/",
                "/*[GEN-16]*/",
                "/*[GEN-22]*/",
                "/*[GEN-23]*/",
                "/*[GEN-24]*/",
                "/*[GEN-25]*/",
            };
            foreach (string keyGen in listKeyGen)
            {
                newStr = GetGenData(entityName, listAttr, keyGen, fileContent);
                if (!IsExitsFileContent(fileContent, newStr.Replace(keyGen, "")))
                {
                    fileContent = fileContent.Replace(keyGen, newStr);
                }
            }
            File.WriteAllText(filePath, fileContent);
        }
    }
}