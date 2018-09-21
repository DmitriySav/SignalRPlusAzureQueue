using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRPlusAzureQueue.Interfaces
{
    public interface IReaderFactory
    {
        IQueueReader CreateQueueReader(string name);
    }
}
