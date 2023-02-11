using System;
using BusAndSignalRSample.Common.Const;
using BusAndSignalRSample.WebApi.Consumers;
using BusAndSignalRSample.WebApi.Hubs;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace BusAndSignalRSample.WebApi
{
    public static class ModuleBootstrapper
    {
        public static void Configue(IServiceCollection services)
        {
            services.AddSingleton<MessageRegistrationHub>();
            services.AddSingleton<IBusControl>(c =>
            {
                return Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(new Uri(CommonConst.Connections.RabbitMqUrl), h =>
                    {
                        h.Username(CommonConst.Connections.RabbitLogin);
                        h.Password(CommonConst.Connections.RabbitPassword);
                    });

                    cfg.ReceiveEndpoint(host, CommonConst.ExchangeAndQueues.MessageRegisteredEvent, e =>
                    {
                        e.Consumer<MessageRegisteredEventConsumer>();
                    });
                });
            });
        }
    }
}
