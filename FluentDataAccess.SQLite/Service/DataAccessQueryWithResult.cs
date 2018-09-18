using System;
using System.Collections.Generic;

#if NET35
using System.Data.SQLite;
#else

using Microsoft.Data.Sqlite;
using SQLiteDataReader = Microsoft.Data.Sqlite.SqliteDataReader;

#endif

namespace FluentDataAccess.SQLite.Service
{
    internal class DataAccessQueryWithResult<T> : IDataAccessQueryWithResult<T>
    {
        private DataAccessQuery DataAccessQuery { get; set; }

        private Func<T> EntityFactory { get; set; }

        private Dictionary<string, Action<SQLiteDataReader, int, T>> ReaderActionsByName { get; set; } = new Dictionary<string, Action<SQLiteDataReader, int, T>>();

        public DataAccessQueryWithResult(DataAccessQuery dataAccessQuery, Func<T> entityFactory)
        {
            DataAccessQuery = dataAccessQuery;
            EntityFactory = entityFactory;
        }

        public IEnumerable<T> Execute()
        {
            using (var command = DataAccessQuery.GetCommand())
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        T entity = EntityFactory();
                        foreach (var nameAndReaderAction in ReaderActionsByName)
                        {
                            var name = nameAndReaderAction.Key;
                            var readerAction = nameAndReaderAction.Value;
                            var position = reader.GetOrdinal(name);
                            readerAction(reader, position, entity);
                        }
                        yield return entity;
                    }
                }
            }
        }

        public IDataAccessQueryWithResult<T> Reading(string name, Action<T, string> action)
        {
            ReaderActionsByName[name] = (reader, position, entity) => action(entity, reader.GetString(position));
            return this;
        }

        public IDataAccessQueryWithResult<T> Reading(string name, Action<T, short> action)
        {
            ReaderActionsByName[name] = (reader, position, entity) => action(entity, reader.GetInt16(position));
            return this;
        }

        public IDataAccessQueryWithResult<T> Reading(string name, Action<T, int> action)
        {
            ReaderActionsByName[name] = (reader, position, entity) => action(entity, reader.GetInt32(position));
            return this;
        }

        public IDataAccessQueryWithResult<T> Reading(string name, Action<T, long> action)
        {
            ReaderActionsByName[name] = (reader, position, entity) => action(entity, reader.GetInt64(position));
            return this;
        }

        public IDataAccessQueryWithResult<T> Reading(string name, Action<T, float> action)
        {
            ReaderActionsByName[name] = (reader, position, entity) => action(entity, reader.GetFloat(position));
            return this;
        }

        public IDataAccessQueryWithResult<T> Reading(string name, Action<T, double> action)
        {
            ReaderActionsByName[name] = (reader, position, entity) => action(entity, reader.GetDouble(position));
            return this;
        }

        public IDataAccessQueryWithResult<T> Reading(string name, Action<T, Guid> action)
        {
            ReaderActionsByName[name] = (reader, position, entity) => action(entity, reader.GetGuid(position));
            return this;
        }

        public IDataAccessQueryWithResult<T> Reading(string name, Action<T, Decimal> action)
        {
            ReaderActionsByName[name] = (reader, position, entity) => action(entity, reader.GetDecimal(position));
            return this;
        }

        public IDataAccessQueryWithResult<T> Reading(string name, Action<T, DateTime> action)
        {
            ReaderActionsByName[name] = (reader, position, entity) => action(entity, reader.GetDateTime(position));
            return this;
        }

        public IDataAccessQueryWithResult<T> Reading(string name, Action<T, byte> action)
        {
            ReaderActionsByName[name] = (reader, position, entity) => action(entity, reader.GetByte(position));
            return this;
        }

        public IDataAccessQueryWithResult<T> Reading(string name, Action<T, bool> action)
        {
            ReaderActionsByName[name] = (reader, position, entity) => action(entity, reader.GetBoolean(position));
            return this;
        }
    }
}