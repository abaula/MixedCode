using System;
using EFCoreUnitOfWork.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EFCoreUnitOfWork.Implementation
{
    public class UnitOfWorkScope : IUnitOfWorkScope
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IUnitOfWorkScopeContextManager _contextManager;

        public UnitOfWorkScope(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _contextManager = _serviceProvider.GetService<IUnitOfWorkScopeContextManager>();
        }

        public void Dispose()
        {
            _contextManager.Dispose();
        }

        public TRepository GetRepository<TRepository>()
        {
            // Репозитории создаются внутри общего scope, что гарантирует наличие одного экземляра DbContext в репозитории.
            // Для правильной работы необходимо, чтобы время жизни DbContext было определено для scope.
            var repository = _serviceProvider.GetService<TRepository>();
            _contextManager.RegisterDbContext(repository.GetType());
            return repository;
        }

        public TWork GetWork<TWork>()
        {
            // Работы создаются внутри общего scope, что гарантирует наличие одного экземляра scope в работах созданных этим scope.
            // Для правильной работы необходимо, чтобы время жизни IUnitOfWorkScope было определено для scope.
            return _serviceProvider.GetService<TWork>();
        }

        public void Commit()
        {
            _contextManager.DoCommit();
        }
    }
}
