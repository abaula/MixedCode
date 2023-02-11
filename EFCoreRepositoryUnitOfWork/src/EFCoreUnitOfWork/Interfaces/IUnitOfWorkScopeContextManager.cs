using System;

namespace EFCoreUnitOfWork.Interfaces
{
    public interface IUnitOfWorkScopeContextManager : IDisposable
    {
        void RegisterDbContext(Type repositoryType);
        void DoCommit();
    }
}
