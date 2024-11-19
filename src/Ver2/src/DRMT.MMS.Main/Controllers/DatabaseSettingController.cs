using SourceBaseBE.Database.DBContexts;
using iSoft.DBLibrary.DBConnections.Factory;
using iSoft.DBLibrary.DBConnections.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;
using iSoft.Common.ConfigsNS;
using SourceBaseBE.MainService.Services;
using DRMT.Compute.Helpers;
using DRMT.MMS.MainDatabase.Migrations;

namespace SourceBaseBE.MainService.Controllers
{
	[ApiController]
	[Route("api/v1/DatabaseSetting")]
	public class DatabaseSettingController : ControllerBase
	{
		internal ILogger _logger = Serilog.Log.Logger;
		internal IServiceProvider provider;
		public DatabaseSettingController(IServiceProvider provider)
		{
			this.provider = provider;
		}

		//[Authorize(Roles = "Root")]
		[HttpPost("create-database")]
		public async Task<IActionResult> CreateDatabase()
		{
			try
			{
				IDBConnectionCustom dBConnectionCustom = DBConnectionFactory.CreateDBConnection(CommonConfig.MasterDatabaseConfig);
				var result = await CommonDBContext.CreateDatabase(dBConnectionCustom);
				return Ok($"Create DB Done, {result}");
			}
			catch (Exception ex)
			{
				var message = ex.Message;
				this._logger.LogError(ex.Message);
				return Ok(message);
			}
		}

		//[Authorize(Roles = "Root")]
		[HttpPost("delete-database")]
		public async Task<IActionResult> DeleteDatabase()
		{
			try
			{
				IDBConnectionCustom dBConnectionCustom = DBConnectionFactory.CreateDBConnection(CommonConfig.MasterDatabaseConfig);
				var result = await CommonDBContext.DeleteDatabase(dBConnectionCustom);
				return Ok($"Delete DB Done, {result}");
			}
			catch (Exception ex)
			{
				var message = ex.Message;
				this._logger.LogError(ex.Message);
				return Ok(message);
			}
		}

        //[Authorize(Roles = "Root")]
        [HttpPost("generate-migration")]
        public IActionResult CreateMigration(string migrationName)
        {
            try
            {
                if (string.IsNullOrEmpty(migrationName))
                {
                    return BadRequest("Migration name cannot be empty.");
                }

                var migrateCmd = MigrationHelper.GetGenerateMigrationCMD(migrationName);
                var migrateDirectory = MigrationHelper.GetDBWorkingDirectory();
                var result = CommandHelper.RunCommand(migrateDirectory, migrateCmd);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to generate migration: {ex.Message}");
            }
        }

        //[Authorize(Roles = "Root")]
        [HttpPost("apply-migration")]
        public IActionResult MigrateDatabase()
        {
            try
            {
                var migrateCmd = MigrationHelper.GetUpdateMigrationCMD();
                var migrateDirectory = MigrationHelper.GetDBWorkingDirectory();
                var result = CommandHelper.RunCommand(migrateDirectory, migrateCmd);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Update migration failed: {ex.Message}");
            }
        }
    }
}
