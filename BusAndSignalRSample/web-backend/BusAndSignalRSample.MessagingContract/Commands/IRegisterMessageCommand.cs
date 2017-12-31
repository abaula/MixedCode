
using System;

namespace BusAndSignalRSample.MessagingContract.Commands
{
    public interface IRegisterMessageCommand
    {
        Guid MessageId { get; }
        string Message { get; }
    }
}
