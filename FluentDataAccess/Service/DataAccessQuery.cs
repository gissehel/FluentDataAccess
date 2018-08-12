using FluentDataAccess.Core.Service;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace FluentDataAccess.Service
{
    public class DataAccessQuery : IDataAccessQuery
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
            ParameterActions.Add(p => p.Add("@" + name, System.Data.DbType.String).Value = value);
            return this;
        }

        public IDataAccessQuery WithParameter(string name, short value)
        {
            ParameterActions.Add(p => p.Add("@" + name, System.Data.DbType.Int16).Value = value);
            return this;
        }

        public IDataAccessQuery WithParameter(string name, int value)
        {
            ParameterActions.Add(p => p.Add("@" + name, System.Data.DbType.Int32).Value = value);
            return this;
        }

        public IDataAccessQuery WithParameter(string name, long value)
        {
            ParameterActions.Add(p => p.Add("@" + name, System.Data.DbType.Int64).Value = value);
            return this;
        }

        public IDataAccessQuery WithParameter(string name, float value)
        {
            ParameterActions.Add(p => p.Add("@" + name, System.Data.DbType.Single).Value = value);
            return this;
        }

        public IDataAccessQuery WithParameter(string name, double value)
        {
            ParameterActions.Add(p => p.Add("@" + name, System.Data.DbType.Double).Value = value);
            return this;
        }

        public IDataAccessQuery WithParameter(string name, Guid value)
        {
            ParameterActions.Add(p => p.Add("@" + name, System.Data.DbType.Guid).Value = value);
            return this;
        }

        public IDataAccessQuery WithParameter(string name, decimal value)
        {
            ParameterActions.Add(p => p.Add("@" + name, System.Data.DbType.Decimal).Value = value);
            return this;
        }

        public IDataAccessQuery WithParameter(string name, DateTime value)
        {
            ParameterActions.Add(p => p.Add("@" + name, System.Data.DbType.DateTime).Value = value);
            return this;
        }

        public IDataAccessQuery WithParameter(string name, byte value)
        {
            ParameterActions.Add(p => p.Add("@" + name, System.Data.DbType.Byte).Value = value);
            return this;
        }

        public IDataAccessQuery WithParameter(string name, bool value)
        {
            ParameterActions.Add(p => p.Add("@" + name, System.Data.DbType.Boolean).Value = value);
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