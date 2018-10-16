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
        private readonly string _name;
        public BearerProvider(string name)
        {
            _name = name;
        }

        public override Task RequestToken(OAuthRequestTokenContext context)
        {
           // base.RequestToken(context);
            var value = context.Request.Query.Get(_name);

            if (!string.IsNullOrEmpty(value))
                context.Token = value;            
            
            return Task.FromResult<object>(null);
        }
    }
}