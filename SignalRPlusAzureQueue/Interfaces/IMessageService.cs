using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using SignalRPlusAzureQueue.Sevices;

namespace SignalRPlusAzureQueue.Interfaces
{
    public interface IMessageService
    {
        void Start();

    }
}