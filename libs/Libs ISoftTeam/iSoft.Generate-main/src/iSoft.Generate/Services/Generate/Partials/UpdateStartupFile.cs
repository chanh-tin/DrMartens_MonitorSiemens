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

        private void UpdateStartupFile(string entityName)
        {
            string generateFolderPath = Environment.GetEnvironmentVariable("GENERATE_PATH_FOLDER");
            string filePath = $"{generateFolderPath}/Startup.cs";
            string fileContent = File.ReadAllText(filePath);
            string newStr = $"            services.AddScoped<{entityName}Service>();";
            if (!IsExitsFileContent(fileContent, newStr))
            {
                fileContent = Regex.Replace(
                fileContent,
                @"(services\.AddScoped\<GenTemplateService\>\(\);)",
                "$1\r\n" + newStr);
                File.WriteAllText(filePath, fileContent);
            }
        }
    }
}