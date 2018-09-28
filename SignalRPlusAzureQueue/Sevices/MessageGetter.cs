using System;
using System.Threading;
using Microsoft.AspNet.SignalR;
using SignalRPlusAzureQueue.Interfaces;

namespace SignalRPlusAzureQueue.Sevices
{
    public class MessageGetter:IMessageService
    {

        private IHubContext Context { get; set; }
        public Timer timer;
        private IQueueReader _reader;
        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(2000);


        public MessageGetter(IQueueReader queueReader, IHubContext context)
        {
            _reader = queueReader;
            Context = context;
            _reader.OnGetMessage += BroadcastSending;
        }

       
        public void Start()
        {
            if (timer != null)
            {
                return;
            }

            timer = new Timer(BeginGetMessages, null, _updateInterval, _updateInterval);
        }

        private void BeginGetMessages(object state)
        {
            if (_reader.Count() > 0)
            {
                _reader.GetMessage();
            }

        }

        private void BroadcastSending(string message)
        {
            Context.Clients.All.broadcastMessage(message);
        }

        
    }
}