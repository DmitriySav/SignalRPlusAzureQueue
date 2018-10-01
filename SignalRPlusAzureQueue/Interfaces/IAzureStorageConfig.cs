using Microsoft.WindowsAzure.Storage;

namespace SignalRPlusAzureQueue.Interfaces
{
    public interface IAzureStorageConfig
    {
        /// <summary>
        /// Get account of azure cloud storage
        /// </summary>
        /// <returns>CloudStorageAccount object </returns>
        CloudStorageAccount GetAccount();

        /// <summary>
        /// Return storage container name
        /// </summary>
        /// <returns>string value</returns>
        string StorageItemReference();

    }
}
