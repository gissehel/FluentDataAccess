using System.Data.Common;

namespace FluentDataAccess.Service
{
    public abstract class DataAccessServiceBase<C> : IDataAccessService where C : DbConnection
    {
        protected C Connection { get; set; }

        public void Dispose()
        {
            Connection?.Close();
            Connection = null;
        }

        public IDataAccessQuery GetQuery(string query)
        {
            if (Connection != null)
            {
                return GetDataAccessQuery(Connection, query);
            }
            return null;
        }

        private bool _isInit = false;

        public void Init()
        {
            if (!_isInit)
            {
                Connection = GetConnection();
                Connection.Open();
                _isInit = true;
            }
        }

        protected abstract IDataAccessQuery GetDataAccessQuery(C connection, string query);

        protected abstract C GetConnection();
    }
}