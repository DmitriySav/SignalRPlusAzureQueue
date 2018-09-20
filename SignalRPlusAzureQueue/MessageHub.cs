using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using SignalRPlusAzureQueue.Readers;

namespace SignalRPlusAzureQueue
{
    public class MessageHub : Hub
    {
        public void Send()
        {
            QueueReader queueReader = new QueueReader("myqueue");
            queueReader.ConnectToQueue();
            Task.Factory.StartNew(() => {
                while (true)
                {
                    if (queueReader.QueueCount() > 0)
                    {
                        var message = queueReader.GetMessage();
                        Clients.All.broadcastMessage(message);
                        Task.Delay(50000);
                    }
                }
            });
            
        }
    }
}