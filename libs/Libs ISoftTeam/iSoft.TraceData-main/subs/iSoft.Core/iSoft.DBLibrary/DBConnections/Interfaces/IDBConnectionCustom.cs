using iSoft.Common.Enums.DBProvider;
using iSoft.Common.Models.ConfigModel.Subs;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace iSoft.DBLibrary.DBConnections.Interfaces
{
    public interface IDBConnectionCustom
    {
        public string GetConnectionString();
        public IDbConnection GetConnection();
        IDbCommand AddParam(IDbCommand command, string key, object value);
        public IDbCommand AddParam(IDbCommand command, string key, object value, string type = "");
        void SetOptionBuilder(ref DbContextOptionsBuilder optionsBuilder);
        public DBServerConfigModel GetDBConfig();
        public EnumDBProvider GetDBProvider();
    }
}
