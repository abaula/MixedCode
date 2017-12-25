using System;

namespace RabbitMqExperiments.MessagingContract
{
    public interface IUserSaid
    {
        Guid Id { get; }
        string Message { get; }
        DateTime CreatedAt { get; }
    }
}
