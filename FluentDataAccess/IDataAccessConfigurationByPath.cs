namespace FluentDataAccess
{
    public interface IDataAccessConfigurationByPath
    {
        string DatabasePath { get; }

        string DatabaseName { get; }
    }
}