﻿using FluentDataAccess.Service;
using Microsoft.Data.Sqlite;
using System;

namespace FluentDataAccess.SQLite.Service
{
    internal class DataAccessQueryWithResult<T> : DataAccessQueryWithResultBase<SqliteConnection, T>
    {
        public DataAccessQueryWithResult(DataAccessQuery dataAccessQuery, Func<T> entityFactory)
        {
            DataAccessQuery = dataAccessQuery;
            EntityFactory = entityFactory;
        }
    }
}