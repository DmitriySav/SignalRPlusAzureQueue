using MiddlewareMessageLib.Converter;
using MiddlewareMessageLib.Enums;
using MiddlewareMessageLib.Interfaces;
using MiddlewareMessageLib.Messages;
using Newtonsoft.Json;

namespace MessageConsumer.Handlers
{
    /// <summary>
    /// Queue reader work with azure storage service
    /// </summary>
    public class MessageEventHandler : MessageEventHandlerBase
    {
        //public override event EventMessageHandler<UserMessage> OnGetUserMessage;
        //public override event EventMessageHandler<CoachMessage> OnGetCoachMessage;
        //public override event EventMessageHandler<UmpireMessage> OnGetUmpireMessage;

        public MessageEventHandler(IAzureStorageProvider azureStorage) : base(azureStorage)
        {
        }

        
    }
}