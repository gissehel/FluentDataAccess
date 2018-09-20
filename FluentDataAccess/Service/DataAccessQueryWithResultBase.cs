using System;
using System.Collections.Generic;
using System.Data.Common;

namespace FluentDataAccess.Service
{
    public class DataAccessQueryWithResultBase<C, T> : IDataAccessQueryWithResult<T> where C : DbConnection
    {
        protected DataAccessQueryBase<C> DataAccessQuery { get; set; }

        protected Func<T> EntityFactory { get; set; }

        private Dictionary<string, Action<DbDataReader, int, T>> ReaderActionsByName { get; set; } = new Dictionary<string, Action<DbDataReader, int, T>>();

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

        protected IDataAccessQueryWithResult<T> Reading<R>(string name, Action<T, R> action, Func<DbDataReader, int, R> read)
        {
            ReaderActionsByName[name] = (reader, position, entity) => action(entity, read(reader, position));
            return this;
        }

        public IDataAccessQueryWithResult<T> Reading(string name, Action<T, string> action) => Reading(name, action, (reader, position) => reader.GetString(position));

        public IDataAccessQueryWithResult<T> Reading(string name, Action<T, short> action) => Reading(name, action, (reader, position) => reader.GetInt16(position));

        public IDataAccessQueryWithResult<T> Reading(string name, Action<T, int> action) => Reading(name, action, (reader, position) => reader.GetInt32(position));

        public IDataAccessQueryWithResult<T> Reading(string name, Action<T, long> action) => Reading(name, action, (reader, position) => reader.GetInt64(position));

        public IDataAccessQueryWithResult<T> Reading(string name, Action<T, float> action) => Reading(name, action, (reader, position) => reader.GetFloat(position));

        public IDataAccessQueryWithResult<T> Reading(string name, Action<T, double> action) => Reading(name, action, (reader, position) => reader.GetDouble(position));

        public IDataAccessQueryWithResult<T> Reading(string name, Action<T, Guid> action) => Reading(name, action, (reader, position) => reader.GetGuid(position));

        public IDataAccessQueryWithResult<T> Reading(string name, Action<T, decimal> action) => Reading(name, action, (reader, position) => reader.GetDecimal(position));

        public IDataAccessQueryWithResult<T> Reading(string name, Action<T, DateTime> action) => Reading(name, action, (reader, position) => reader.GetDateTime(position));

        public IDataAccessQueryWithResult<T> Reading(string name, Action<T, byte> action) => Reading(name, action, (reader, position) => reader.GetByte(position));

        public IDataAccessQueryWithResult<T> Reading(string name, Action<T, bool> action) => Reading(name, action, (reader, position) => reader.GetBoolean(position));
    }
}