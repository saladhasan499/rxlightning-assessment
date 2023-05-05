using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public class HardcodedBearerAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public HardcodedBearerAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // Get the Authorization header value
        if (!Request.Headers.TryGetValue("Authorization", out var authorization))
        {
            return Task.FromResult(AuthenticateResult.Fail("Missing Authorization header"));
        }

        // Check if the header value starts with "Bearer "
        if (!authorization.ToString().StartsWith("Bearer "))
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization header format"));
        }

        // Extract the token string after "Bearer "
        string tokenString = authorization.ToString().Substring("Bearer ".Length).Trim();

        // Validate the token
        if (tokenString == "tokenString")
        {
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, "user@example.com") };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
        else
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid token"));
        }
    }

}
