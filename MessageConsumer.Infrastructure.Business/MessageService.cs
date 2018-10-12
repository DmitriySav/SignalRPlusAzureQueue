using System;
using System.Threading;
using MessageConsumer.Services.Interfaces;
using MiddlewareMessageLib.Interfaces;

namespace MessageConsumer.Infrastructure.Business
{
    /// <summary>
    /// Message broadcasting sender
    /// </summary>
    public class MessageGetter:IMessageService
    {
        private IAzureStorageProvider _azureQueueStorage;
       // private IHubContext Context { get; set; }
        private Timer _timer;
        
        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(4000);


        public MessageGetter(IAzureStorageProvider azureStorageProvider)
        {           
            _azureQueueStorage = azureStorageProvider;
        }

       /// <summary>
       /// Methed which start timer if its not exist
       /// </summary>
        public void Start()
        {
            if (_timer != null)
            {
                return;
            }

            _timer = new Timer(BeginGetMessages, null, _updateInterval, _updateInterval);
        }

        /// <summary>
        /// Method invoke by timer 
        /// </summary>
        /// <param name="state">An object containing information to be used by the callback method, or null.</param>
        private void BeginGetMessages(object state)
        {
            _azureQueueStorage.GetItem();
        }
       


    }
}