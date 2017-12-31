using System;
using System.Threading;
using System.Threading.Tasks;
using BusAndSignalRSample.MessagingContract.Commands;
using BusAndSignalRSample.MessagingContract.Events;
using MassTransit;

namespace BusAndSignalRSample.RegisterService
{
    internal class RegisterMessageCommandConsumer : IConsumer<IRegisterMessageCommand>
    {
        public async Task Consume(ConsumeContext<IRegisterMessageCommand> context)
        {
            var command = context.Message;

            await Console.Out.WriteLineAsync($"The message is \"{command.Message}\". Message id: {command.MessageId}");

            Thread.Sleep(2000);

            await context.Publish<IMessageRegisteredEvent>(new
            {
                command.MessageId,
                command.Message,
                RegisteredAt = DateTime.Now
            });
        }
    }
}
