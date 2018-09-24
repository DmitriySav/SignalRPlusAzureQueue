using Microsoft.WindowsAzure.Storage;

namespace SignalRPlusAzureQueue.Interfaces
{
    public interface IAzureStorageConfig
    {
        CloudStorageAccount GetAccount();
        string StorageItemReference();

    }
}
