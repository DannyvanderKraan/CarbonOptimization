using Microsoft.Identity.Client;

namespace CarbonOptimization.Authentication;

internal class ClientCredentialAuthenticator : IAuthenticator
{
    // Create a confidential client application 
    private readonly IConfidentialClientApplication _confidentialClientApplication;

    private readonly CarbonOptimizationClientOptions _options;

    public ClientCredentialAuthenticator(CarbonOptimizationClientOptions options)
    {
        _options = options;
        if (string.IsNullOrWhiteSpace(options.ClientId) || string.IsNullOrWhiteSpace(options.ClientSecret))
        {
            throw new InvalidOperationException("ClientId and ClientSecret cannot be empty when specifying client credentials");
        }

        if (string.IsNullOrWhiteSpace(options.TenantId))
        {
            throw new InvalidOperationException("TenantId cannot be empty");
        }

        if (options.Scopes.Length == 0)
        {
            throw new InvalidOperationException("Scopes cannot be empty");
        }

        _confidentialClientApplication = ConfidentialClientApplicationBuilder.Create(_options.ClientId)
                .WithClientSecret(_options.ClientSecret)
                .WithAuthority(string.Format(Constants.AuthorityFormat, _options.TenantId))
                .Build();
    }

    public async Task<string> GetAccessToken(CancellationToken cancellationToken = default)
    {
        var authenticationResult = await _confidentialClientApplication
            .AcquireTokenForClient(_options.Scopes)
            .ExecuteAsync(cancellationToken);

        return authenticationResult.AccessToken;
    }
}
