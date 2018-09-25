

namespace SignalRPlusAzureQueue.Interfaces
{
     public interface IAzureQueueStorageService
    {
        void ConnectedToAccount();
        int QueueCount();
        string GetMessage();
    }
}