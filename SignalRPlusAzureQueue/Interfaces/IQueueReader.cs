

namespace SignalRPlusAzureQueue.Interfaces
{
    public delegate void ReaderEventHandler(string message);
    public interface IQueueReader
    {
        event ReaderEventHandler OnGetMessage;
        int Count();
        void GetMessage();
    }
}

