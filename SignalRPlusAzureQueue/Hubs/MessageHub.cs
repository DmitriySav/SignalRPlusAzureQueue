using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Azure;
using SignalRPlusAzureQueue.Config;
using SignalRPlusAzureQueue.Interfaces;
using SignalRPlusAzureQueue.Readers;
using SignalRPlusAzureQueue.Sevices;


namespace SignalRPlusAzureQueue.Hubs
{
    public class MessageHub : Hub
    {

        private readonly IMessageService _messageService;

        public MessageHub(IMessageService messageService)
        {
            if (_messageService == null)
                { throw new ArgumentNullException("messageService"); }
            _messageService = messageService;
        }

        public void Sendtwo()
        {
            Clients.All.broadcastMessage("hello");
        }

        public void Send()
        {
            _messageService.Start();
        }



    }
}