using FluentDataAccess.Core.Service;

namespace FluentDataAccess.Example.netcore.Service
{
    public class DataAccessConfigurationService : IDataAccessConfigurationService
    {
        public string ApplicationDataPath => ".";

        public string DatabaseName => "TestDB";
    }
}