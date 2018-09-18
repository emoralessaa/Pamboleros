using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Pamboleros.Api.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Pamboleros.Api.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "El nombre de usuario y/o contraseña son incorrectos.");
                return;
            }

            if (!user.EmailConfirmed)
            {
                context.SetError("invalid_grant", "El usuario no ha confirmado su correo.");
                return;
            }

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, "JWT");

            var ticket = new AuthenticationTicket(oAuthIdentity, null);

            context.Validated(ticket);

        }
    }
}