namespace SignalRPlusAzureQueue.Interfaces
{
    public interface IAzureStorageConfig
    {
        string GetconnectionString();
        string StorageItemReference();

    }
}
