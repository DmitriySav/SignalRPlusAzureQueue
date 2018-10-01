using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using SignalRPlusAzureQueue.Interfaces;



namespace SignalRPlusAzureQueue.Hubs
{
    public class MessageHub : Hub
    {

        private readonly IMessageService _messageService;

        public MessageHub(IMessageService messageService)
        {
            _messageService = messageService;
        }
        /// <summary>
        /// Method invoke on signalr client connection and assign user to group "authenticated or anonymous"
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            AssignToSecurityGroup();

            return base.OnConnected();
        }

        private void AssignToSecurityGroup()
        {
            if (Context.User.Identity.IsAuthenticated)
                Groups.Add(Context.ConnectionId, "authenticated");
            else
                Groups.Add(Context.ConnectionId, "anonymous");
        }

        /// <summary>
        /// Method run timer in messageService, begin recieve messages
        /// </summary>
        /// <returns>bool value True if authenticated, False if not authenticated</returns>
        public bool Start()
        {
             _messageService.Start();

            return Context.User.Identity.IsAuthenticated;
        }


    }
}