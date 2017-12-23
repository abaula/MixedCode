using System;
using RabbitMqExperiments.MessagingContract.Events;

namespace RabbitMqExperiments.ProviderApp.Model
{
    internal class UserSaid : IUserSaid
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
