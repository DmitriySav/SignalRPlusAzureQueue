using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignalRPlusAzureQueue.Models;

namespace SignalRPlusAzureQueue.Interfaces
{
    public interface IUserService
    {
        bool IsAuthenticate(string userName, string password);
    }
}
