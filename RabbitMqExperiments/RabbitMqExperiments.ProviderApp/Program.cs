using System;
using MassTransit;
using RabbitMqExperiments.Common.Const;
using RabbitMqExperiments.Common.Helpers;
using RabbitMqExperiments.MessagingContract.Events;
using RabbitMqExperiments.ProviderApp.Model;

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

            while (true)
            {
                var message = Console.ReadLine();

                if (StringHelper.Equals("exit", message))
                {
                    bus.Stop();
                    return;
                }

                bus.Publish<IUserSaid>(new UserSaid
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
