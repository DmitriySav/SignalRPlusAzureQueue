using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security.OAuth;
using SignalRPlusAzureQueue.Interfaces;
using SignalRPlusAzureQueue.Models;
using SignalRPlusAzureQueue.Sevices;

namespace SignalRPlusAzureQueue.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {

        private IUserService _userService;

        public SimpleAuthorizationServerProvider(IUserService userService)
        {
            _userService = userService;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            if (!string.IsNullOrEmpty(context.UserName))
            {
                var IsAuth = _userService.IsAuthenticate(context.UserName, context.Password);
                if (IsAuth)
                {
                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim("Name", context.UserName));
                    identity.AddClaim(new Claim("Password", context.Password));
                   context.Validated(identity);
                }
                //context.Validated(IsAuth);
            }
        }
    }
}