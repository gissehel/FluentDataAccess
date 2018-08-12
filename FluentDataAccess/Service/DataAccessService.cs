using FluentDataAccess.Core.Service;
using System.Data.SQLite;
using System.IO;

namespace FluentDataAccess.Service
{
    public class DataAccessService : IDataAccessService

    {
        private IDataAccessConfigurationService DataAccessConfigurationService { get; set; }
        private string DataPath { get; set; } = ".";
        private string DatabaseName { get; set; } = ".";
        private SQLiteConnection SQLiteConnection { get; set; }

        public DataAccessService(IDataAccessConfigurationService dataAccessConfigurationService)
        {
            DataAccessConfigurationService = dataAccessConfigurationService;
            SQLiteConnection = null;
        }

        public void Init()
        {
            DataPath = DataAccessConfigurationService.ApplicationDataPath;
            DatabaseName = DataAccessConfigurationService.DatabaseName;
            if (!Directory.Exists(DataPath))
            {
                Directory.CreateDirectory(DataPath);
            }
            SQLiteConnection = new SQLiteConnection(string.Format(@"Data Source={0}\{1}.sqlite;Version=3;", DataPath, DatabaseName));
            SQLiteConnection.Open();
        }

        public void Dispose()
        {
            SQLiteConnection.Close();
        }

        public IDataAccessQuery GetQuery(string query)
        {
            return new DataAccessQuery(SQLiteConnection, query);
        }
    }
}