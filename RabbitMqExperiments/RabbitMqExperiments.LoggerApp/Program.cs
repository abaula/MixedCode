using System;
using MassTransit;
using RabbitMqExperiments.Common.Const;

namespace RabbitMqExperiments.LoggerApp
{
    class Program
    {
        static void Main()
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(AppConst.RabbitMqUrl), h =>
                {
                    h.Username(AppConst.RabbitLogin);
                    h.Password(AppConst.RabbitPassword);
                });

                cfg.ReceiveEndpoint(host, AppConst.RabbitUserTalkLogQueue, e =>
                {
                    e.Consumer<UserSaidLoggerConsumer>();
                });
            });

            bus.Start();

            Console.WriteLine("Hello from RabbitMqExperiments.LoggerApp!");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            bus.Stop();
        }
    }
}
