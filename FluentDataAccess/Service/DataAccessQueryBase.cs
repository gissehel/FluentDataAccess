using System;
using System.Collections.Generic;
using System.Data.Common;

namespace FluentDataAccess.Service
{
    public abstract class DataAccessQueryBase<C> : IDataAccessQuery where C : DbConnection
    {
        protected C Connection { get; set; }

        public string Query { get; set; }
        private List<Action<DbParameterCollection>> ParameterActions { get; set; } = new List<Action<DbParameterCollection>>();

        public int Execute()
        {
            using (var command = GetCommand())
            {
                return command.ExecuteNonQuery();
            }
        }

        public abstract IDataAccessQueryWithResult<T> Returning<T>() where T : new();

        public abstract IDataAccessQueryWithResult<T> Returning<T>(Func<T> entityFactory);

        public abstract IDataAccessQueryWithIndexedResult ReturningWithIndex();

        protected abstract void AddParameter<R, P>(DbParameterCollection collection, string name, P type, R value) where P : Enum;

        protected IDataAccessQuery WithParameter<R, P>(string name, R value, P type) where P : Enum
        {
            ParameterActions.Add(p => AddParameter(p, name, type, value));
            return this;
        }

        public abstract IDataAccessQuery WithParameter(string name, string value);

        public abstract IDataAccessQuery WithParameter(string name, short value);

        public abstract IDataAccessQuery WithParameter(string name, int value);

        public abstract IDataAccessQuery WithParameter(string name, long value);

        public abstract IDataAccessQuery WithParameter(string name, float value);

        public abstract IDataAccessQuery WithParameter(string name, double value);

        public abstract IDataAccessQuery WithParameter(string name, Guid value);

        public abstract IDataAccessQuery WithParameter(string name, decimal value);

        public abstract IDataAccessQuery WithParameter(string name, DateTime value);

        public abstract IDataAccessQuery WithParameter(string name, byte value);

        public abstract IDataAccessQuery WithParameter(string name, bool value);

        public DbCommand GetCommand()
        {
            var command = Connection.CreateCommand();
            command.CommandText = Query;
            foreach (var parameterAction in ParameterActions)
            {
                parameterAction(command.Parameters);
            }
            return command;
        }
    }
}