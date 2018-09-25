using Microsoft.AspNet.SignalR;
using SignalRPlusAzureQueue.Interfaces;



namespace SignalRPlusAzureQueue.Hubs
{
    public class MessageHub : Hub
    {

        private readonly IMessageService _messageService;

        public MessageHub(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public void OnConnection()
        {
            Clients.All.broadcastMessage("connected");
        }

        public void Start()
        {
            _messageService.Start();

        }


    }
}