using System;
using MassTransit;
using RabbitMqExperiments.Common.Const;

namespace RabbitMqExperiments.ConsumerApp
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

                cfg.ReceiveEndpoint(host, AppConst.RabbitUserTalkQueue, e =>
                {
                    e.Consumer<UserSaidConsumer>();
                });
            });

            bus.Start();

            Console.WriteLine("Hello from RabbitMqExperiments.ConsumerApp!");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            bus.Stop();
        }
    }
}
