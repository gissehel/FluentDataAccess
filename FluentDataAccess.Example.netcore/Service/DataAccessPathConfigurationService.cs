namespace FluentDataAccess.Example.netcore.Service
{
    public class DataAccessPathConfigurationService : IDataAccessPathConfigurationService
    {
        public string DatabasePath => ".";

        public string DatabaseName => "TestDB";
    }
}