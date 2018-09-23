using System;
using Ninject.Modules;
using SignalRPlusAzureQueue.Sevices;
using SignalRPlusAzureQueue.Readers;
using SignalRPlusAzureQueue.Interfaces;
using System.Web.Mvc;


namespace SignalRPlusAzureQueue.Config
{
    public class NinjectRegistrations: NinjectModule
    {
        public override void Load()
        {
           Bind<IAzureStorageConfig>().To<AzureQueueConfig>();
           Bind<IQueueReader>().To<QueueReader>();
           Bind<IMessageService>().To<MessageService>().InSingletonScope();
        }
    }
}