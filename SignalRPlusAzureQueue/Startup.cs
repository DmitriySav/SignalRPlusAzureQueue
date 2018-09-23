using Microsoft.Owin;
using SignalRPlusAzureQueue.Config;
using SignalRPlusAzureQueue.DependencyResolvers;
using SignalRPlusAzureQueue.Interfaces;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Ninject;
using Ninject.Modules;
using Owin;
using SignalRPlusAzureQueue.Hubs;

[assembly: OwinStartup(typeof(SignalRPlusAzureQueue.Startup))]

namespace SignalRPlusAzureQueue
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            NinjectModule ninjectModule = new NinjectRegistrations();
            var kernel = new StandardKernel(ninjectModule);
            var resolver = new NinjectSignalRDependencyResolver(kernel);
            kernel.Bind<IHubContext>().ToConstant(resolver.Resolve<IConnectionManager>().GetHubContext<MessageHub>())
                .WhenInjectedInto<IMessageService>();


            app.MapSignalR(new HubConfiguration
            {
                Resolver = resolver
            });
        }
    }
}
