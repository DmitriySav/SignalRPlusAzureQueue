using System.Reflection;
using Autofac;
using Autofac.Integration.SignalR;
using MessageConsumer.AutofacModules;
using MessageConsumer.Handlers;
using MessageConsumer.Providers;
using MessageConsumer.Services.Interfaces;
using MiddlewareMessageLib.Entity;
using MiddlewareMessageLib.Repositories;
using MessageConsumer.Infrastructure.Business.AutofacModules;
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

            builder.Register(c => new SimpleAuthorizationServerProvider(c.Resolve<IUserService>()))
                .AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<MessageEventHandler>()
                .SingleInstance();

            //builder.Register(ctx=>new AzureProviderModule(ctx.Resolve<ConnectionStringRepo>()));
            builder.RegisterModule(new AzureProviderModule(new ConnectionStringRepo(new MiddlewareContext("name=AppContext"))));
            //builder.RegisterModule<ConfigInjectionModule>();
            //builder.Register(c=>c.Resolve<IConnectionManager>().GetHubContext<MessageHub>())
            //    .Named<IHubContext>("MessageHub");

            
            //    .SingleInstance()
            //    .WithParameter(new ResolvedParameter((pi, ctx) => pi.ParameterType == typeof(IHubContext),
            //        (pi, ctx) => ctx.ResolveNamed<IHubContext>("MessageHub")));

            Container = builder.Build();
        }
    }
}