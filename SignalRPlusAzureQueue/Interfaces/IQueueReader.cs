

namespace SignalRPlusAzureQueue.Interfaces
{
    public delegate void ReaderEventHandler(string message);
    public interface IQueueReader
    {
        /// <summary>
        /// Event on GetMessage method 
        /// </summary>
        event ReaderEventHandler OnGetMessage;
        /// <summary>
        /// Method get count of items in readable object
        /// </summary>
        /// <returns>Integer number count of items</returns>
        int Count();
        /// <summary>
        /// Get message from readable object
        /// </summary>
        void GetMessage();
    }
}

