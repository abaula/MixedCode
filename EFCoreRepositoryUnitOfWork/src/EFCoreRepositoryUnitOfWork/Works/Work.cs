using EFCoreRepositoryUnitOfWork.Repositories;
using EFCoreUnitOfWork.Interfaces;

namespace EFCoreRepositoryUnitOfWork.Works
{
    public class Work : IWork
    {
        private readonly IUnitOfWorkScope _scope;

        public Work(IUnitOfWorkScope scope)
        {
            _scope = scope;
        }

        public int ScopeHashCode => _scope.GetHashCode();

        public void DoWork()
        {
            _scope.GetRepository<IEditRepository>()
                .AddValue($"ScopeHashCode={_scope.GetHashCode()} - внутри IWork.");
        }
    }
}
