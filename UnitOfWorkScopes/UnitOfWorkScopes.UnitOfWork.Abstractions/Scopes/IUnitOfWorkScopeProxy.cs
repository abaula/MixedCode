using System;
using System.Data;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Contexts;

namespace UnitOfWorkScopes.UnitOfWork.Abstractions.Scopes
{
    public interface IUnitOfWorkScopeProxy : IDisposable
    {
        IsolationLevel IsolationLevel { get; set; }
        void RegisterContext(ISharedContext context);
        T Get<T>();
        void Commit();
        void Rollback();
    }
}
