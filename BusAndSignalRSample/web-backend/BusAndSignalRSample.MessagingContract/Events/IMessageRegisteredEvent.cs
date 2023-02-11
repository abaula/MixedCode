using System;

namespace BusAndSignalRSample.MessagingContract.Events
{
    public interface IMessageRegisteredEvent
    {
        Guid MessageId { get; }
        string Message { get; }
        DateTime RegisteredAt { get; }
    }
}
