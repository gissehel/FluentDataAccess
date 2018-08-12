namespace FluentDataAccess.Core.Service
{
    public interface IDataAccessConfigurationService
    {
        string ApplicationDataPath { get; }

        string DatabaseName { get; }
    }
}