using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;

namespace SignalRPlusAzureQueue.Providers
{
    public class BearerTokenProvider : OAuthBearerAuthenticationProvider
    {
        public BearerTokenProvider()
        {
  
        }
    }
}