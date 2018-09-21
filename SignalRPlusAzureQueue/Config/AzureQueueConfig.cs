using Microsoft.Azure;
using SignalRPlusAzureQueue.Interfaces;

namespace SignalRPlusAzureQueue.Config
{
    class AzureQueueConfig : IAzureStorageConfig
    {
        public string GetconnectionString()
        {
            return CloudConfigurationManager.GetSetting("StorageConnectionString");
        }

        public string StorageItemReference()
        {
            return "myqueue";
        }
    }
}