using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac.Integration.SignalR;
using Microsoft.AspNet.SignalR;
using SignalRPlusAzureQueue.DependencyConteiners;

namespace SignalRPlusAzureQueue
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //var container = new AutofacContainer().Container;
            //GlobalHost.DependencyResolver = new AutofacDependencyResolver(container);


        }
    }
}
