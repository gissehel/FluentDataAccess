using System;
using System.Collections.Generic;

#if NET35
using System.Data.SQLite;
#else

using Microsoft.Data.Sqlite;
using SQLiteConnection = Microsoft.Data.Sqlite.SqliteConnection;
using SQLiteParameterCollection = Microsoft.Data.Sqlite.SqliteParameterCollection;
using SQLiteCommand = Microsoft.Data.Sqlite.SqliteCommand;

#endif

namespace FluentDataAccess.SQLite.Service
{
    internal class DataAccessQuery : IDataAccessQuery
    {
        public SQLiteConnection SQLiteConnection { get; set; }
        public string Query { get; set; }

        private List<Action<SQLiteParameterCollection>> ParameterActions { get; set; } = new List<Action<SQLiteParameterCollection>>();

        public DataAccessQuery(SQLiteConnection sqliteConnection, string query)
        {
            SQLiteConnection = sqliteConnection;
            Query = query;
        }

        public int Execute()
        {
            using (var command = GetCommand())
            {
                return command.ExecuteNonQuery();
            }
        }

        public IDataAccessQueryWithResult<T> Returning<T>() where T : new()
        {
            return new DataAccessQueryWithResult<T>(this, () => new T());
        }

        public IDataAccessQueryWithResult<T> Returning<T>(Func<T> entityFactory)
        {
            return new DataAccessQueryWithResult<T>(this, entityFactory);
        }

        public IDataAccessQueryWithIndexedResult ReturningWithIndex()
        {
            return new DataAccessQueryWithIndexedResult(this);
        }

        public IDataAccessQuery WithParameter(string name, string value)
        {
            ParameterActions.Add(p => p.Add
            (
                "@" + name,
#if NET35
                System.Data.DbType.String
#else
                Microsoft.Data.Sqlite.SqliteType.Text
#endif
            ).Value = value);
            return this;
        }

        public IDataAccessQuery WithParameter(string name, short value)
        {
            ParameterActions.Add(p => p.Add("@" + name,
#if NET35
                System.Data.DbType.Int16
#else
                Microsoft.Data.Sqlite.SqliteType.Integer
#endif
                ).Value = value);
            return this;
        }

        public IDataAccessQuery WithParameter(string name, int value)
        {
            ParameterActions.Add(p => p.Add
            (
                "@" + name,
#if NET35
                System.Data.DbType.Int32
#else
                Microsoft.Data.Sqlite.SqliteType.Integer
#endif
            ).Value = value);
            return this;
        }

        public IDataAccessQuery WithParameter(string name, long value)
        {
            ParameterActions.Add(p => p.Add
            (
                "@" + name,
#if NET35
                System.Data.DbType.Int64
#else
                Microsoft.Data.Sqlite.SqliteType.Integer
#endif
            ).Value = value);
            return this;
        }

        public IDataAccessQuery WithParameter(string name, float value)
        {
            ParameterActions.Add(p => p.Add
            (
                "@" + name,
#if NET35
                System.Data.DbType.Single
#else
                Microsoft.Data.Sqlite.SqliteType.Real
#endif
            ).Value = value);
            return this;
        }

        public IDataAccessQuery WithParameter(string name, double value)
        {
            ParameterActions.Add(p => p.Add
            (
                "@" + name,
#if NET35
                System.Data.DbType.Double
#else
                Microsoft.Data.Sqlite.SqliteType.Real
#endif
            ).Value = value);
            return this;
        }

        public IDataAccessQuery WithParameter(string name, Guid value)
        {
            ParameterActions.Add(p => p.Add
            (
                "@" + name,
#if NET35
                System.Data.DbType.Guid
#else
                Microsoft.Data.Sqlite.SqliteType.Text
#endif
            ).Value = value);
            return this;
        }

        public IDataAccessQuery WithParameter(string name, decimal value)
        {
            ParameterActions.Add(p => p.Add
            (
                "@" + name,
#if NET35
                System.Data.DbType.Decimal
#else
                Microsoft.Data.Sqlite.SqliteType.Real
#endif
            ).Value = value);
            return this;
        }

        public IDataAccessQuery WithParameter(string name, DateTime value)
        {
            ParameterActions.Add(p => p.Add
            (
                "@" + name,
#if NET35
                System.Data.DbType.DateTime
#else
                Microsoft.Data.Sqlite.SqliteType.Text
#endif
            ).Value = value);
            return this;
        }

        public IDataAccessQuery WithParameter(string name, byte value)
        {
            ParameterActions.Add(p => p.Add
            (
                "@" + name,
#if NET35
                System.Data.DbType.Byte
#else
                Microsoft.Data.Sqlite.SqliteType.Integer
#endif
            ).Value = value);
            return this;
        }

        public IDataAccessQuery WithParameter(string name, bool value)
        {
            ParameterActions.Add(p => p.Add
            (
                "@" + name,
#if NET35
                System.Data.DbType.Boolean
#else
                Microsoft.Data.Sqlite.SqliteType.Integer
#endif
            ).Value = value);
            return this;
        }

        public SQLiteCommand GetCommand()
        {
            var command = new SQLiteCommand(Query, SQLiteConnection);
            foreach (var parameterAction in ParameterActions)
            {
                parameterAction(command.Parameters);
            }
            return command;
        }
    }
}