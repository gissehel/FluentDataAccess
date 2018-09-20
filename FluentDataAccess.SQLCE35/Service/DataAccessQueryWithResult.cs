using FluentDataAccess.Service;
using System;
using System.Data.SqlServerCe;

namespace FluentDataAccess.SQLCE35.Service
{
    internal class DataAccessQueryWithResult<T> : DataAccessQueryWithResultBase<SqlCeConnection, T>
    {
        public DataAccessQueryWithResult(DataAccessQuery dataAccessQuery, Func<T> entityFactory)
        {
            DataAccessQuery = dataAccessQuery;
            EntityFactory = entityFactory;
        }
    }
}