namespace CarbonOptimization.Authentication;

internal interface IAuthenticator
{
    Task<string> GetAccessToken(CancellationToken cancellationToken = default);
}
