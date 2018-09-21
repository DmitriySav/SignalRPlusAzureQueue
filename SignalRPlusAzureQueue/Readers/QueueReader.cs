using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Queue;
using SignalRPlusAzureQueue.Interfaces;

namespace SignalRPlusAzureQueue.Readers
{
    public class QueueReader:IQueueReader
    {
        private string _queueName;
        private CloudQueue _queue;
        private static QueueReader Instance;


        private QueueReader(string queueName)
        {
            _queueName = queueName;
        }

        public static QueueReader GetInstane(string queueName)
        {
            if(Instance == null)
            Instance = new QueueReader(queueName);
            return Instance;
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