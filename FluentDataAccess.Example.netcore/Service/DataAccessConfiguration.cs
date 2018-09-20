namespace FluentDataAccess.Example.netcore.Service
{
    public class DataAccessConfiguration : IDataAccessConfigurationByPath
    {
        public string DatabasePath => ".";

        public string DatabaseName => "TestDB";
    }
}