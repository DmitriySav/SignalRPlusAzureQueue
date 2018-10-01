using SignalRPlusAzureQueue.Interfaces;

namespace SignalRPlusAzureQueue.Readers
{
    /// <summary>
    /// Queue reader work with azure storage service
    /// </summary>
    public class QueueReader:IQueueReader
    {
        private IAzureQueueStorageService _azureQueueStorageService;


        public QueueReader(IAzureQueueStorageService azureQueueStorageService)
        {
            _azureQueueStorageService = azureQueueStorageService;
            _azureQueueStorageService.ConnectedToAccount();
            
        }

        /// <summary>
        /// Event OnGetMessage
        /// </summary>
        public event ReaderEventHandler OnGetMessage;

        event ReaderEventHandler IQueueReader.OnGetMessage
        {
            add { if (OnGetMessage == null) { OnGetMessage += value; } }
            remove { OnGetMessage -= value; }
        }

        /// <summary>
        /// Get number of items in queue
        /// </summary>
        /// <returns>int number</returns>
        public int Count()
        {
            return _azureQueueStorageService.QueueCount();
        }

        /// <summary>
        /// Get message from queue
        /// Invoke OnGetMessage event
        /// </summary>
        public void GetMessage()
        {
            var message = _azureQueueStorageService.GetMessage();
            OnGetMessage?.Invoke(message);
        }
        
    }
}