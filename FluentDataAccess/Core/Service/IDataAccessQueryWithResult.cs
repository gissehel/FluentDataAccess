using System;
using System.Collections.Generic;

namespace FluentDataAccess.Core.Service
{
    public interface IDataAccessQueryWithResult<T>
    {
        IDataAccessQueryWithResult<T> Reading(string name, Action<T, string> action);

        IDataAccessQueryWithResult<T> Reading(string name, Action<T, short> action);

        IDataAccessQueryWithResult<T> Reading(string name, Action<T, int> action);

        IDataAccessQueryWithResult<T> Reading(string name, Action<T, long> action);

        IDataAccessQueryWithResult<T> Reading(string name, Action<T, float> action);

        IDataAccessQueryWithResult<T> Reading(string name, Action<T, double> action);

        IDataAccessQueryWithResult<T> Reading(string name, Action<T, Guid> action);

        IDataAccessQueryWithResult<T> Reading(string name, Action<T, decimal> action);

        IDataAccessQueryWithResult<T> Reading(string name, Action<T, DateTime> action);

        IDataAccessQueryWithResult<T> Reading(string name, Action<T, byte> action);

        IDataAccessQueryWithResult<T> Reading(string name, Action<T, bool> action);

        IEnumerable<T> Execute();
    }
}