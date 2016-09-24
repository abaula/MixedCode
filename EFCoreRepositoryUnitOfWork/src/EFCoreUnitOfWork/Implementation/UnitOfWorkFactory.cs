using System;
using EFCoreUnitOfWork.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EFCoreUnitOfWork.Implementation
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWorkFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IUnitOfWorkScope Create()
        {
            return _serviceProvider.GetService<IUnitOfWorkScope>();
        }
    }
}
