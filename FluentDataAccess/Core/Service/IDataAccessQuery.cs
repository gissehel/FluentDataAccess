using System;

namespace FluentDataAccess.Core.Service
{
    public interface IDataAccessQuery
    {
        IDataAccessQuery WithParameter(string name, string value);

        IDataAccessQuery WithParameter(string name, short value);

        IDataAccessQuery WithParameter(string name, int value);

        IDataAccessQuery WithParameter(string name, long value);

        IDataAccessQuery WithParameter(string name, float value);

        IDataAccessQuery WithParameter(string name, double value);

        IDataAccessQuery WithParameter(string name, Guid value);

        IDataAccessQuery WithParameter(string name, Decimal value);

        IDataAccessQuery WithParameter(string name, byte value);

        IDataAccessQuery WithParameter(string name, bool value);

        IDataAccessQueryWithResult<T> Returning<T>() where T : new();

        IDataAccessQueryWithResult<T> Returning<T>(Func<T> entityFactory);

        IDataAccessQueryWithIndexedResult ReturningWithIndex();

        int Execute();
    }
}