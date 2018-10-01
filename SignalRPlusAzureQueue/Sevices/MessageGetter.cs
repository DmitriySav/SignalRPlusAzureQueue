using System;
using System.Threading;
using Microsoft.AspNet.SignalR;
using SignalRPlusAzureQueue.Interfaces;

namespace SignalRPlusAzureQueue.Sevices
{
    /// <summary>
    /// Message broadcasting sender
    /// </summary>
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

       /// <summary>
       /// Methed which start timer if its not exist
       /// </summary>
        public void Start()
        {
            if (timer != null)
            {
                return;
            }

            timer = new Timer(BeginGetMessages, null, _updateInterval, _updateInterval);
        }

        /// <summary>
        /// Method invoke by timer 
        /// </summary>
        /// <param name="state">An object containing information to be used by the callback method, or null.</param>
        private void BeginGetMessages(object state)
        {
            if (_reader.Count() > 0)
            {
                _reader.GetMessage();
            }

        }
        /// <summary>
        /// Method sending messages for users in group
        /// </summary>
        /// <param name="message"> Message from invoked event OnGetMessage</param>
        private void BroadcastSending(string message)
        {
            Context.Clients.Group("authenticated").broadcastMessage(message + " You are authenticated user");
            Context.Clients.Group("anonymous").broadcastMessage(message + "You are anonymous user");
        }

        
    }
}