using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.SignalR;
using MessageConsumer;
using MessageConsumer.DependencyConteiners;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace MessageConsumer
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var container = new AutofacContainer().Container;
            
            var resolver = new AutofacDependencyResolver(container);
            
            app.UseAutofacMiddleware(container);

            //AddSignalRInjection(container, resolver);

            ConfigurationAuth(app, container);

            app.MapSignalR(new HubConfiguration
            {
                Resolver = resolver
            });

        }


        public void ConfigurationAuth(IAppBuilder app, IContainer container)
        {

           OAuthAuthorizationServerOptions oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = container.BeginLifetimeScope().Resolve<IOAuthAuthorizationServerProvider>() 
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());


        }

        //private void AddSignalRInjection(IContainer container, IDependencyResolver resolver)
        //{
        //    var updater = new ContainerBuilder();

        //    updater.RegisterInstance(resolver.Resolve<IConnectionManager>());
        //    updater.Update(container);
        //}
    }
}
