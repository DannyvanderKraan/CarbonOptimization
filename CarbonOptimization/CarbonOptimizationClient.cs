using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using static System.Formats.Asn1.AsnWriter;

namespace CarbonOptimization
{
    /// <summary>
    /// Client to interact with the Carbon Optimization API
    /// </summary>
    public class CarbonOptimizationClient
    {
        /// <summary>
        /// The authority for the Azure AD tenant
        /// </summary>
        private readonly string _authority = "https://login.microsoftonline.com/";
        /// <summary>
        /// The scopes for the CarbonOptimizationClient
        /// </summary>
        private readonly string[] _scopes = { "https://management.azure.com/.default" };

        private string _accessToken;
        private readonly string _carbonEmissionDataAvailableDateRangeUrl = "https://management.azure.com/providers/Microsoft.Carbon/queryCarbonEmissionDataAvailableDateRange?api-version=2023-04-01-preview";
        private readonly string _carbonEmissionReportsUrl = "https://management.azure.com/providers/Microsoft.Carbon/carbonEmissionReports?api-version=2023-04-01-preview";
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _tenantId;

        public CarbonOptimizationClient(IOptions<CarbonOptimizationClientOptions> options)
        {
            _clientId = options.Value.ClientId;
            _clientSecret = options.Value.ClientSecret;
            _tenantId = options.Value.TenantId;
        }

        /// <summary>
        /// Get the date range for which carbon emission data is available
        /// </summary>
        /// <returns></returns>
        public async Task<DateRange?> GetCarbonEmissionDataAvailableDateRange()
        {
            if (string.IsNullOrWhiteSpace(_accessToken)) await GetAccessToken();

            DateRange? dateRange = null;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

                HttpResponseMessage response = await client.PostAsync(_carbonEmissionDataAvailableDateRangeUrl, null);

                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseContent);

                var dateRangeResponse = JsonConvert.DeserializeObject<DateRangeResponse>(responseContent);
                if (dateRangeResponse != null) dateRange = DateRange.Create(dateRangeResponse);
            }
            return dateRange;
        }

        /// <summary>
        /// Get carbon emission reports for the given date range
        /// </summary>
        /// <param name="itemDetailsQuery">Item details report type query</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">An argument exception if the start and end date are not in the same month</exception>
        public async Task<CarbonEmissionDataListResult?> GetCarbonEmissionReports(ItemDetailsQuery itemDetailsQuery)
        {
            if (string.IsNullOrWhiteSpace(_accessToken)) await GetAccessToken();

            //Currently, the API only supports querying for data within the same month
            if (itemDetailsQuery.DateRange.StartDate.Month != itemDetailsQuery.DateRange.EndDate.Month)
            {
                throw new ArgumentException("Start date and end date should be in the same month");
            }

            CarbonEmissionDataListResult? carbonEmissionDataListResult;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

                ItemDetailsQueryFilter itemDetailsQueryFilter = ItemDetailsQueryFilter.Create(itemDetailsQuery);

                string json = JsonConvert.SerializeObject(itemDetailsQueryFilter);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(_carbonEmissionReportsUrl, content);

                string responseContent = await response.Content.ReadAsStringAsync();
                carbonEmissionDataListResult = JsonConvert.DeserializeObject<CarbonEmissionDataListResult>(responseContent);
            }
            return carbonEmissionDataListResult;
        }

        private async Task GetAccessToken()
        {
            // Create a confidential client application 
            var confidentialClientApplication = ConfidentialClientApplicationBuilder.Create(_clientId)
                .WithClientSecret(_clientSecret)
                .WithAuthority(_authority + _tenantId)
                .Build();
            // Acquire an access token for the client
            AuthenticationResult authenticationResult = await confidentialClientApplication
                .AcquireTokenForClient(_scopes)
                .ExecuteAsync();
            // Store the access token
            _accessToken = authenticationResult.AccessToken;
        }
    }
}
