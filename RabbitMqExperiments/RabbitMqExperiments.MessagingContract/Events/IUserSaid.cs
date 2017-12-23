using System;

namespace RabbitMqExperiments.MessagingContract.Events
{
    public interface IUserSaid
    {
        Guid Id { get; }
        string Message { get; }
        DateTime CreatedAt { get; }
    }
}
