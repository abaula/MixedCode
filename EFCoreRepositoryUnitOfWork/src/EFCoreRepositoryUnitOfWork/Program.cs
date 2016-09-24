using System;
using System.Data.SqlClient;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EFCoreRepositoryUnitOfWork.Contexts;
using EFCoreUnitOfWork.Implementation;
using EFCoreUnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EFCoreRepositoryUnitOfWork
{
    public class Program
    {
        public static IConfigurationRoot Configuration { get; private set; }
        public static IServiceProvider Container { get; private set; }
        public static void Main(string[] args)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new ContainerBuilder();
            Configure(builder);
            builder.Populate(new ServiceCollection());
            var container = builder.Build();
            Container = container.Resolve<IServiceProvider>();
            Run();
            Console.ReadKey();
        }

        private static void Configure(ContainerBuilder builder)
        {
            var options = new DbContextOptionsBuilder<SampleContext>()
                .UseSqlServer(new SqlConnection(Configuration["ConnectionString"]))
                .Options;

            builder.RegisterInstance(options).As<DbContextOptions<SampleContext>>();
            builder.RegisterType<UnitOfWorkScope>().As<IUnitOfWorkScope>();
            builder.RegisterType<UnitOfWorkFactory>().As<IUnitOfWorkFactory>();
            builder.RegisterType<UnitOfWorkScopeContextManager>().As<IUnitOfWorkScopeContextManager>();
            builder.RegisterType<Service>();

            // Обязательно регистрируем с опцией InstancePerLifetimeScope().
            builder.RegisterType<SampleContext>().InstancePerLifetimeScope();
            builder.RegisterType<ReadOnlyRepository>().As<IReadOnlyRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EditRepository>().As<IEditRepository>().InstancePerLifetimeScope();
        }

        private static void Run()
        {
            var service = Container.GetService<Service>();
            // Делаем проверки.
            service.CheckDbContextInstancesAndThrow();
            // Делаем работу.
            service.DoSomeCommitedWork();
            service.DoSomeInterruptedWork();
            service.DoSomeNotCommitedWork();
            var values = service.GetAllValues();

            foreach (var value in values)
            {
                Console.WriteLine(value);
            }

            service.DeleteAll();
        }
    }
}
