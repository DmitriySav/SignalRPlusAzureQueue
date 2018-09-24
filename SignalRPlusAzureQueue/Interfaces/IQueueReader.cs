using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRPlusAzureQueue.Interfaces
{
    public delegate void ReaderEventHandler(string message);
    public interface IQueueReader
    {
        event ReaderEventHandler OnGetMessage;
        void ConnectToQueue();
        int QueueCount();
        void GetMessage();
    }
}

