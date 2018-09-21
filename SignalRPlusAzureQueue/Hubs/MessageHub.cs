using System;
using Microsoft.AspNet.SignalR;
using SignalRPlusAzureQueue.Interfaces;
using SignalRPlusAzureQueue.Readers;
using Timer = System.Threading.Timer;

namespace SignalRPlusAzureQueue.Hubs
{
    public class MessageGetter
    {
        private IQueueReader _reader;
        private Hub _context;
        public static MessageGetter Instance;
        public string CurrentMessage;
        private readonly object _GetMessageLock = new object();
        private Timer _timer;
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
            lock (_GetMessageLock)
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

    public class MessageHub : Hub
    {
        public void OnConnection()
        {
            var queueReader = QueueReader.GetInstane("myqueue");
            MessageGetter.GetInstance(queueReader, this);
        }
    }
}