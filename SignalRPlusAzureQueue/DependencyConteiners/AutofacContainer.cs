using Autofac;
using Autofac.Integration.SignalR;
using System.Reflection;
using Microsoft.AspNet.SignalR;
using SignalRPlusAzureQueue.Config;
using SignalRPlusAzureQueue.Hubs;
using SignalRPlusAzureQueue.Interfaces;
using SignalRPlusAzureQueue.Readers;
using SignalRPlusAzureQueue.Sevices;

namespace SignalRPlusAzureQueue.DependencyConteiners
{
    public class AutofacContainer
    {
        public IContainer Container { get; set; }

        public AutofacContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterHubs(Assembly.GetExecutingAssembly())
                .PropertiesAutowired();

            builder.RegisterType<AzureStorageConfig>()
                .As<IAzureStorageConfig>();

            builder.RegisterType<QueueReader>()
                .As<IQueueReader>();

            builder.Register(context => GlobalHost.ConnectionManager.GetHubContext<MessageHub>())
                .As<IHubContext>();

            builder.RegisterType<MessageGetter>()
                .As<IMessageService>().SingleInstance();


            Container = builder.Build();
        }
    }
}