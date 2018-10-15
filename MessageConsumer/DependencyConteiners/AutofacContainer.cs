using System.Reflection;
using Autofac;
using Autofac.Integration.SignalR;
using MessageConsumer.AutofacModules;
using MiddlewareMessageLib.Entity;
using MiddlewareMessageLib.Repositories;
using MessageConsumer.Services.AutofacModules;
using MessageConsumer.Infrastructure.Data.AutofacModules;
using MessageConsumer.Utils.AutofacModules;

namespace MessageConsumer.DependencyConteiners
{
    public class AutofacContainer
    {
        public IContainer Container { get; }

        public AutofacContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterHubs(Assembly.GetExecutingAssembly())
                .PropertiesAutowired();

            builder.RegisterModule<ServiceModule>();

            builder.RegisterModule<InfrastructureDataModule>();

            builder.RegisterModule<UtilsModule>();
                      
            builder.RegisterModule(new MessageConsumerModule(new ConnectionStringRepo(new MiddlewareContext())));
           
            Container = builder.Build();
        }
    }
}