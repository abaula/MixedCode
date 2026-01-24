using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

namespace DecoupledLogging
{
    class Program
    {
        private static void Main()
        {
            //UseWrapper();
            UseServiceProvider();
        }

        private static void UseWrapper()
        {
            var service = new MyService();
            var serviceWithLogger = new MyServiceWrapperWithLogger(service);
            serviceWithLogger.Instance.DoWork();
        }

        private static void UseServiceProvider()
        {
            var services = new ServiceCollection();
            // Регистрация ConditionalWeakTable<MyService, LoggerForMyService>
            // AddSingleton используется только для демонстрации работоспособности,
            // в реальных приложениях рекомендую AddScoped.
            services.AddSingleton<ConditionalWeakTable<MyService, LoggerForMyService>>();
            // Регистрация MyService.
            services.AddScoped<MyService>();
            // Регистрация IMyService.
            services.AddScoped<IMyService>(sp =>
            {
                var instance = sp.GetRequiredService<MyService>();
                var logger = new LoggerForMyService(instance);
                var conditionalWeekTable = sp.GetRequiredService<ConditionalWeakTable<MyService, LoggerForMyService>>();
                conditionalWeekTable.Add(instance, logger);

                return instance;
            });
            // Регистрация Lazy<MyService>.
            services.AddScoped(sp => new Lazy<IMyService>(() => sp.GetRequiredService<IMyService>()));
            var serviceProvider = services.BuildServiceProvider();
            // instance, который держит ссылки на теневые объекты логгеров.
            var conditionalWeekTable = serviceProvider.GetRequiredService<ConditionalWeakTable<MyService, LoggerForMyService>>();
            var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

            PrintConditionalWeakTable("Перед началом работы", conditionalWeekTable);

            DoServiceWork(scopeFactory);
            DoLazyServiceWork(scopeFactory);

            PrintConditionalWeakTable("По окнчании работы", conditionalWeekTable);

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            PrintConditionalWeakTable("После GC", conditionalWeekTable);
        }

        private static void DoServiceWork(IServiceScopeFactory scopeFactory)
        {
            // instance MyService
            using var scope = scopeFactory.CreateScope();

            var service = scope.ServiceProvider.GetRequiredService<IMyService>();
            service.DoWork();

            var table = scope.ServiceProvider.GetRequiredService<ConditionalWeakTable<MyService, LoggerForMyService>>();
            PrintConditionalWeakTable("DoServiceWork", table);
        }

        private static void DoLazyServiceWork(IServiceScopeFactory scopeFactory)
        {
            using var scope = scopeFactory.CreateScope();
            var lazyService = scope.ServiceProvider.GetRequiredService<Lazy<IMyService>>();
            lazyService.Value.DoWork();

            var table = scope.ServiceProvider.GetRequiredService<ConditionalWeakTable<MyService, LoggerForMyService>>();
            PrintConditionalWeakTable("DoLazyServiceWork", table);
        }

        private static void PrintConditionalWeakTable(string title,
            ConditionalWeakTable<MyService, LoggerForMyService> table)
        {
            Console.WriteLine(title);
            var values = new List<string>();

            foreach(var (key, value) in table)
                values.Add($"Row(key: {key.GetHashCode()}, value: {value.GetHashCode()})");

            Console.WriteLine($"Кол-во записей: {values.Count}");
            foreach(var value in values)
                Console.WriteLine(value);
        }
    }

    public class LoggerForMyService
    {
        public LoggerForMyService(MyService serviceInstance)
        {
            // Ссылку на объект MyService не храним, иначе не будет правильно работать ConditionalWeakTable.
            serviceInstance.LogEvent += LogInformation;
        }

        private void LogInformation(object? sender, string message)
        {
            Console.WriteLine($"LogEvent(obj: {sender?.GetHashCode()}, message: {message})");
        }
    }

    public class MyServiceWrapperWithLogger
    {
        public readonly MyService Instance;
        public MyServiceWrapperWithLogger(MyService serviceInstance)
        {
            Instance = serviceInstance;
            Instance.LogEvent += LogInformation;
        }

        private void LogInformation(object? sender, string message)
        {
            Console.WriteLine(message);
        }
    }

    public interface IMyService
    {
        void DoWork();
    }

    public class MyService : IMyService
    {
        public event EventHandler<string>? LogEvent;

        public void DoWork()
        {
            LogEvent?.Invoke(this, "Работа начата");
            // Логика...
            LogEvent?.Invoke(this, "Ошибка произошла");
        }
    }
}