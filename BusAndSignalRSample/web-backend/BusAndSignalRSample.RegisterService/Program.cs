using System;
using BusAndSignalRSample.Common.Const;
using MassTransit;

namespace BusAndSignalRSample.RegisterService
{
    class Program
    {
        static void Main()
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(CommonConst.Connections.RabbitMqUrl), h =>
                {
                    h.Username(CommonConst.Connections.RabbitLogin);
                    h.Password(CommonConst.Connections.RabbitPassword);
                });

                cfg.ReceiveEndpoint(host, CommonConst.ExchangeAndQueues.RegisterMessageCommand, e =>
                {
                    e.Consumer<RegisterMessageCommandConsumer>();
                });
            });

            bus.Start();

            Console.WriteLine($"Hello from {nameof(RegisterService)}");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            bus.Stop();
        }
    }
}
