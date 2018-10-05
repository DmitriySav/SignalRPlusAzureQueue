using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SignalRPlusAzureQueue.Sevices;
using SignalRPlusAzureQueue.Util;

namespace SignalRPlusAzureQueue.Models
{
    public class UserContext: DbContext
    {
        public static DbSet<UserModel> Users { get; set; }
        public static DbSet<Role> Roles { get; set; }

        public UserContext()
        {           
           
            
        }
    }
}