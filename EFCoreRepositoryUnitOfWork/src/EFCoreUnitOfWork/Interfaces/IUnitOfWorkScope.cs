using System;

namespace EFCoreUnitOfWork.Interfaces
{
    public interface IUnitOfWorkScope : IDisposable
    {
        TRepository Get<TRepository>();
        void Commit();
    }
}
