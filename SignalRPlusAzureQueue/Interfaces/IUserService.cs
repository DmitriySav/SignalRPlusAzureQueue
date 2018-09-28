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
        /// <summary>
        /// Method check user's credentials
        /// </summary>
        /// <param name="userName"> User name string</param>
        /// <param name="password"> User's password</param>
        /// <returns>bool value, true if user is authenticated</returns>
        bool IsAuthenticate(string userName, string password);
    }
}
