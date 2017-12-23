using System;
using System.Threading.Tasks;
using MassTransit;
using RabbitMqExperiments.MessagingContract.Events;

namespace RabbitMqExperiments.ConsumerApp
{
    public class UserSaidConsumer : IConsumer<IUserSaid>
    {
        public async Task Consume(ConsumeContext<IUserSaid> context)
        {
            var data = context.Message;
            await Console.Out.WriteLineAsync($"The user said: \"{data.Message}\", at {data.CreatedAt}. Message id: {data.Id}");
        }
    }
}
