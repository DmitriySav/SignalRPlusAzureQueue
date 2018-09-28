using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRPlusAzureQueue.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}