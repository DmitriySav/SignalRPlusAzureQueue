using System.Linq;
using Autofac;
using MiddlewareMessageLib.Enums;
using MiddlewareMessageLib.Interfaces;
using MiddlewareMessageLib.Models;
using MiddlewareMessageLib.Providers;

namespace MessageConsumer.AutofacModules
{
    public class AzureProviderModule:Module
    {
        private ConnectionString connectionString;
        
        public AzureProviderModule(IConnectionStringRepo repo)
        {
            connectionString = repo.GetAll().SingleOrDefault(item => item.Environment == EnvironmentEnum.Development);
        }
        
        protected override void Load(ContainerBuilder container)
        {
            container.RegisterType<AzureStorageQueueProvider>()
                .As<IAzureStorageProvider>()
                .WithParameter("connectionString", connectionString.Value)
                .WithParameter("itemReference", connectionString.Name)
                .SingleInstance();
            base.Load(container);
        }
    }
}