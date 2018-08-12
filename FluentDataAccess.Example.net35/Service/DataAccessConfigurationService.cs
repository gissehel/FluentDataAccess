using FluentDataAccess.Core.Service;

namespace FluentDataAccess.Example.net35.Service
{
    public class DataAccessConfigurationService : IDataAccessConfigurationService
    {
        public string ApplicationDataPath => ".";

        public string DatabaseName => "TestDB";
    }
}