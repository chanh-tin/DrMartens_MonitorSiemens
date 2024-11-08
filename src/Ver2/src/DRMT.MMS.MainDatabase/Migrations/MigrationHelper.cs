using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRMT.MMS.MainDatabase.Migrations
{
    public static class MigrationHelper
    {
        //* script install dotnet-ef: dotnet tool install --global dotnet-ef

        public static string GetGenerateMigrationCMD(string migrationName)
        {
            string projectStartup = "DRMT.MMS.Main";
            string migrationFolder = "Migrations";
            return $"dotnet ef migrations add {migrationName} --context CommonDbContext --startup-project ../{projectStartup} --project . --output-dir {migrationFolder} --no-build";
        }

        public static string GetUpdateMigrationCMD()
        {
            string projectStartup = "DRMT.MMS.Main";
            return $"dotnet ef database update --context CommonDbContext --startup-project ../{projectStartup} --project . --no-build";
        }

        public static string GetDBWorkingDirectory()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            return currentDirectory + "Database";
        }
    }
}
