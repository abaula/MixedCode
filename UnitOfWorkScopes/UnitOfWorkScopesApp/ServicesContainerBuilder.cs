using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using UnitOfWorkScopes.Dal.Abstractions.Commands;
using UnitOfWorkScopes.Dal.Abstractions.Contexts;
using UnitOfWorkScopes.Dal.Abstractions.Queries;
using UnitOfWorkScopes.Dal.Implementation.Commands;
using UnitOfWorkScopes.Dal.Implementation.Contexts;
using UnitOfWorkScopes.Dal.Implementation.Queries;
using UnitOfWorkScopes.Domain.Abstractions.Works;
using UnitOfWorkScopes.Domain.Implementation.Works;
using UnitOfWorkScopes.Services.Abstractions;
using UnitOfWorkScopes.Services.Implementation;
using UnitOfWorkScopes.UnitOfWork.Abstractions.Scopes;
using UnitOfWorkScopes.UnitOfWork.Implementation;

namespace UnitOfWorkScopesApp
{
    public static class ServicesContainerBuilder
    {
        public static ContainerBuilder Get(IConfigurationRoot configuration)
        {
            var builder = new ContainerBuilder();

            // NLog
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddNLog();
            loggerFactory.ConfigureNLog("nlog.config");
            builder.RegisterInstance(loggerFactory).As<ILoggerFactory>();
            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>)).SingleInstance();

            // Unit of Work
            builder.RegisterType<UnitOfWorkScopeFactory>().As<IUnitOfWorkScopeFactory>();
            builder.RegisterType<UnitOfWorkScopeProxy>().As<IUnitOfWorkScopeProxy>()
                // Реализация IUnitOfWorkScopeProxy хранит зарегестированные Context-ы - для управления транзакциями.
                // Поэтому!!! Обязательно регистрируем с опцией InstancePerLifetimeScope(),
                // чтобы передовать один и тот-же экземпляр между потребителями в рамках одного Scope.
                .InstancePerLifetimeScope();

            // Context
            builder.RegisterType<OrderStorageContext>().As<IOrderStorageContext>()
                .WithParameter(new TypedParameter(typeof(string), configuration["ConnectionStrings:OrderStorage"]))
                // Обязательно регистрируем с опцией InstancePerLifetimeScope(),
                // чтобы передовать один и тот-же экземпляр между потребителями в рамках одного Scope.
                .InstancePerLifetimeScope();

            // DAL CQRS
            builder.RegisterType<GetOrderGoodsInfoAsyncQueryStub>().As<IGetOrderGoodsInfoAsyncQuery>();
            //builder.RegisterType<GetOrderGoodsInfoAsyncQuery>().As<IGetOrderGoodsInfoAsyncQuery>();
            builder.RegisterType<GetOrderAmountAsyncQueryStub>().As<IGetOrderAmountAsyncQuery>();
            builder.RegisterType<ReserveGoodsCmdStub>().As<IReserveGoodsCmd>();            
            builder.RegisterType<ApproveOrderCmdStub>().As<IApproveOrderCmd>();

            // Workers
            builder.RegisterType<GetOrderAmountAsyncWork>().As<IGetOrderAmountAsyncWork>();
            builder.RegisterType<GetOrderGoodsAsyncWork>().As<IGetOrderGoodsAsyncWork>();
            builder.RegisterType<ReserveOrderGoodsAsyncWork>().As<IReserveOrderGoodsAsyncWork>();

            // Services
            builder.RegisterType<OrdersService>().As<IOrdersService>();

            return builder;
        }
    }
}
