using System.Threading.Tasks;
using BusAndSignalRSample.MessagingContract.Events;
using BusAndSignalRSample.WebApi.Hubs;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace BusAndSignalRSample.WebApi.Consumers
{
    internal class MessageRegisteredEventConsumer : IConsumer<IMessageRegisteredEvent>
    {
        public async Task Consume(ConsumeContext<IMessageRegisteredEvent> context)
        {
            var hub = Startup.ServiceProvider.GetRequiredService<MessageRegistrationHub>();
            await hub.OnMessageRegistered(context.Message);
        }
    }
}
