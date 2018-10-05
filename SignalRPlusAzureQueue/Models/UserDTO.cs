using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRPlusAzureQueue.Models
{
    public class UserDTO
    {
        public string UserEmail { get; set; }
        public string Password { get; set; }

        public virtual List<Role> Roles { get; set; }

    }
}