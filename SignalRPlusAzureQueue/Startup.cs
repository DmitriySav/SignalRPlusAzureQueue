using Autofac;
using Autofac.Integration.SignalR;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Owin;
using Owin;
using SignalRPlusAzureQueue.DependencyConteiners;

[assembly: OwinStartup(typeof(SignalRPlusAzureQueue.Startup))]

namespace SignalRPlusAzureQueue
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = new AutofacContainer().Container;

            var resolver = new AutofacDependencyResolver(container);

            GlobalHost.DependencyResolver = new AutofacDependencyResolver(container);

            app.UseAutofacMiddleware(container);
            app.MapSignalR(new HubConfiguration
            {
                Resolver = resolver
            });

            AddSignalRInjection(container, resolver);
            app.MapSignalR();
        }

        private void AddSignalRInjection(IContainer container, IDependencyResolver resolver)
        {
            var updater = new ContainerBuilder();

            updater.RegisterInstance(resolver.Resolve<IConnectionManager>());
            updater.Update(container);
        }
    }
}
