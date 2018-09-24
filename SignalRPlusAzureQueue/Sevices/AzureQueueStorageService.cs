using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using SignalRPlusAzureQueue.Interfaces;

namespace SignalRPlusAzureQueue.Sevices
{
    public class AzureQueueStorageService : IAzureQueueStorageService
    {
        private readonly IAzureStorageConfig _azureStorageConfig;
        private CloudQueue _queue;

        public AzureQueueStorageService(IAzureStorageConfig azureStorageConfig)
        {
            _azureStorageConfig = azureStorageConfig;

        }

        public void ConnectedToAccount()
        {
            CloudStorageAccount storageAccount = _azureStorageConfig.GetAccount();
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            _queue = queueClient.GetQueueReference(_azureStorageConfig.StorageItemReference());
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