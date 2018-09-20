using FluentDataAccess.Service;
using System.Data.SqlServerCe;
using System.IO;

namespace FluentDataAccess.SQLCE35.Service
{
    internal class DataAccessService : DataAccessServiceBase<SqlCeConnection>
    {
        private IDataAccessConfigurationByPathWithPassword ConfigurationService { get; set; }
        private string DataPath { get; set; } = ".";
        private string DatabaseName { get; set; } = ".";

        public DataAccessService(IDataAccessConfigurationByPathWithPassword dataAccessPathConfigurationWithPasswordService)
        {
            ConfigurationService = dataAccessPathConfigurationWithPasswordService;
            Connection = null;
        }

        protected override IDataAccessQuery GetDataAccessQuery(SqlCeConnection connection, string query)
        {
            return new DataAccessQuery(connection, query);
        }

        protected override SqlCeConnection GetConnection()
        {
            DataPath = ConfigurationService.DatabasePath;
            DatabaseName = ConfigurationService.DatabaseName;
            var password = ConfigurationService.Password;
            if (!Directory.Exists(DataPath))
            {
                Directory.CreateDirectory(DataPath);
            }

            var filename = Path.Combine(DataPath, DatabaseName + ".db");
            var connString = string.Format(@"Data Source={0};Password={1}", filename, password);
            if (!File.Exists(filename))
            {
                var engine = new SqlCeEngine(connString);
                engine.CreateDatabase();
            }
            return new SqlCeConnection(connString);
        }
    }
}