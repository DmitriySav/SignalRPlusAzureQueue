using Autofac;
using Autofac.Integration.SignalR;
using System.Reflection;
using Autofac.Core;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
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

            builder.RegisterType<AzureQueueStorageService>()
                .As<IAzureQueueStorageService>();

            builder.Register(c=>c.Resolve<IConnectionManager>().GetHubContext<MessageHub>())
                .Named<IHubContext>("MessageHub");

            builder.RegisterType<MessageGetter>()
                .As<IMessageService>()
                .SingleInstance()
                .WithParameter(new ResolvedParameter((pi, ctx) => pi.ParameterType == typeof(IHubContext),
                    (pi, ctx) => ctx.ResolveNamed<IHubContext>("MessageHub")));

            Container = builder.Build();
        }
    }
}