using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SignalRPlusAzureQueue.Startup))]

namespace SignalRPlusAzureQueue
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
