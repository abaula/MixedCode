using System;

namespace UnitOfWorkScopes.UnitOfWork.Abstractions.Contexts
{
    public interface ISharedContext : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
