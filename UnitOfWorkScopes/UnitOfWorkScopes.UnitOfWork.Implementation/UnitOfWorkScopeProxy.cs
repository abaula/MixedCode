using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.DependencyInjection;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Contexts;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Scopes;

namespace UnitOfWorkScopes.UnitOfWork.Implementation
{
    public class UnitOfWorkScopeProxy : IUnitOfWorkScopeProxy
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly HashSet<ISharedContext> _contexts;

        public UnitOfWorkScopeProxy(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _contexts = new HashSet<ISharedContext>();
        }

        public IsolationLevel IsolationLevel { get; set; }

        public void RegisterContext(ISharedContext context)
        {
            _contexts.Add(context);
        }

        public T Get<T>()
        {
            return _serviceProvider.GetRequiredService<T>();
        }

        public void Commit()
        {
            foreach (var context in _contexts)
                context.Commit();
        }

        public void Rollback()
        {
            foreach (var context in _contexts)
                context.Rollback();
        }

        public void Dispose()
        {
            foreach (var context in _contexts)
                context.Dispose();
        }
    }
}
