namespace FluentDataAccess
{
    public static class DataAccessSQLite
    {
        public static IDataAccessService GetService(IDataAccessConfigurationByPath dataAccessPathConfigurationService)
            => new SQLite.Service.DataAccessService(dataAccessPathConfigurationService);
    }
}