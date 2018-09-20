using FluentDataAccess.Service;
using Microsoft.Data.Sqlite;
using System.IO;

namespace FluentDataAccess.SQLite.Service
{
    internal class DataAccessService : DataAccessServiceBase<SqliteConnection>
    {
        private IDataAccessConfigurationByPath ConfigurationService { get; set; }
        private string DataPath { get; set; } = ".";
        private string DatabaseName { get; set; } = ".";

        public DataAccessService(IDataAccessConfigurationByPath dataAccessPathConfigurationService)
        {
            ConfigurationService = dataAccessPathConfigurationService;
            Connection = null;
        }

        protected override IDataAccessQuery GetDataAccessQuery(SqliteConnection connection, string query)
            => new DataAccessQuery(connection, query);

        protected override SqliteConnection GetConnection()
        {
            DataPath = ConfigurationService.DatabasePath;
            DatabaseName = ConfigurationService.DatabaseName;
            if (!Directory.Exists(DataPath))
            {
                Directory.CreateDirectory(DataPath);
            }
            return new SqliteConnection(string.Format(@"Data Source={0}", Path.Combine(DataPath, DatabaseName + ".sqlite")));
        }
    }
}