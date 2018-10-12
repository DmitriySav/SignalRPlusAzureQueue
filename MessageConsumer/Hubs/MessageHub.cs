using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MessageConsumer.Handlers;
using MessageConsumer.Services.Interfaces;
using Microsoft.AspNet.SignalR;
using MessageConsumer.Entities;
using MessageConsumer.Infrastructure.Business;
using MiddlewareMessageLib.Messages;

namespace MessageConsumer.Hubs
{
    public class MessageHub : Hub
    {
        private MessageEventHandler _eventHandler;
        private IMessageService _messageService;
        private readonly IHubGroupManager<string> _groupManager;
        private static bool _eventSign;


        public MessageHub(IMessageService messageService,
                        MessageEventHandler eventHandler,
                        IHubGroupManager<string> groupManager)
        {
            _messageService = messageService;
            _eventHandler = eventHandler;
            _groupManager = groupManager;
        }
        /// <summary>
        /// Method invoke on signalr client connection and assign user to group "authenticated or anonymous"
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            Clients.Caller.broadcastMessage("hello");
            AssignToSecurityGroup();
            if (!_eventSign)
            {
                AssignToMessageEvents();
                _eventSign = true;
            }

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            RemoveFromSecurityGroup();

            return base.OnDisconnected(stopCalled);
        }


        private void AssignToMessageEvents()
        {
            _eventHandler.OnGetUserMessage += UserSender;
            _eventHandler.OnGetCoachMessage += CoachSender;
            _eventHandler.OnGetUmpireMessage += UmpireSender;
        }

        private void AssignToSecurityGroup()
        {

            if (Context.User.Identity.IsAuthenticated)
            {
                if (Context.User.Identity is ClaimsIdentity identity)
                {
                    var roles = identity.Claims.Where(c => c.Type == ClaimTypes.Role).ToList();
                    foreach (var role in roles)
                    {
                        var userId = identity.Claims
                            .SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                        _groupManager.AddToGroup(role.Value, userId, Context.ConnectionId);
                    }
                }
            }

            else
                _groupManager.AddToGroup("Anonymous", "User", Context.ConnectionId);
        }


        private void RemoveFromSecurityGroup()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                if (Context.User.Identity is ClaimsIdentity identity)
                {
                    var roles = identity.Claims.Where(c => c.Type == ClaimTypes.Role).ToList();
                    foreach (var role in roles)
                    {
                        var userId = identity.Claims
                            .SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                        _groupManager.RemoveFromGroup(userId, Context.ConnectionId);
                    }
                }
            }
            else
            {
                _groupManager.RemoveFromGroup("User", Context.ConnectionId);
            }
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

        private void UserSender(UserMessage userMessage)
        {
            var users = _groupManager.GetUsersFromGroup("User");
            foreach (var user in users)
            {
                Clients.Client(user.ConnectionIds[0]).broadcastMessage($"UserId - {userMessage.FacilityId}\n"+
                                                                     $"User name - {userMessage.FacilityName}\n"+
                                                                     $"User action - {userMessage.Action}");

            }
        }
        private void CoachSender(CoachMessage coachMessage)
        {
            var coaches = _groupManager.GetUsersFromGroup("Coach");
            foreach (var coach in coaches)
            {
                Clients.Client(coach.ConnectionIds[0])
                    .broadcastMessage($"Coach MessageId - {coachMessage.UserId}\n"+
                                      $"Coach Activity - {coachMessage.UserActivity}\n"+
                                      $"Coach message created at - {coachMessage.CreatedAtUtc}");

            }
        }
        private void UmpireSender(UmpireMessage umpireMessage)
        {
            var umpires = _groupManager.GetUsersFromGroup("Umpire");
            foreach (var umpire in umpires)
            {
                Clients.Clients(umpire.ConnectionIds)
                    .broadcastMessage($"Umpire name -{umpireMessage.FacilityName}\n "+
                                        $"Umpire activity - {umpireMessage.Activity}\n"+
                                        $"Umpire event date - {umpireMessage.CreatedAtUtc}");
            }
        }

    }
}