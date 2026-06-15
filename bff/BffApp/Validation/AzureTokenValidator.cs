using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public static class AzureTokenValidator
{
    private static IConfigurationManager<OpenIdConnectConfiguration>? _configManager;

    private static IConfigurationManager<OpenIdConnectConfiguration> GetConfig(string authority)
    {
        _configManager ??= new ConfigurationManager<OpenIdConnectConfiguration>(
            $"{authority}/.well-known/openid-configuration",
            new OpenIdConnectConfigurationRetriever()
        );

        return _configManager;
    }

    // ID TOKEN VALIDATION (FOR CLIENT APPS)
    public static async Task<ClaimsPrincipal?> ValidateIdTokenAsync(
        string token,
        string tenantId,
        string clientId,
        CancellationToken cancellationToken =default)
    {
        var authority = $"https://login.microsoftonline.com/{tenantId}/v2.0";
        var configManager = GetConfig(authority);
        var config = await configManager.GetConfigurationAsync(cancellationToken);

        var handler = new JwtSecurityTokenHandler();

        var parameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = authority,

            ValidateAudience = true,
            ValidAudience = clientId, // ID token audience = client app

            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(2),

            ValidateIssuerSigningKey = true,
            IssuerSigningKeys = config.SigningKeys
        };

        var principal = handler.ValidateToken(token, parameters, out var validatedToken);

        // Ensure ID token type
        ValidateTokenType(validatedToken as JwtSecurityToken, expectedType: "JWT");

        return principal;
    }

    // Helpers
    private static void ValidateTokenType(JwtSecurityToken? jwt, string expectedType)
    {
        if (jwt == null)
            throw new SecurityTokenException("Invalid token format");

        if (jwt.Header.Typ != expectedType)
        {
            throw new SecurityTokenException(
                $"Invalid token type. Expected: {expectedType}, Actual: {jwt.Header.Typ}");
        }
    }
}