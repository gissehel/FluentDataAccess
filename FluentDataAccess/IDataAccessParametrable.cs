using System;

namespace FluentDataAccess
{
    public interface IDataAccessParametrable<R> where R : IDataAccessParametrable<R>
    {
        R WithParameter(string name, string value);

        R WithParameter(string name, short value);

        R WithParameter(string name, int value);

        R WithParameter(string name, long value);

        R WithParameter(string name, float value);

        R WithParameter(string name, double value);

        R WithParameter(string name, Guid value);

        R WithParameter(string name, decimal value);

        R WithParameter(string name, byte value);

        R WithParameter(string name, bool value);
    }
}