using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Azure;
using SignalRPlusAzureQueue.Config;
using SignalRPlusAzureQueue.Interfaces;
using SignalRPlusAzureQueue.Readers;
using SignalRPlusAzureQueue.Sevices;
using Timer = System.Threading.Timer;

namespace SignalRPlusAzureQueue.Hubs
{
    public class MessageHub : Hub
    {


        public void OnConnection()
        {

            var queueReader = QueueReader.GetInstane(new AzureQueueConfig());
            MessageGetter.GetInstance(queueReader, this);
        }

       
    }
}