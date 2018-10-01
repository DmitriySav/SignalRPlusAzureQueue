using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using SignalRPlusAzureQueue.Interfaces;

namespace SignalRPlusAzureQueue.Sevices
{
    /// <summary>
    /// Implement IAzureQueueStorageService service for control azure queue
    /// </summary>
    public class AzureQueueStorageService : IAzureQueueStorageService
    {
        private readonly IAzureStorageConfig _azureStorageConfig;
        private CloudQueue _queue;

        public AzureQueueStorageService(IAzureStorageConfig azureStorageConfig)
        {
            _azureStorageConfig = azureStorageConfig;

        }
        /// <summary>
        /// Connect to Azure Cloud Storage Account and get queue
        /// </summary>
        public void ConnectedToAccount()
        {
            CloudStorageAccount storageAccount = _azureStorageConfig.GetAccount();
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            _queue = queueClient.GetQueueReference(_azureStorageConfig.StorageItemReference());
        }

        /// <summary>
        /// Count number of items in queue
        /// </summary>
        /// <returns>return number of items</returns>
        ///  /// <exception cref="Microsoft.WindowsAzure.Storage.StorageException">Thrown when queue not connected and cannot 
        /// fetches the queue's attributes.</exception>
        public int QueueCount()
        {
            try
            {
                _queue.FetchAttributes();
            }
            catch (StorageException)
            {
                return 0;
            }
            
            var count = _queue.ApproximateMessageCount;
            return count.GetValueOrDefault();           
        }
        /// <summary>
        /// Get last message from queue
        /// </summary>
        /// <returns>string message</returns>
        public string GetMessage()
        {
            CloudQueueMessage queueMessage = _queue.GetMessage();
            _queue.DeleteMessage(queueMessage);
            return queueMessage.AsString;
        }

    }
}