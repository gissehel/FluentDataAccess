#if NET35

using System.Data.SQLite;

#else

using Microsoft.Data.Sqlite;
using SQLiteConnection = Microsoft.Data.Sqlite.SqliteConnection;

#endif

using System.IO;

namespace FluentDataAccess.SQLite.Service
{
    internal class DataAccessService : IDataAccessService
    {
        private IDataAccessPathConfigurationService DataAccessPathConfigurationService { get; set; }
        private string DataPath { get; set; } = ".";
        private string DatabaseName { get; set; } = ".";
        private SQLiteConnection SQLiteConnection { get; set; }

        public DataAccessService(IDataAccessPathConfigurationService dataAccessPathConfigurationService)
        {
            DataAccessPathConfigurationService = dataAccessPathConfigurationService;
            SQLiteConnection = null;
        }

        public void Init()
        {
            DataPath = DataAccessPathConfigurationService.DatabasePath;
            DatabaseName = DataAccessPathConfigurationService.DatabaseName;
            if (!Directory.Exists(DataPath))
            {
                Directory.CreateDirectory(DataPath);
            }
            SQLiteConnection = new SQLiteConnection(string.Format(@"Data Source={0}", Path.Combine(DataPath, DatabaseName + ".sqlite")));
            SQLiteConnection.Open();
        }

        public void Dispose()
        {
            SQLiteConnection?.Close();
            SQLiteConnection = null;
        }

        public IDataAccessQuery GetQuery(string query)
        {
            if (SQLiteConnection != null)
            {
                return new DataAccessQuery(SQLiteConnection, query);
            }
            return null;
        }
    }
}