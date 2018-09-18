using System;

namespace FluentDataAccess
{
    public interface IDataAccessService : IDisposable
    {
        void Init();

        IDataAccessQuery GetQuery(string query);
    }
}