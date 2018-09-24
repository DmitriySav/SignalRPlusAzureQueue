using System;
using System.Threading;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using SignalRPlusAzureQueue.Interfaces;

namespace SignalRPlusAzureQueue.Sevices
{
    public class MessageGetter:IMessageService
    {

        public IHubContext _context { get; set; }
        public Timer _timer;
        private IQueueReader _reader;
        //public static MessageGetter Instance;
        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(2000);


        public MessageGetter(IQueueReader queueReader, IHubContext context)
        {
            _reader = queueReader;
            _context = context;
            _reader.ConnectToQueue();
            _reader.OnGetMessage += BroadcastSending;
        }

       
        public void Start()
        {
            if (_timer != null)
            {
                return;
            }

            _timer = new Timer(GetMessage, null, _updateInterval, _updateInterval);
        }

        private void GetMessage(object state)
        {
            if (_reader.QueueCount() > 0)
            {
                _reader.GetMessage();
            }

        }

        private void BroadcastSending(string message)
        {
            _context.Clients.All.broadcastMessage(message);
        }

        
    }
}