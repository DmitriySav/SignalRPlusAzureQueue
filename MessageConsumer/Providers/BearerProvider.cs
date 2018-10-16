using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security.OAuth;

namespace MessageConsumer.Providers
{
    public class BearerProvider:OAuthBearerAuthenticationProvider
    {
        public override Task RequestToken(OAuthRequestTokenContext context)
        {
           // base.RequestToken(context);
            var value = context.Request.Query.Get("Authorization");

            if (!string.IsNullOrEmpty(value))
                context.Token = value;            
            
            return Task.FromResult<object>(null);
        }
    }
}