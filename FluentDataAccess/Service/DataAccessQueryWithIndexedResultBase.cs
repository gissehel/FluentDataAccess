using System;
using System.Collections.Generic;
using System.Data.Common;

namespace FluentDataAccess.Service
{
    public class DataAccessQueryWithIndexedResultBase<C> : IDataAccessQueryWithIndexedResult where C : DbConnection
    {
        protected DataAccessQueryBase<C> DataAccessQuery { get; set; }

        protected Dictionary<string, Action<DbDataReader, int, int>> ReaderActionsByName { get; set; } = new Dictionary<string, Action<DbDataReader, int, int>>();

        public int Execute()
        {
            using (var command = DataAccessQuery.GetCommand())
            {
                using (var reader = command.ExecuteReader())
                {
                    int index = 0;
                    while (reader.Read())
                    {
                        foreach (var nameAndReaderAction in ReaderActionsByName)
                        {
                            var name = nameAndReaderAction.Key;
                            var readerAction = nameAndReaderAction.Value;
                            var position = reader.GetOrdinal(name);
                            readerAction(reader, position, index);
                        }
                        index++;
                    }
                    return index;
                }
            }
        }

        protected IDataAccessQueryWithIndexedResult Reading<R>(string name, Action<int, R> action, Func<DbDataReader, int, R> read)
        {
            ReaderActionsByName[name] = (reader, position, index) => action(index, read(reader, position));
            return this;
        }

        public IDataAccessQueryWithIndexedResult Reading(string name, Action<int, string> action) => Reading(name, action, (reader, position) => reader.GetString(position));

        public IDataAccessQueryWithIndexedResult Reading(string name, Action<int, short> action) => Reading(name, action, (reader, position) => reader.GetInt16(position));

        public IDataAccessQueryWithIndexedResult Reading(string name, Action<int, int> action) => Reading(name, action, (reader, position) => reader.GetInt32(position));

        public IDataAccessQueryWithIndexedResult Reading(string name, Action<int, long> action) => Reading(name, action, (reader, position) => reader.GetInt64(position));

        public IDataAccessQueryWithIndexedResult Reading(string name, Action<int, float> action) => Reading(name, action, (reader, position) => reader.GetFloat(position));

        public IDataAccessQueryWithIndexedResult Reading(string name, Action<int, double> action) => Reading(name, action, (reader, position) => reader.GetDouble(position));

        public IDataAccessQueryWithIndexedResult Reading(string name, Action<int, Guid> action) => Reading(name, action, (reader, position) => reader.GetGuid(position));

        public IDataAccessQueryWithIndexedResult Reading(string name, Action<int, decimal> action) => Reading(name, action, (reader, position) => reader.GetDecimal(position));

        public IDataAccessQueryWithIndexedResult Reading(string name, Action<int, DateTime> action) => Reading(name, action, (reader, position) => reader.GetDateTime(position));

        public IDataAccessQueryWithIndexedResult Reading(string name, Action<int, byte> action) => Reading(name, action, (reader, position) => reader.GetByte(position));

        public IDataAccessQueryWithIndexedResult Reading(string name, Action<int, bool> action) => Reading(name, action, (reader, position) => reader.GetBoolean(position));
    }
}