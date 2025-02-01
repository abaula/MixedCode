using System;
using BusAndSignalRSample.MessagingContract.Commands;

namespace BusAndSignalRSample.WebApi.Model
{
    public class RegisterMessageCommand : IRegisterMessageCommand
    {
        public Guid MessageId { get; set; }
        public string Message { get; set; }
    }
}
