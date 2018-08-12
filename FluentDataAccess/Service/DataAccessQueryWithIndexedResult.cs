using FluentDataAccess.Core.Service;
using System;
using System.Collections.Generic;

#if NET35
using System.Data.SQLite;
#else

using Microsoft.Data.Sqlite;
using SQLiteDataReader = Microsoft.Data.Sqlite.SqliteDataReader;

#endif

namespace FluentDataAccess.Service
{
    internal class DataAccessQueryWithIndexedResult : IDataAccessQueryWithIndexedResult
    {
        private DataAccessQuery DataAccessQuery { get; set; }

        private Dictionary<string, Action<SQLiteDataReader, int, int>> ReaderActionsByName { get; set; } = new Dictionary<string, Action<SQLiteDataReader, int, int>>();

        public DataAccessQueryWithIndexedResult(DataAccessQuery dataAccessQuery)
        {
            DataAccessQuery = dataAccessQuery;
        }

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

        public IDataAccessQueryWithIndexedResult Reading(string name, Action<int, string> action)
        {
            ReaderActionsByName[name] = (reader, position, index) => action(index, reader.GetString(position));
            return this;
        }

        public IDataAccessQueryWithIndexedResult Reading(string name, Action<int, short> action)
        {
            ReaderActionsByName[name] = (reader, position, index) => action(index, reader.GetInt16(position));
            return this;
        }

        public IDataAccessQueryWithIndexedResult Reading(string name, Action<int, int> action)
        {
            ReaderActionsByName[name] = (reader, position, index) => action(index, reader.GetInt32(position));
            return this;
        }

        public IDataAccessQueryWithIndexedResult Reading(string name, Action<int, long> action)
        {
            ReaderActionsByName[name] = (reader, position, index) => action(index, reader.GetInt64(position));
            return this;
        }

        public IDataAccessQueryWithIndexedResult Reading(string name, Action<int, float> action)
        {
            ReaderActionsByName[name] = (reader, position, index) => action(index, reader.GetFloat(position));
            return this;
        }

        public IDataAccessQueryWithIndexedResult Reading(string name, Action<int, double> action)
        {
            ReaderActionsByName[name] = (reader, position, index) => action(index, reader.GetDouble(position));
            return this;
        }

        public IDataAccessQueryWithIndexedResult Reading(string name, Action<int, Guid> action)
        {
            ReaderActionsByName[name] = (reader, position, index) => action(index, reader.GetGuid(position));
            return this;
        }

        public IDataAccessQueryWithIndexedResult Reading(string name, Action<int, Decimal> action)
        {
            ReaderActionsByName[name] = (reader, position, index) => action(index, reader.GetDecimal(position));
            return this;
        }

        public IDataAccessQueryWithIndexedResult Reading(string name, Action<int, DateTime> action)
        {
            ReaderActionsByName[name] = (reader, position, index) => action(index, reader.GetDateTime(position));
            return this;
        }

        public IDataAccessQueryWithIndexedResult Reading(string name, Action<int, byte> action)
        {
            ReaderActionsByName[name] = (reader, position, index) => action(index, reader.GetByte(position));
            return this;
        }

        public IDataAccessQueryWithIndexedResult Reading(string name, Action<int, bool> action)
        {
            ReaderActionsByName[name] = (reader, position, index) => action(index, reader.GetBoolean(position));
            return this;
        }
    }
}