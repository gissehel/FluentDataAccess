using System;

namespace FluentDataAccess
{
    public interface IDataAccessResultReader<R, T> where R : IDataAccessResultReader<R, T>
    {
        R Reading(string name, Action<T, string> action);

        R Reading(string name, Action<T, short> action);

        R Reading(string name, Action<T, int> action);

        R Reading(string name, Action<T, long> action);

        R Reading(string name, Action<T, float> action);

        R Reading(string name, Action<T, double> action);

        R Reading(string name, Action<T, Guid> action);

        R Reading(string name, Action<T, decimal> action);

        R Reading(string name, Action<T, DateTime> action);

        R Reading(string name, Action<T, byte> action);

        R Reading(string name, Action<T, bool> action);
    }
}