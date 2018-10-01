using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using SignalRPlusAzureQueue.Interfaces;

namespace SignalRPlusAzureQueue.Config
{
    class AzureStorageConfig : IAzureStorageConfig
    {
        /// <summary>
        /// Get azure account
        /// </summary>
        /// <returns>return Cloud storage account</returns>
        public  CloudStorageAccount GetAccount()
        {
            var account = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            return account ;
        }
        /// <summary>
        /// get storage container name
        /// </summary>
        /// <returns>return string, container name</returns>
        public string StorageItemReference()
        {
            return "myqueue";
        }
    }
}