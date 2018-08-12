using System;

namespace FluentDataAccess.Core.Service
{
    public interface IDataAccessService : IDisposable
    {
        void Init();

        IDataAccessQuery GetQuery(string query);
    }
}