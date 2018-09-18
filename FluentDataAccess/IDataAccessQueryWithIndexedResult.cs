namespace FluentDataAccess
{
    public interface IDataAccessQueryWithIndexedResult : IDataAccessResultReader<IDataAccessQueryWithIndexedResult, int>
    {
        int Execute();
    }
}