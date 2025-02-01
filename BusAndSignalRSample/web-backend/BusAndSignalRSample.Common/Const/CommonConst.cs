
namespace BusAndSignalRSample.Common.Const
{
    public static class CommonConst
    {
        public static class Connections
        {
            public const string RabbitMqUrl = "rabbitmq://localhost";
            public const string RabbitLogin = "guest";
            public const string RabbitPassword = "guest";
        }

        public static class ExchangeAndQueues
        {
            public const string RegisterMessageCommand = "busandsignalrsample_register_message_command";
            public const string MessageRegisteredEvent = "busandsignalrsample_message_registered_event";
        }
    }
}
