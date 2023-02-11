using System;
using BusAndSignalRSample.MessagingContract.Events;

namespace BusAndSignalRSample.WebApi.Model
{
    public class MessageRegisteringEvent : IMessageRegisteringEvent
    {
        public Guid MessageId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
