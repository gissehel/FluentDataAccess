namespace FluentDataAccess
{
    public static class DataAccessSQLite
    {
        public static IDataAccessService GetService(IDataAccessPathConfigurationService dataAccessPathConfigurationService)
        {
            return new SQLite.Service.DataAccessService(dataAccessPathConfigurationService);
        }
    }
}