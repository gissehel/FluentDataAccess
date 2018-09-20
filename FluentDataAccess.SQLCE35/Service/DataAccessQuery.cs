using FluentDataAccess.Service;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlServerCe;

namespace FluentDataAccess.SQLCE35.Service
{
    internal class DataAccessQuery : DataAccessQueryBase<SqlCeConnection>
    {
        public DataAccessQuery(SqlCeConnection sqlCeConnection, string query)
        {
            Connection = sqlCeConnection;
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
            (collection as SqlCeParameterCollection).AddWithValue("@" + name, value);
        }

        public override IDataAccessQuery WithParameter(string name, string value) => WithParameter(name, value, SqlDbType.Text);

        public override IDataAccessQuery WithParameter(string name, short value) => WithParameter(name, value, SqlDbType.SmallInt);

        public override IDataAccessQuery WithParameter(string name, int value) => WithParameter(name, value, SqlDbType.Int);

        public override IDataAccessQuery WithParameter(string name, long value) => WithParameter(name, value, SqlDbType.BigInt);

        public override IDataAccessQuery WithParameter(string name, float value) => WithParameter(name, value, SqlDbType.Float);

        public override IDataAccessQuery WithParameter(string name, double value) => WithParameter(name, value, SqlDbType.Real);

        public override IDataAccessQuery WithParameter(string name, Guid value) => WithParameter(name, value, SqlDbType.UniqueIdentifier);

        public override IDataAccessQuery WithParameter(string name, decimal value) => WithParameter(name, value, SqlDbType.Decimal);

        public override IDataAccessQuery WithParameter(string name, DateTime value) => WithParameter(name, value, SqlDbType.DateTime);

        public override IDataAccessQuery WithParameter(string name, byte value) => WithParameter(name, value, SqlDbType.TinyInt);

        public override IDataAccessQuery WithParameter(string name, bool value) => WithParameter(name, value, SqlDbType.Bit);
    }
}