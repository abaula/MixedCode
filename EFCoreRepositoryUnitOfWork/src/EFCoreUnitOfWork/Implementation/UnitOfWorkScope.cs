using System;
using EFCoreUnitOfWork.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EFCoreUnitOfWork.Implementation
{
    public class UnitOfWorkScope : IUnitOfWorkScope
    {
        private readonly IServiceScope _serviceScope;
        private readonly IUnitOfWorkScopeContextManager _contextManager;

        public UnitOfWorkScope(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.GetService<IServiceScopeFactory>();
            _serviceScope = scope.CreateScope();
            // Магия происходит здесь. IUnitOfWorkScopeContextManager создаётся в отдельном scope, 
            // и наследует его через конструктор.
            _contextManager = _serviceScope.ServiceProvider.GetService<IUnitOfWorkScopeContextManager>();
        }

        public void Dispose()
        {
            _contextManager.Dispose();
            _serviceScope.Dispose();
        }

        public TRepository GetRepository<TRepository>()
        {
            // Репозитории создаются из отдельного scope, что гарантирует наличие одного экземляра DbContext в репозитории.
            // Время жизни DbContext определено для scope.
            var repository = _serviceScope.ServiceProvider.GetService<TRepository>();
            _contextManager.RegisterDbContext(repository.GetType());
            return repository;
        }

        public void Commit()
        {
            _contextManager.DoCommit();
        }
    }
}
