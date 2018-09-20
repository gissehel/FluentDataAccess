using FluentDataAccess.Service;
using System.Data.SqlServerCe;

namespace FluentDataAccess.SQLCE35.Service
{
    internal class DataAccessQueryWithIndexedResult : DataAccessQueryWithIndexedResultBase<SqlCeConnection>
    {
        public DataAccessQueryWithIndexedResult(DataAccessQuery dataAccessQuery)
        {
            DataAccessQuery = dataAccessQuery;
        }
    }
}