using System;
using System.Collections.Generic;
using System.Linq;
using EFCoreUnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EFCoreUnitOfWork.Implementation
{
    public class UnitOfWorkScopeContextManager : IUnitOfWorkScopeContextManager
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<Type, DbContext> _contexts; 

        public UnitOfWorkScopeContextManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _contexts = new Dictionary<Type, DbContext>();
        }

        public void Dispose()
        {
            foreach(var context in _contexts.Values)
                context.Dispose();
        }

        public void RegisterDbContext(Type repositoryType)
        {
            if (_contexts.ContainsKey(repositoryType))
                return;

            var constructors = repositoryType.GetConstructors();

            if(constructors.Length != 1)
                throw new NotSupportedException("Указанный тип репозитория не поддерживается. Поддерживаются репозитории только с одним конструктором.");

            var parameters = constructors.Single().GetParameters()
                .Select(p => p.ParameterType)
                .Where(t => t.GetTypeInfo().IsSubclassOf(typeof(DbContext)))
                .ToArray();

            if (parameters.Length != 1)
                throw new NotSupportedException("Указанный тип репозитория не поддерживается. Поддерживаются репозитории только с одним контекстом.");

            var contextType = parameters.Single();
            var contextInstance = (DbContext)_serviceProvider.GetService(contextType);
            _contexts.Add(repositoryType, contextInstance);
        }

        public void DoCommit()
        {
            foreach (var context in _contexts.Values)
                context.SaveChanges();
        }
    }
}
