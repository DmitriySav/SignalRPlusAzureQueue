using System;
using System.Threading;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using SignalRPlusAzureQueue.Interfaces;

namespace SignalRPlusAzureQueue.Sevices
{
    public class MessageGetter
    {

        private Hub _context;
        private Timer _timer;
        private IQueueReader _reader;
        public string CurrentMessage;
        public static MessageGetter Instance;
        private readonly object _getMessageLock = new object();
        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(2000);


        private MessageGetter(IQueueReader queueReader, Hub context)
        {
            _reader = queueReader;
            _context = context;
            _reader.ConnectToQueue();
            _timer = new Timer(GetMessage, null, _updateInterval, _updateInterval);
        }

        private void GetMessage(object state)
        {
            lock (_getMessageLock)
            {
                if (_reader.QueueCount() > 0)
                {
                    CurrentMessage = _reader.GetMessage();
                    _context.Clients.All.broadcastMessage(CurrentMessage);
                }
            }
        }

        public static MessageGetter GetInstance(IQueueReader queueReader, Hub context)
        {
            if (Instance == null)
                Instance = new MessageGetter(queueReader, context);
            return Instance;
        }
    }
}