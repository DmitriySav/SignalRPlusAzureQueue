using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Queue;
using SignalRPlusAzureQueue.Interfaces;

namespace SignalRPlusAzureQueue.Readers
{
    public class QueueReader:IQueueReader
    {
        private IAzureStorageConfig _config;       
        private CloudQueue _queue;
        public  event ReaderEventHandler OnGetMessage;

        event ReaderEventHandler IQueueReader.OnGetMessage
        {
            add { if (OnGetMessage == null) { OnGetMessage += value; } }
            remove { OnGetMessage -= value; }
        }

        private QueueReader(IAzureStorageConfig azureConfig)
        {
            _config = azureConfig;            
        }

        public void ConnectToQueue()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_config.GetconnectionString());
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            _queue = queueClient.GetQueueReference(_config.StorageItemReference());
        }
        public int QueueCount()
        {
            _queue.FetchAttributes();
            var count = _queue.ApproximateMessageCount;
            return count.GetValueOrDefault();
        }
        public void GetMessage()
        {
            CloudQueueMessage queueMessage = _queue.GetMessage();
            _queue.DeleteMessage(queueMessage);
            OnGetMessage?.Invoke(queueMessage.AsString);
        }
        
    }
}