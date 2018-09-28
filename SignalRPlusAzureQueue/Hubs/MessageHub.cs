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

        public void OnConnection()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                Clients.Caller.Message("authenticated");
            }
            else
            {
                Clients.Caller.Message("anonymous");
            }


        }

        [Authorize]
        public void OnAuthorize()
        {
            Clients.Caller.Message("Authorize");
        }

        public void Start()
        {
            // _messageService.Start();

        }


    }
}