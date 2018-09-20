using FluentDataAccess.SQLCE35;

namespace FluentDataAccess.Example.net35.Service
{
    public class DataAccessConfiguration : IDataAccessConfigurationByPathWithPassword
    {
        public string DatabasePath => ".";

        public string DatabaseName => "TestDB";

        public string Password => "Grut";
    }
}