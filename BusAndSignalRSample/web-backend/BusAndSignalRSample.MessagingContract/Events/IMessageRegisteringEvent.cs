using System;

namespace BusAndSignalRSample.MessagingContract.Events
{
    public interface IMessageRegisteringEvent
    {
        Guid MessageId { get; }
        string Message { get; }
        DateTime CreatedAt { get; }
    }
}
