using System;

namespace EFCoreUnitOfWork.Interfaces
{
    public interface IUnitOfWorkScope : IDisposable
    {
        TRepository GetRepository<TRepository>();
        void Commit();
    }
}
