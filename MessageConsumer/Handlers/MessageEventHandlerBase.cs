using MiddlewareMessageLib.Converter;
using MiddlewareMessageLib.Enums;
using MiddlewareMessageLib.Interfaces;
using MiddlewareMessageLib.Messages;
using Newtonsoft.Json;

namespace MessageConsumer.Handlers
{
    public abstract class MessageEventHandlerBase
    {

        protected MessageEventHandlerBase(IAzureStorageProvider azureStorage)
        {
            azureStorage.OnGetStringMessage += ParseEvent;
        }

        public delegate void EventMessageHandler<in T>(T obj);

        public virtual event EventMessageHandler<UserMessage> OnGetUserMessage;
        public virtual event EventMessageHandler<CoachMessage> OnGetCoachMessage;
        public virtual event EventMessageHandler<UmpireMessage> OnGetUmpireMessage;


        protected virtual void EventRegister()
        {

        }

        protected virtual void ParseEvent(object sender, string message)
        {

            var messageContext = MessageJsonSerializer.ParseMessageContext(message);
            if (messageContext.messageType == MessageEnum.UserMessage)
            {
                var userMessage = JsonConvert.DeserializeObject<UserMessage>(messageContext.messageBody);
                OnGetUserMessage?.Invoke(userMessage);
            }

            if (messageContext.messageType == MessageEnum.UmpireMessage)
            {
                var umpireMessage = JsonConvert.DeserializeObject<UmpireMessage>(messageContext.messageBody);
                OnGetUmpireMessage?.Invoke(umpireMessage);
            }

            if (messageContext.messageType == MessageEnum.CoachMessage)
            {
                var coachMessage = JsonConvert.DeserializeObject<CoachMessage>(messageContext.messageBody);
                OnGetCoachMessage?.Invoke(coachMessage);
            }


        }

    }
}