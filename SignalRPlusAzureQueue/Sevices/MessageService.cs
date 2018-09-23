using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using SignalRPlusAzureQueue.Hubs;
using SignalRPlusAzureQueue.Interfaces;

namespace SignalRPlusAzureQueue.Sevices
{
    public class MessageService:IMessageService
    {
        private IQueueReader _reader;
        private readonly IHubContext _hubContext;
        private Timer _timer;

        public MessageService(IQueueReader queueReader, IHubContext hubContext)
        {
            _reader = queueReader;
            _reader.ConnectToQueue();
            _hubContext = hubContext;
            _reader.OnGetMessage += BroadcastSending;
        }
        public void Start()
        {
            if (_timer != null)
            {
                return;
            }

            _timer = new Timer(GetMessage, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));
        }

        private void GetMessage(object sender)
        {          
                if (_reader.QueueCount() > 0)
                {
                    _reader.GetMessage();
                }           
        }
    
        public void BroadcastSending(string message)
        {
            _hubContext.Clients.All.broadcastMessage(message);
        }
    }
}