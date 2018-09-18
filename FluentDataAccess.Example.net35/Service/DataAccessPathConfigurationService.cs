namespace FluentDataAccess.Example.net35.Service
{
    public class DataAccessPathConfigurationService : IDataAccessPathConfigurationService
    {
        public string DatabasePath => ".";

        public string DatabaseName => "TestDB";
    }
}