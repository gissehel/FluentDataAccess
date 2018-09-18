using System;

namespace FluentDataAccess
{
    public interface IDataAccessQuery : IDataAccessParametrable<IDataAccessQuery>
    {
        IDataAccessQueryWithResult<T> Returning<T>() where T : new();

        IDataAccessQueryWithResult<T> Returning<T>(Func<T> entityFactory);

        IDataAccessQueryWithIndexedResult ReturningWithIndex();

        int Execute();
    }
}