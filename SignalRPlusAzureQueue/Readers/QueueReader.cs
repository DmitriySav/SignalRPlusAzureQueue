using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Queue;
using SignalRPlusAzureQueue.Interfaces;

namespace SignalRPlusAzureQueue.Readers
{
    public class QueueReader:IQueueReader
    {
        private IAzureQueueStorageService _azureQueueStorageService;


        public QueueReader(IAzureQueueStorageService azureQueueStorageService)
        {
            _azureQueueStorageService = azureQueueStorageService;
            _azureQueueStorageService.ConnectedToAccount();
            
        }

        public event ReaderEventHandler OnGetMessage;

        event ReaderEventHandler IQueueReader.OnGetMessage
        {
            add { if (OnGetMessage == null) { OnGetMessage += value; } }
            remove { OnGetMessage -= value; }
        }

        
        public int Count()
        {
            return _azureQueueStorageService.QueueCount();
        }

        public void GetMessage()
        {
            var message = _azureQueueStorageService.GetMessage();
            OnGetMessage?.Invoke(message);
        }
        
    }
}