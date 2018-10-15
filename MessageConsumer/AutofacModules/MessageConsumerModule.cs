using System;
using System.Linq;
using Autofac;
using MessageConsumer.Handlers;
using MessageConsumer.Providers;
using MessageConsumer.Services.Interfaces;
using MiddlewareMessageLib.Enums;
using MiddlewareMessageLib.Interfaces;
using MiddlewareMessageLib.Models;
using MiddlewareMessageLib.Providers;

namespace MessageConsumer.AutofacModules
{
    public class MessageConsumerModule:Module
    {
        private ConnectionString connectionString;
        
        public MessageConsumerModule(IConnectionStringRepo repo)
        {
            connectionString = repo.GetAll().SingleOrDefault(item => item.Environment == EnvironmentEnum.Development);
        }
        
        protected override void Load(ContainerBuilder container)
        {
            container.Register(c => new SimpleAuthorizationServerProvider(c.Resolve<IUserService>()))
                .AsImplementedInterfaces().SingleInstance();
            container.RegisterType<MessageEventHandler>()
                .As<MessageEventHandlerBase>()
                .SingleInstance();
            //container.Register(c=>c.Resolve<IConnectionManager>().GetHubContext<MessageHub>())
            //    .Named<IHubContext>("MessageHub");
            //    .SingleInstance()
            //    .WithParameter(new ResolvedParameter((pi, ctx) => pi.ParameterType == typeof(IHubContext),
            //        (pi, ctx) => ctx.ResolveNamed<IHubContext>("MessageHub")));
            try
            {
                container.RegisterType<AzureStorageQueueProvider>()
                    .As<IAzureStorageProvider>()
                    .WithParameter("connectionString", connectionString.Value)
                    .WithParameter("itemReference", connectionString.Name)
                    .SingleInstance();
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            base.Load(container);
        }
    }
}