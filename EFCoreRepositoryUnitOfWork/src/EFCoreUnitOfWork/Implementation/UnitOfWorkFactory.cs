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
            // Каждый экземпляр IUnitOfWorkScope необходимо создавать в своём изолированном scope, 
            // это позволит обмениваться одними и теми-же экземплярами объектов между разными классами.
            // Время жизни объектов, которыми необходимо обмениваться, должно быть определено для scope.
            // Кроме этого, если создавать экземпляр IUnitOfWorkScope в глобальном scope,
            // то при повторном обращении к нему будет ошибка, т.к. IUnitOfWorkScope поддерживает IDisposable.
            return _serviceProvider
                .GetService<IServiceScopeFactory>()
                .CreateScope()
                .ServiceProvider
                .GetService<IUnitOfWorkScope>();
        }
    }
}
