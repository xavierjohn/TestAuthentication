namespace TestAuthentication;

using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public class TestAuthenticationHandler(IOptionsMonitor<TestAuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder)
    : AuthenticationHandler<TestAuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // Create authenticated user
        ClaimsIdentity claimsIdentity = new(Options.Claims, "Test Auth claims identity");
        List<ClaimsIdentity> identities = [claimsIdentity];
        AuthenticationTicket ticket = new(new ClaimsPrincipal(identities), TestAuthenticationSchemeOptions.DefaultScheme);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
