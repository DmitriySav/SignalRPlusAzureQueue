using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessageConsumer.Entities
{
    public class HubUser
    {
            public string UserId { get; }
            public List<string> ConnectionIds { get; }

            public HubUser(string userId, string connectionId)
            {
                UserId = userId;
                ConnectionIds = new List<string> { connectionId };

            }
        
    }
}
