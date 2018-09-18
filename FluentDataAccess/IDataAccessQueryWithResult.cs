using System.Collections.Generic;

namespace FluentDataAccess
{
    public interface IDataAccessQueryWithResult<T> : IDataAccessResultReader<IDataAccessQueryWithResult<T>, T>
    {
        IEnumerable<T> Execute();
    }
}