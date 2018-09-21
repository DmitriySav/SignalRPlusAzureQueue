using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Queue;
using SignalRPlusAzureQueue.Interfaces;

namespace SignalRPlusAzureQueue.Readers
{
    public class QueueReader:IQueueReader
    {
        private IAzureStorageConfig _config;       
        private CloudQueue _queue;
        private static QueueReader _instance;


        private QueueReader(IAzureStorageConfig azureConfig)
        {
            _config = azureConfig;
            
        }

        public static QueueReader GetInstane(IAzureStorageConfig azureConfig)
        {
            if(_instance == null)
            _instance = new QueueReader(azureConfig);
            return _instance;
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
        public string GetMessage()
        {
            CloudQueueMessage queueMessage = _queue.GetMessage();
            _queue.DeleteMessage(queueMessage);
            return queueMessage.AsString;
        }
        
    }
}