using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace CarbonOptimization
{
    /// <summary>
    /// Client to interact with the Carbon Optimization API
    /// </summary>
    public class CarbonOptimizationClient
    {
        private readonly string _accessToken;
        private readonly string _carbonEmissionDataAvailableDateRangeUrl = "https://management.azure.com/providers/Microsoft.Carbon/queryCarbonEmissionDataAvailableDateRange?api-version=2023-04-01-preview";
        private readonly string _carbonEmissionReportsUrl = "https://management.azure.com/providers/Microsoft.Carbon/carbonEmissionReports?api-version=2023-04-01-preview";

        public CarbonOptimizationClient(string accessToken)
        {
            _accessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
        }

        /// <summary>
        /// Get the date range for which carbon emission data is available
        /// </summary>
        /// <returns></returns>
        public async Task<DateRangeResponse?> GetCarbonEmissionDataAvailableDateRange()
        {
            DateRangeResponse? dateRangeResponse;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

                HttpResponseMessage response = await client.PostAsync(_carbonEmissionDataAvailableDateRangeUrl, null);

                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseContent);

                dateRangeResponse = JsonConvert.DeserializeObject<DateRangeResponse>(responseContent);
            }
            return dateRangeResponse;
        }

        /// <summary>
        /// Get carbon emission reports for the given date range
        /// </summary>
        /// <param name="itemDetailsQuery">Item details report type query</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">An argument exception if the start and end date are not in the same month</exception>
        public async Task<CarbonEmissionDataListResult?> GetCarbonEmissionReports(ItemDetailsQuery itemDetailsQuery)
        {
            //Currently, the API only supports querying for data within the same month
            if(itemDetailsQuery.DateRange.StartDate.Month != itemDetailsQuery.DateRange.EndDate.Month)
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

 
    }
}
