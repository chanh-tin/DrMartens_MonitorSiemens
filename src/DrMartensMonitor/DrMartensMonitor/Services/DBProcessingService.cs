using DrMartensMonitor.Enities;
using DrMartensMonitor.Repository;
using iSoft.Common.Models.ConfigModel.Subs;
using iSoft.Database.DBContexts;
using iSoft.DBLibrary.DBConnections.Factory;
using iSoft.DBLibrary.DBConnections.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrMartensMonitor.Services
{
  public class DBProcessingService : BaseService
  {
    private IDBConnectionCustom _connCus;
    private CommonDBContext _dbContext = null;
    private IDbConnection conn;

    public DBProcessingService()
    {
      DBServerConfigModel dbConfig = new DBServerConfigModel(
      iSoft.Common.Enums.DBProvider.EnumDBProvider.Postgres,
      "localhost", //localhost
      2601, // 2601
      "DrMartens", // gdb_config_db
      "postgres", //postgres
      "8wEzA1TMKby9eQsWXBaupj52"); //8wEzA1TMKby9eQsWXBaupj52

      this._connCus = DBConnectionFactory.CreateDBConnection(dbConfig);
      this._dbContext = new CommonDBContext(_connCus);
    }
    public async Task ConnectDB()
    {
      try
      {
        await _dbContext.Database.EnsureCreatedAsync();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"DbProcessingServices ConnectDB:{ex.Message}\r\n{ex.StackTrace}");
      }
    }

    public void InsertDBPayLoad(string nameMachine)
    {
      DBDataMonitorRepository dBDataMonitorRepository = new DBDataMonitorRepository(this._dbContext);
      DBDataMonitorEnity dB_DataMonitor = new DBDataMonitorEnity()
      {
        NameMachine = nameMachine,
        //ProductID = productid,
        //MessageID = messageid,
        //Status = status
      };
      //dBPayLoadRepository.Insert(dB_DataMonitor);
    }

  }
}
