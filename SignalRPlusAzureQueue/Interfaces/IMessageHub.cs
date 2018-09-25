

namespace SignalRPlusAzureQueue.Interfaces
{
    public interface IMessageHub
    {
        void broadcastMessage(string str);

    }
}
