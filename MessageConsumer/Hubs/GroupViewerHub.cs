using System.Threading.Tasks;
using MessageConsumer.Services.Interfaces;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace MessageConsumer.Hubs
{
    public class GroupViewerHub : Hub
    {
        private readonly IHubGroupManager<string> _groupManager;
        private static bool _eventAssign;

        public GroupViewerHub(IHubGroupManager<string> HubManager)
        {
            _groupManager = HubManager;
        }

        public override Task OnConnected()
        {
            AssignEvent();
            return base.OnConnected();
        }


        private void AssignEvent()
        {
            if (!_eventAssign)
            {
                _groupManager.OnGroupChange += _groupManager_OnGroupChange;
                _eventAssign = true;
            }
        }

        private void _groupManager_OnGroupChange(object sender, Entities.Events.ManagerEventArgs<string, Entities.HubUser> e)
        {
            GetGroups();
            GetUsers();
        }

        public void GetGroups()
        {
            var groups = _groupManager.GetAllGroups();
            Clients.Caller.getGroups(JsonConvert.SerializeObject(groups));
        }

        public void GetUsers()
        {
            var users = _groupManager.GetAllUsers();
            Clients.Caller.getUsers(JsonConvert.SerializeObject(users));
        }

        public void DeleteUser(string userId)
        {
            _groupManager.RemoveFromGroup(userId);
            GetGroups();
            GetUsers();
        }
        
        public void GetUsers(string group)
        {
            var users = _groupManager.GetUsersFromGroup(group);
            Clients.Caller.getUsers(JsonConvert.SerializeObject(users));
        }
    }
}