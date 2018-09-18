namespace FluentDataAccess
{
    public interface IDataAccessPathConfigurationService
    {
        string DatabasePath { get; }

        string DatabaseName { get; }
    }
}