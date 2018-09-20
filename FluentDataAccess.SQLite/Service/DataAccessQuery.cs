using FluentDataAccess.Service;
using Microsoft.Data.Sqlite;
using System;
using System.Data.Common;

namespace FluentDataAccess.SQLite.Service
{
    internal class DataAccessQuery : DataAccessQueryBase<SqliteConnection>
    {
        public DataAccessQuery(SqliteConnection sqliteConnection, string query)
        {
            Connection = sqliteConnection;
            Query = query;
        }

        public override IDataAccessQueryWithResult<T> Returning<T>()
            => new DataAccessQueryWithResult<T>(this, () => new T());

        public override IDataAccessQueryWithResult<T> Returning<T>(Func<T> entityFactory)
            => new DataAccessQueryWithResult<T>(this, entityFactory);

        public override IDataAccessQueryWithIndexedResult ReturningWithIndex()
            => new DataAccessQueryWithIndexedResult(this);

        protected override void AddParameter<R, P>(DbParameterCollection collection, string name, P type, R value)
        {
            SqliteType typeImplemented = (SqliteType)Convert.ToInt32(type);
            var parameter = (collection as SqliteParameterCollection).Add("@" + name, typeImplemented);
            parameter.Value = value;
        }

        public override IDataAccessQuery WithParameter(string name, string value) => WithParameter(name, value, SqliteType.Text);

        public override IDataAccessQuery WithParameter(string name, short value) => WithParameter(name, value, SqliteType.Integer);

        public override IDataAccessQuery WithParameter(string name, int value) => WithParameter(name, value, SqliteType.Integer);

        public override IDataAccessQuery WithParameter(string name, long value) => WithParameter(name, value, SqliteType.Integer);

        public override IDataAccessQuery WithParameter(string name, float value) => WithParameter(name, value, SqliteType.Real);

        public override IDataAccessQuery WithParameter(string name, double value) => WithParameter(name, value, SqliteType.Real);

        public override IDataAccessQuery WithParameter(string name, Guid value) => WithParameter(name, value, SqliteType.Text);

        public override IDataAccessQuery WithParameter(string name, decimal value) => WithParameter(name, value, SqliteType.Real);

        public override IDataAccessQuery WithParameter(string name, DateTime value) => WithParameter(name, value, SqliteType.Text);

        public override IDataAccessQuery WithParameter(string name, byte value) => WithParameter(name, value, SqliteType.Integer);

        public override IDataAccessQuery WithParameter(string name, bool value) => WithParameter(name, value, SqliteType.Integer);
    }
}