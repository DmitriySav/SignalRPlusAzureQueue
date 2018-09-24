using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRPlusAzureQueue.Interfaces
{
     public interface IAzureQueueStorageService
    {
        void ConnectedToAccount();
        int QueueCount();
        string GetMessage();
    }
}