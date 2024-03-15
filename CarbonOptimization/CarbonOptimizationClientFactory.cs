using Microsoft.Identity.Client;

namespace CarbonOptimization
{
    /// <summary>
    /// Factory class to create CarbonOptimizationClient
    /// </summary>
    public class CarbonOptimizationClientFactory
    {
        /// <summary>
        /// The authority for the Azure AD tenant
        /// </summary>
        private readonly string _authority = "https://login.microsoftonline.com/";
        /// <summary>
        /// The scopes for the CarbonOptimizationClient
        /// </summary>
        private readonly string[] _scopes = { "https://management.azure.com/.default" };

        /// <summary>
        /// Create a CarbonOptimizationClient
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<CarbonOptimizationClient> Create(string clientId, string clientSecret, string tenantId)
        {
            // Create a confidential client application 
            var confidentialClientApplication = ConfidentialClientApplicationBuilder.Create(clientId)
                .WithClientSecret(clientSecret)
                .WithAuthority(_authority + tenantId)
                .Build();
            // Acquire an access token for the client
            AuthenticationResult authenticationResult;
            authenticationResult = await confidentialClientApplication
                .AcquireTokenForClient(_scopes)
                .ExecuteAsync();
            // Use the access token to create a CarbonOptimizationClient
            string accessToken = authenticationResult.AccessToken;
            return new CarbonOptimizationClient(accessToken);
        }
    }
}
