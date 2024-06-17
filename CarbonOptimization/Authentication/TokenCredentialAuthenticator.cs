using Azure.Core;

namespace CarbonOptimization.Authentication;

internal class TokenCredentialAuthenticator : IAuthenticator
{
    private readonly TokenCredential _tokenCredential;
    private readonly CarbonOptimizationClientOptions _options;

    public TokenCredentialAuthenticator(TokenCredential tokenCredential, CarbonOptimizationClientOptions options)
    {
        _tokenCredential = tokenCredential;
        _options = options;

        if (string.IsNullOrWhiteSpace(options.TenantId))
        {
            throw new InvalidOperationException("TenantId cannot be empty");
        }

        if (options.Scopes.Length == 0)
        {
            throw new InvalidOperationException("Scopes cannot be empty");
        }
    }

    public async Task<string> GetAccessToken(CancellationToken cancellationToken = default)
    {
        if (_tokenCredential is null)
        {
            throw new InvalidOperationException("TokenCredential is not set");
        }
        var tokenRequestContext = new TokenRequestContext(_options.Scopes, _options.TenantId);
        var result = await _tokenCredential.GetTokenAsync(tokenRequestContext, cancellationToken);
        return result.Token;
    }
}
