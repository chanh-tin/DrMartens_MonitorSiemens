using SourceBaseBE.Database.DBContexts;
using iSoft.DBLibrary.DBConnections.Factory;
using iSoft.DBLibrary.DBConnections.Interfaces;
using iSoft.Common.Enums.DBProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;
using iSoft.Common.ConfigsNS;
using iSoft.InfluxDB.Services;
using Microsoft.Extensions.DependencyInjection;
using SourceBaseBE.MainService.Services;
using iSoft.Common.Enums;
using iSoft.Common.Utils;

namespace SourceBaseBE.MainService.Controllers
{
	[ApiController]
	[Route("api/v1/DatabaseSetting")]
	public class DatabaseSettingController : ControllerBase
	{
		internal ILogger _logger = Serilog.Log.Logger;
		internal IServiceProvider provider;
		internal UserService userService;
		public DatabaseSettingController(IServiceProvider provider, UserService userService)
		{
			this.provider = provider;
			this.userService = userService;
		}

		[Authorize(Roles = "Root")]
		[HttpPost("create-database")]
		public async Task<IActionResult> CreateDatabase()
		{
			try
			{
				IDBConnectionCustom dBConnectionCustom = DBConnectionFactory.CreateDBConnection(CommonConfig.GetConfig().MasterDatabaseConfig);
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

		[Authorize(Roles = "Root")]
		[HttpPost("delete-database")]
		public async Task<IActionResult> DeleteDatabase()
		{
			try
			{
				IDBConnectionCustom dBConnectionCustom = DBConnectionFactory.CreateDBConnection(CommonConfig.GetConfig().MasterDatabaseConfig);
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
	}
}
