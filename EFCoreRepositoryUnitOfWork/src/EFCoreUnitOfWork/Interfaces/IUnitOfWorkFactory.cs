using System;

namespace EFCoreUnitOfWork.Interfaces
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWorkScope Create();
    }
}
