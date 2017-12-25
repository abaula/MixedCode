using System;
using System.Threading.Tasks;
using MassTransit;
using RabbitMqExperiments.MessagingContract;

namespace RabbitMqExperiments.LoggerApp
{
    public class UserSaidLoggerConsumer : IConsumer<IUserSaid>
    {
        public async Task Consume(ConsumeContext<IUserSaid> context)
        {
            var data = context.Message;
            await Console.Out.WriteLineAsync($"Log: \"{data.Message}\", at {data.CreatedAt}. Message id: {data.Id}");
        }
    }
}
