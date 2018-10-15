using MiddlewareMessageLib.Converter;
using MiddlewareMessageLib.Enums;
using MiddlewareMessageLib.Interfaces;
using MiddlewareMessageLib.Messages;
using Newtonsoft.Json;

namespace MessageConsumer.Handlers
{
    public abstract class MessageEventHandlerBase
    {
        private readonly IAzureStorageProvider _azureStorage;
        protected MessageEventHandlerBase(IAzureStorageProvider azureStorage)
        {
            _azureStorage = azureStorage;
            EventRegister();
            
        }

        public delegate void EventMessageHandler<in T>(T obj);

        public virtual event EventMessageHandler<UserMessage> OnGetUserMessage;
        public virtual event EventMessageHandler<CoachMessage> OnGetCoachMessage;
        public virtual event EventMessageHandler<UmpireMessage> OnGetUmpireMessage;


        protected virtual void EventRegister()
        {
            _azureStorage.OnGetStringMessage += ParseEvent;
        }

        protected virtual void ParseEvent(object sender, string message)
        {

            var (messageType, messageBody) = MessageJsonSerializer.ParseMessageContext(message);
            if (messageType == MessageEnum.UserMessage)
            {
                var userMessage = JsonConvert.DeserializeObject<UserMessage>(messageBody);
                OnGetUserMessage?.Invoke(userMessage);
            }

            if (messageType == MessageEnum.UmpireMessage)
            {
                var umpireMessage = JsonConvert.DeserializeObject<UmpireMessage>(messageBody);
                OnGetUmpireMessage?.Invoke(umpireMessage);
            }

            if (messageType == MessageEnum.CoachMessage)
            {
                var coachMessage = JsonConvert.DeserializeObject<CoachMessage>(messageBody);
                OnGetCoachMessage?.Invoke(coachMessage);
            }


        }

    }
}