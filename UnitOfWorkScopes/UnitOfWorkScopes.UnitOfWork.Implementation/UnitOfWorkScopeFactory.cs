using System;
using System.Data;
using Microsoft.Extensions.DependencyInjection;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Scopes;

namespace UnitOfWorkScopes.UnitOfWork.Implementation
{
    public class UnitOfWorkScopeFactory : IUnitOfWorkScopeFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWorkScopeFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IUnitOfWorkScopeProxy Create(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            var scope = _serviceProvider
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<IUnitOfWorkScopeProxy>();

            scope.IsolationLevel = isolationLevel;

            return scope;
        }
    }
}
