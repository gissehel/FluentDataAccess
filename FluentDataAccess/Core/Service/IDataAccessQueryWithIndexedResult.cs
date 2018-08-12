using System;

namespace FluentDataAccess.Core.Service
{
    public interface IDataAccessQueryWithIndexedResult
    {
        IDataAccessQueryWithIndexedResult Reading(string name, Action<int, string> action);

        IDataAccessQueryWithIndexedResult Reading(string name, Action<int, short> action);

        IDataAccessQueryWithIndexedResult Reading(string name, Action<int, int> action);

        IDataAccessQueryWithIndexedResult Reading(string name, Action<int, long> action);

        IDataAccessQueryWithIndexedResult Reading(string name, Action<int, float> action);

        IDataAccessQueryWithIndexedResult Reading(string name, Action<int, double> action);

        IDataAccessQueryWithIndexedResult Reading(string name, Action<int, Guid> action);

        IDataAccessQueryWithIndexedResult Reading(string name, Action<int, decimal> action);

        IDataAccessQueryWithIndexedResult Reading(string name, Action<int, DateTime> action);

        IDataAccessQueryWithIndexedResult Reading(string name, Action<int, byte> action);

        IDataAccessQueryWithIndexedResult Reading(string name, Action<int, bool> action);

        int Execute();
    }
}