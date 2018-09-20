using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Queue;

namespace SignalRPlusAzureQueue.Readers
{
    public class QueueReader
    {
        private string _queueName;
        private CloudQueue _queue;
        public QueueReader(string queueName)
        {
            _queueName = queueName;
        }
        public void ConnectToQueue()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
               CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            _queue = queueClient.GetQueueReference(_queueName);
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