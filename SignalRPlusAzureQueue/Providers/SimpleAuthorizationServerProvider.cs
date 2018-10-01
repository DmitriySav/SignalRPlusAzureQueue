using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using SignalRPlusAzureQueue.Interfaces;


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
                var isAuth = _userService.IsAuthenticate(context.UserName, context.Password);
                if (isAuth)
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