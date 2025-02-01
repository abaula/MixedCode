using EFCoreUnitOfWork.Interfaces;

namespace EFCoreRepositoryUnitOfWork.Works
{
    public interface IWork
    {
        int ScopeHashCode { get; }
        void DoWork();
    }
}
