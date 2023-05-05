using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

public class HardcodedTokenAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private const string AUTHORIZATION_HEADER = "Authorization";
    private const string BEARER_PREFIX = "Bearer ";
    private const string AUTH_TOKEN = "tokenString"; // replace with your hardcoded token

    public HardcodedTokenAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(AUTHORIZATION_HEADER, out var authorizationHeader))
        {
            return AuthenticateResult.Fail("Missing authorization header");
        }

        var authHeaderValue = authorizationHeader.ToString();
        if (!authHeaderValue.StartsWith(BEARER_PREFIX, StringComparison.OrdinalIgnoreCase))
        {
            return AuthenticateResult.Fail("Invalid authorization scheme");
        }

        var token = authHeaderValue.Substring(BEARER_PREFIX.Length);
        if (!token.Equals(AUTH_TOKEN))
        {
            return AuthenticateResult.Fail("Invalid token");
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "hardcodeduser")
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}
