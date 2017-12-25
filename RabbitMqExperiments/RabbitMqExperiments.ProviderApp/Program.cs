using System;
using MassTransit;
using RabbitMqExperiments.Common.Const;
using RabbitMqExperiments.Common.Helpers;
using RabbitMqExperiments.MessagingContract;

namespace RabbitMqExperiments.ProviderApp
{
    class Program
    {
        static void Main()
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(AppConst.RabbitMqUrl), h =>
                {
                    h.Username(AppConst.RabbitLogin);
                    h.Password(AppConst.RabbitPassword);
                });
            });

            bus.Start();

            Console.WriteLine("Hello from RabbitMqExperiments.ProviderApp!");
            Console.WriteLine("Start writing lines or type 'exit' to stop me!");
            Console.WriteLine("To publish the message just enter it.");
            Console.WriteLine("To send a message use prefix -s.");

            while (true)
            {
                var message = Console.ReadLine();

                if (StringHelper.Equals("exit", message))
                {
                    bus.Stop();
                    return;
                }

                if (StringHelper.NotNullOrEmpty(message) && StringHelper.StartsWith(message, "-s "))
                {
                    var baseUri = new Uri(AppConst.RabbitMqUrl);
                    var endpointUri = new Uri(baseUri, AppConst.RabbitUserTalkQueue);

                    var endpoint = bus.GetSendEndpoint(endpointUri)
                        .GetAwaiter()
                        .GetResult();

                    endpoint.Send<IUserSaid>(new
                        {
                            Id = Guid.NewGuid(),
                            CreatedAt = DateTime.Now,
                            // ReSharper disable once PossibleNullReferenceException
                            Message = message.Substring(3)
                        })
                        .GetAwaiter()
                        .GetResult();

                    continue;
                }

                bus.Publish<IUserSaid>(new
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    Message = message
                })
                .GetAwaiter()
                .GetResult();
            }
        }
    }
}
