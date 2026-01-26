using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

namespace DecoupledLogging
{
    class Program
    {
        private static void Main()
        {
            UseServiceProvider();
        }

        private static void UseServiceProvider()
        {
            var services = new ServiceCollection();
            // Регистрация ConditionalWeakTable<object, object>
            // AddSingleton используется только для демонстрации работоспособности,
            // в реальных приложениях рекомендую AddScoped.
            services.AddSingleton<ConditionalWeakTable<object, object>>();

            // Регистрируем все необходимые классы - сервис и его теневой логер.
            services.AddScopedWithLogger<IMyService, MyService, LoggerForMyService>();
            services.AddScopedWithLogger<IMySecondService, MySecondService, LoggerForMySecondService>();
            // Регистрация Lazy<IMyService> для проверки работы с Lazy<>.
            services.AddScoped(sp => new Lazy<IMyService>(() => sp.GetRequiredService<IMyService>()));

            // Создаём ServiceProvider.
            var serviceProvider = services.BuildServiceProvider();

            // Instance ConditionalWeakTable, который держит ссылки на теневые объекты логгеров.
            var conditionalWeekTable = serviceProvider.GetRequiredService<ConditionalWeakTable<object, object>>();
            // ScopeFactory для использования в методах.
            var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

            PrintConditionalWeakTable("CWT перед началом работы", conditionalWeekTable);

            DoServiceWork(scopeFactory);
            DoSecondServiceWork(scopeFactory);
            DoLazyServiceWork(scopeFactory);

            PrintConditionalWeakTable("CWT по окончании работы", conditionalWeekTable);

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            PrintConditionalWeakTable("CWT после GC", conditionalWeekTable);
        }

        private static void DoServiceWork(IServiceScopeFactory scopeFactory)
        {
            using var scope = scopeFactory.CreateScope();
            // instance MyService
            var service = scope.ServiceProvider.GetRequiredService<IMyService>();
            service.DoWork();

            var table = scope.ServiceProvider.GetRequiredService<ConditionalWeakTable<object, object>>();
            PrintConditionalWeakTable("CWT в методе DoServiceWork", table);
        }

        private static void DoSecondServiceWork(IServiceScopeFactory scopeFactory)
        {
            using var scope = scopeFactory.CreateScope();

            // instance MySecondService
            var service = scope.ServiceProvider.GetRequiredService<IMySecondService>();
            service.DoWork();

            var table = scope.ServiceProvider.GetRequiredService<ConditionalWeakTable<object, object>>();
            PrintConditionalWeakTable("CWT в методе DoSecondServiceWork", table);
        }

        private static void DoLazyServiceWork(IServiceScopeFactory scopeFactory)
        {
            using var scope = scopeFactory.CreateScope();
            // instance Lazy<IMyService>
            var lazyService = scope.ServiceProvider.GetRequiredService<Lazy<IMyService>>();
            lazyService.Value.DoWork();

            var table = scope.ServiceProvider.GetRequiredService<ConditionalWeakTable<object, object>>();
            PrintConditionalWeakTable("CWT в методе DoLazyServiceWork", table);
        }

        private static void PrintConditionalWeakTable(string title,
            ConditionalWeakTable<object, object> table)
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

    public static class ServiceCollectionExtensions
    {
        public static ServiceCollection AddScopedWithLogger<TService, TServiceInstance, TServiceLogger>(this ServiceCollection services)
            where TService : class
            where TServiceInstance : class, TService
            where TServiceLogger : class
        {
            // Регистрация TServiceInstance.
            services.AddScoped<TServiceInstance>();
            // Регистрация TServiceLogger.
            services.AddScoped<TServiceLogger>();
            // Регистрация TService.
            services.AddScoped<TService>(sp =>
            {
                var instance = sp.GetRequiredService<TServiceInstance>();
                var logger = sp.GetRequiredService<TServiceLogger>();
                var conditionalWeekTable = sp.GetRequiredService<ConditionalWeakTable<object, object>>();
                // Помещаем instance и logger в ConditionalWeakTable.
                conditionalWeekTable.Add(instance, logger);

                return instance;
            });

            return services;
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
            Console.WriteLine($"MyService.LogEvent(obj: {sender?.GetHashCode()}, message: {message})");
        }
    }

    public interface IMyService
    {
        event EventHandler<string>? LogEvent;
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

    public class LoggerForMySecondService
    {
        public LoggerForMySecondService(MySecondService serviceInstance)
        {
            // Ссылку на объект MySecondService не храним, иначе не будет правильно работать ConditionalWeakTable.
            serviceInstance.LogEvent += LogInformation;
        }

        private void LogInformation(object? sender, string message)
        {
            Console.WriteLine($"MySecondService.LogEvent(obj: {sender?.GetHashCode()}, message: {message})");
        }
    }

    public interface IMySecondService
    {
        event EventHandler<string>? LogEvent;
        void DoWork();
    }

    public class MySecondService : IMySecondService
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