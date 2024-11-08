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
        private ILogger<GenerateSourceService> _logger;
        CRUDFileData templateCRUD_base = new CRUDFileData("GenTemplate", "/Generate", 1);
        CRUDFileData templateCRUD = new CRUDFileData("GenTemplate", "/Generate", 2);
        CRUDFileData newCRUD_base = null;
        CRUDFileData newCRUD = null;
        AttrParser attrParser = new AttrParser();

        public GenerateSourceService(ILogger<GenerateSourceService> logger)
        {
            _logger = logger;
        }

        internal void UpdateReferenceFile(string entityName)
        {

            string entityContent = File.ReadAllText(newCRUD_base.filePath_Entity);
            string tableName = "";
            var listAttr = attrParser.ParseAttributes(entityContent, ref tableName);
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = $"{entityName}s";
            }

            UpdateDBContextFile(entityName, listAttr, tableName);

            //UpdateStartupFile(entityName);

            UpdateRequestModel(entityName, listAttr);

            UpdateResponseModel(entityName, listAttr);

            UpdateService(entityName, listAttr);

            UpdateServiceImp(entityName, listAttr);

            UpdateRepository(entityName, listAttr, tableName);

            //UpdateController(entityName, listAttr);
        }

        internal void CheckFileEntity(string entityName)
        {
            newCRUD_base = new CRUDFileData(entityName, "", 3);
            newCRUD = new CRUDFileData(entityName, "", 4);
            if (File.Exists(newCRUD_base.filePath_Entity))
            {
            }
            else
            {
                throw new Exception($"{entityName}Entity.cs not found");
            }
        }

        internal void BackupEntity(string entityName)
        {
            if (File.Exists(newCRUD_base.filePath_Entity))
            {
                File.Copy(newCRUD_base.filePath_Entity, newCRUD_base.GetBackupFilePathEntity(), true);
            }
            else
            {
                throw new Exception($"{entityName}Entity.cs not found");
            }
        }

        internal void RemoveEntityBK(string entityName)
        {
            if (File.Exists(newCRUD_base.filePath_Entity))
            {
                File.Delete(newCRUD_base.GetBackupFilePathEntity());
            }
        }

        internal void CloneFile(string entityName)
        {
            for (int i = 0; i < templateCRUD_base.listFilePath.Count; i++)
            {
                string filePath = templateCRUD_base.listFilePath[i];
                if (File.Exists(filePath) && filePath != templateCRUD_base.filePath_Entity)
                {
                    var directory = newCRUD_base.listFilePath[i].GetDirectory();
                    if (!Path.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    File.Copy(filePath, newCRUD_base.listFilePath[i], true);
                }
            }

            for (int i = 0; i < templateCRUD.listFilePath.Count; i++)
            {
                string filePath = templateCRUD.listFilePath[i];
                if (File.Exists(filePath) && filePath != templateCRUD.filePath_Entity)
                {
                    var directory = newCRUD.listFilePath[i].GetDirectory();
                    if (!Path.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    if (!File.Exists(newCRUD.listFilePath[i]))
                    {
                        File.Copy(filePath, newCRUD.listFilePath[i], false);
                    }

                    //File.Copy(filePath, newCRUD.listFilePath[i], true);
                }
            }
        }

        internal void ReplaceFileData(string entityName)
        {
            foreach (var filePath in newCRUD_base.listFilePath)
            {
                string fileContent = File.ReadAllText(filePath);
                fileContent = fileContent.Replace(templateCRUD_base.name, newCRUD_base.name)
                    .Replace(templateCRUD_base.name.ToUpper(), newCRUD_base.name.ToUpper())
                    .Replace(templateCRUD_base.name.ToLower(), newCRUD_base.name.ToLower());
                File.WriteAllText(filePath, fileContent);
            }

            foreach (var filePath in newCRUD.listFilePath)
            {
                string fileContent = File.ReadAllText(filePath);
                fileContent = fileContent.Replace(templateCRUD.name, newCRUD.name)
                    .Replace(templateCRUD.name.ToUpper(), newCRUD.name.ToUpper())
                    .Replace(templateCRUD.name.ToLower(), newCRUD.name.ToLower());
                File.WriteAllText(filePath, fileContent);
            }
        }

        internal void UpdateByEntityBackup(string entityName)
        {
            if (File.Exists(newCRUD_base.GetBackupFilePathEntity()))
            {
                File.Copy(newCRUD_base.GetBackupFilePathEntity(), newCRUD_base.filePath_Entity, true);
            }
        }

        private bool IsExitsFileContent(string fileContent, string newStr)
        {
            string fileContentTemp = Regex.Replace(fileContent + "", @"\s", "");
            string searchStr = Regex.Replace(newStr, @"\s", "");
            if (fileContentTemp.IndexOf(searchStr) == -1)
            {
                return false;
            }
            return true;
        }
    }
}