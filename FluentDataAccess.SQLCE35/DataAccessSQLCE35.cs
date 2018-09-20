using FluentDataAccess.SQLCE35;

namespace FluentDataAccess
{
    public static class DataAccessSQLCE35
    {
        public static IDataAccessService GetService(IDataAccessConfigurationByPathWithPassword dataAccessPathConfigurationWithPasswordService)
        {
            return new SQLCE35.Service.DataAccessService(dataAccessPathConfigurationWithPasswordService);
        }
    }
}