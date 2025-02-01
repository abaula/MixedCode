using System;
using System.Data.SqlClient;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EFCoreRepositoryUnitOfWork.Contexts;
using EFCoreRepositoryUnitOfWork.Repositories;
using EFCoreRepositoryUnitOfWork.Works;
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
            Console.WriteLine();
            Console.WriteLine("Работа приложения завершена. Для выхода нажмите любую клавишу.");
            Console.ReadKey();
        }

        private static void Configure(ContainerBuilder builder)
        {
            builder.Register(c => new DbContextOptionsBuilder<SampleContext>()
                .UseSqlServer(new SqlConnection(Configuration["ConnectionString"]))
                .Options)
                .As<DbContextOptions<SampleContext>>();
            builder.RegisterType<UnitOfWorkFactory>().As<IUnitOfWorkFactory>();
            builder.RegisterType<UnitOfWorkScopeContextManager>().As<IUnitOfWorkScopeContextManager>();
            builder.RegisterType<Service>();

            // IUnitOfWorkScope обязательно регистрируем с опцией InstancePerLifetimeScope().
            // Делаем это, чтобы передавать один экземпляр IUnitOfWorkScope между объектами TWork выполняющими работу 
            // в рамках созданного scope.
            builder.RegisterType<UnitOfWorkScope>().As<IUnitOfWorkScope>().InstancePerLifetimeScope();

            // Объекты доступа к данным TRepository и рабочие объекты TWork 
            // не обязательно регистрировать с опцией InstancePerLifetimeScope().
            // Можно это сделать, чтобы оптимизировать время жизни объектов
            // и использовать один экземпляр несколько раз.
            builder.RegisterType<ReadOnlyRepository>().As<IReadOnlyRepository>();
            builder.RegisterType<EditRepository>().As<IEditRepository>();
            builder.RegisterType<FalseWork>().As<IFalseWork>();
            builder.RegisterType<Work>().As<IWork>();

            // Контекст обязательно регистрируем с опцией InstancePerLifetimeScope().
            // Делаем это, чтобы передавать один экземпляр DbContext между объектами 
            // доступа к данным TRepository, выполняющими работу в рамках созданного scope.
            builder.RegisterType<SampleContext>().InstancePerLifetimeScope();
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
            service.DoSomeWorkWithWorkers();
            var values = service.GetAllValues();

            foreach (var value in values)
            {
                Console.WriteLine(value);
            }

            service.DeleteAll();
        }
    }
}
