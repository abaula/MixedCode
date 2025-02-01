using System;
using System.Threading.Tasks;
using BusAndSignalRSample.Common.Const;
using BusAndSignalRSample.MessagingContract.Commands;
using BusAndSignalRSample.MessagingContract.Events;
using BusAndSignalRSample.WebApi.Model;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace BusAndSignalRSample.WebApi.Hubs
{
    internal class MessageRegistrationHub : Hub
    {
        private readonly IBusControl _bus;

        public MessageRegistrationHub(IBusControl bus)
        {
            _bus = bus;
        }

        public async Task RegisterMessage(RegisterMessageCommand command)
        {
            var baseUri = new Uri(CommonConst.Connections.RabbitMqUrl);
            var endpointUri = new Uri(baseUri, CommonConst.ExchangeAndQueues.RegisterMessageCommand);
            var endpoint = await _bus.GetSendEndpoint(endpointUri);

            await endpoint.Send<IRegisterMessageCommand>(new
            {
                command.MessageId,
                command.Message
            });

            await OnMessageRegistering(new MessageRegisteringEvent
            {
                MessageId = command.MessageId,
                Message = command.Message,
                CreatedAt = DateTime.Now
            });
        }

        public async Task OnMessageRegistered(IMessageRegisteredEvent data)
        {
            await Clients.All.InvokeAsync("messageRegistered", data);
        }

        public async Task OnMessageRegistering(IMessageRegisteringEvent data)
        {
            await Clients.All.InvokeAsync("messageRegistering", data);
        }
    }
}
