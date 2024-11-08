using System;
using System.Collections.Generic;

namespace SourceBaseBE.MainService.Services.Generate
{
    public class CRUDFileData
    {
        public string name { get; set; }
        //public string filePath_FormDataConvert { get; set; }
        public string filePath_Service { get; set; }
        public string filePath_Entity { get; set; }
        public string filePath_Controller { get; set; }
        public string filePath_RequestModel { get; set; }
        public string filePath_ResponseModel { get; set; }
        public string filePath_Repository { get; set; }
        public List<string> listFilePath = new List<string>();
        public CRUDFileData(string name, string subFolder, int optionFlag)
        {
            string generateFolderPath = Environment.GetEnvironmentVariable("GENERATE_PATH_FOLDER");
            string generateFolderDBPath = Environment.GetEnvironmentVariable("GENERATE_PATH_DB_FOLDER");
            this.name = name;
            switch (optionFlag)
            {
                case 1:
                    //this.filePath_FormDataConvert = $"{generateFolderPath}/FormDataConvertNS{subFolder}/{name}FormDataConvert.cs";
                    filePath_Service = $"{generateFolderPath}/Services{subFolder}/Base{name}Service.cs";
                    filePath_Entity = $"{generateFolderDBPath}/Entities{subFolder}/{name}Entity.cs";
                    //filePath_Controller = $"{generateFolderPath}/Controllers{subFolder}/Base{name}Controller.cs";
                    filePath_RequestModel = $"{generateFolderDBPath}/Models/RequestModels{subFolder}/Base{name}RequestModel.cs";
                    filePath_ResponseModel = $"{generateFolderDBPath}/Models/ResponseModels{subFolder}/Base{name}ResponseModel.cs";
                    filePath_Repository = $"{generateFolderDBPath}/Repository{subFolder}/Base{name}Repository.cs";
                    break;

                case 2:
                    //this.filePath_FormDataConvert = $"{generateFolderPath}/FormDataConvertNS{subFolder}/{name}FormDataConvert.cs";
                    filePath_Service = $"{generateFolderPath}/Services{subFolder}/{name}Service.cs";
                    filePath_Entity = $"{generateFolderDBPath}/Entities{subFolder}/{name}Entity.cs";
                    //filePath_Controller = $"{generateFolderPath}/Controllers{subFolder}/{name}Controller.cs";
                    filePath_RequestModel = $"{generateFolderDBPath}/Models/RequestModels{subFolder}/{name}RequestModel.cs";
                    filePath_ResponseModel = $"{generateFolderDBPath}/Models/ResponseModels{subFolder}/{name}ResponseModel.cs";
                    filePath_Repository = $"{generateFolderDBPath}/Repository{subFolder}/{name}Repository.cs";
                    break;

                case 3:
                    //this.filePath_FormDataConvert = $"{generateFolderPath}/FormDataConvertNS{subFolder}/{name}FormDataConvert.cs";
                    filePath_Service = $"{generateFolderPath}/Services{subFolder}/Base/Base{name}Service.cs";
                    filePath_Entity = $"{generateFolderDBPath}/Entities{subFolder}/{name}Entity.cs";
                    //filePath_Controller = $"{generateFolderPath}/Controllers{subFolder}/Base/Base{name}Controller.cs";
                    filePath_RequestModel = $"{generateFolderDBPath}/Models/RequestModels{subFolder}/Base/Base{name}RequestModel.cs";
                    filePath_ResponseModel = $"{generateFolderDBPath}/Models/ResponseModels{subFolder}/Base/Base{name}ResponseModel.cs";
                    filePath_Repository = $"{generateFolderDBPath}/Repository{subFolder}/Base/Base{name}Repository.cs";
                    break;

                case 4:
                    //this.filePath_FormDataConvert = $"{generateFolderPath}/FormDataConvertNS{subFolder}/{name}FormDataConvert.cs";
                    filePath_Service = $"{generateFolderPath}/Services{subFolder}/{name}Service.cs";
                    filePath_Entity = $"{generateFolderDBPath}/Entities{subFolder}/{name}Entity.cs";
                    //filePath_Controller = $"{generateFolderPath}/Controllers{subFolder}/{name}Controller.cs";
                    filePath_RequestModel = $"{generateFolderDBPath}/Models/RequestModels{subFolder}/{name}RequestModel.cs";
                    filePath_ResponseModel = $"{generateFolderDBPath}/Models/ResponseModels{subFolder}/{name}ResponseModel.cs";
                    filePath_Repository = $"{generateFolderDBPath}/Repository{subFolder}/{name}Repository.cs";
                    break;
            }
            //listFilePath.Add(filePath_FormDataConvert);
            listFilePath.Add(filePath_Service);
            listFilePath.Add(filePath_Entity);
            //listFilePath.Add(filePath_Controller);
            listFilePath.Add(filePath_RequestModel);
            listFilePath.Add(filePath_ResponseModel);
            listFilePath.Add(filePath_Repository);
        }

        internal string GetBackupFilePathEntity()
        {
            return filePath_Entity.Replace("Entity.cs", "Entity_Backup.txt");
        }
    }
}
