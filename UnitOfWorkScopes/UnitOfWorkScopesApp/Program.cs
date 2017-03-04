using System;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnitOfWorkScopes.Services.Abstractions;
using Autofac;

namespace UnitOfWorkScopesApp
{
    public class Program
    {
        public static IConfigurationRoot Configuration { get; private set; }
        public static IServiceProvider ServiceProvider { get; private set; }

        public static void Main(string[] args)
        {
            BuildConfiguration();
            BuildServiceProvider();
            DoWork();
        }

        private static void DoWork()
        {
            var ordersService = ServiceProvider.GetRequiredService<IOrdersService>();
            var orderId = Guid.NewGuid();
            var amount = ordersService.GetOrderAmountAsync(orderId)
                .GetAwaiter()
                .GetResult();

            ordersService.ApproveOrderAsync(orderId)
                .GetAwaiter()
                .GetResult();

            ordersService.DeleteOrderAsync(orderId)
                .GetAwaiter()
                .GetResult();
        }

        private static void BuildServiceProvider()
        {
            var builder = ServicesContainerBuilder.Get(Configuration);
            builder.Populate(new ServiceCollection());
            var container = builder.Build();
            ServiceProvider = container.Resolve<IServiceProvider>();
        }

        private static void BuildConfiguration()
        {
            Configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", false, true)
               //.AddEnvironmentVariables()
               .Build();
        }
    }
}
