﻿using FluentDataAccess.Service;
using Microsoft.Data.Sqlite;

namespace FluentDataAccess.SQLite.Service
{
    internal class DataAccessQueryWithIndexedResult : DataAccessQueryWithIndexedResultBase<SqliteConnection>
    {
        public DataAccessQueryWithIndexedResult(DataAccessQuery dataAccessQuery)
        {
            DataAccessQuery = dataAccessQuery;
        }
    }
}