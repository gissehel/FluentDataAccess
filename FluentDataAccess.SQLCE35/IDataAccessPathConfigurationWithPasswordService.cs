namespace FluentDataAccess.SQLCE35
{
    public interface IDataAccessConfigurationByPathWithPassword : IDataAccessConfigurationByPath
    {
        string Password { get; }
    }
}