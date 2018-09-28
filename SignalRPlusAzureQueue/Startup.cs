using System;
using Autofac;
using Autofac.Integration.SignalR;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SignalRPlusAzureQueue.DependencyConteiners;
using SignalRPlusAzureQueue.Interfaces;
using SignalRPlusAzureQueue.Providers;
using SignalRPlusAzureQueue.Sevices;

[assembly: OwinStartup(typeof(SignalRPlusAzureQueue.Startup))]

namespace SignalRPlusAzureQueue
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = new AutofacContainer().Container;
            var resolver = new AutofacDependencyResolver(container);

            app.UseAutofacMiddleware(container);

            AddSignalRInjection(container, resolver);

            ConfigurationAuth(app);

            app.MapSignalR(new HubConfiguration
            {
                Resolver = resolver
            });
        }


        public void ConfigurationAuth(IAppBuilder app)
        {
            IUserService userService = new UserService();
            

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider(userService)
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());


        }

        private void AddSignalRInjection(IContainer container, IDependencyResolver resolver)
        {
            var updater = new ContainerBuilder();

            updater.RegisterInstance(resolver.Resolve<IConnectionManager>());
            updater.Update(container);
        }
    }
}
