

namespace SignalRPlusAzureQueue.Interfaces
{
     public interface IAzureQueueStorageService
    {

        /// <summary>
        /// Connect to Azure Cloud Storage Account and get queue
        /// </summary>
        void ConnectedToAccount();
        /// <summary>
        /// Count number of items in queue
        /// </summary>
        /// <returns>return number of items</returns>
        int QueueCount();
        /// <summary>
        /// Get last message from queue
        /// </summary>
        /// <returns>string message</returns>
        string GetMessage();
    }
}