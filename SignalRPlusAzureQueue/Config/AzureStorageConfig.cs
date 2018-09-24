using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using SignalRPlusAzureQueue.Interfaces;

namespace SignalRPlusAzureQueue.Config
{
    class AzureStorageConfig : IAzureStorageConfig
    {
        public  CloudStorageAccount GetAccount()
        {
            var account = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            return account ;
        }

        public string StorageItemReference()
        {
            return "myqueue";
        }
    }
}