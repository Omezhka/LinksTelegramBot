using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using WebApplicationLinksTelegramBot.Database;

namespace WebApplicationLinksTelegramBot.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock
            ) : base(options, logger, encoder, clock)
        { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            Response.Headers.Add("WWW-Authenticate", "Basic");

            if (!Request.Headers.ContainsKey("Authorization")) 
                return Task.FromResult(AuthenticateResult.Fail("Authorization header missing."));

            // Get authorization key
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var authHeaderRegex = new Regex(@"Basic (.*)");

            if (!authHeaderRegex.IsMatch(authorizationHeader))
                return Task.FromResult(AuthenticateResult.Fail("Authorization code not formatted properly."));

            var authBase64 = Encoding.UTF8.GetString(Convert.FromBase64String(authHeaderRegex.Replace(authorizationHeader, "$1")));
            var authSplit = authBase64.Split(Convert.ToChar(":"), 2);
            var authUsername = authSplit[0];
            Console.WriteLine(authUsername);
            var authPassword = authSplit.Length > 1 ? authSplit[1] : throw new Exception("Unable to get password");
            Console.WriteLine(authPassword);
            AuthenticatedUser authenticatedUser;

            using (var context = new LinkstelegrambotContext())
            {
                if (context.Auths.Where(auth => auth.Login == authUsername && auth.Password == authPassword).Any())
                    authenticatedUser = new AuthenticatedUser("BasicAuthentication", true, context.Auths.FirstOrDefault(auth => auth.Login == authUsername).Login);
                 else
                    return Task.FromResult(AuthenticateResult.Fail("The username or password is not correct."));    
            }
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(authenticatedUser));

            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
        }
    }
}