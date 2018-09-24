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