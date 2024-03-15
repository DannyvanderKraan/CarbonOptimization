using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarbonOptimization
{
    public class ItemDetailsQueryFilter
    {
        /// <summary>
        /// This array defines the scopes of carbon emissions to be included in the query. 
        /// Carbon emission scopes typically have Scope 1 (direct emissions from owned or controlled sources), 
        /// Scope 2 (indirect emissions from the generation of purchased energy), 
        /// and Scope 3 (all other indirect emissions that occur in a company’s value chain). 
        /// Developers need to specify which of these scopes they want to include in the emissions data retrieval, 
        /// ensuring the data reflects the specific environmental impact areas relevant to their analysis.
        /// </summary>
        [JsonProperty("carbonScopeList")]
        public string[] CarbonScopeList { get; set; }

        /// <summary>
        /// This property indicates the type of item categories to consider in the emissions data report. 
        /// The category type could range from resource types, service types, locations, etc. 
        /// It helps categorize the emissions data based on the specified dimension, providing a clearer view 
        /// of which areas contribute to the carbon footprint, thus allowing for targeted sustainability efforts.
        /// </summary>
        [JsonProperty("categoryType")]
        public string CategoryType { get; set; }

        /// <summary>
        /// Specifies the start and end dates for the period over which carbon emissions data should be retrieved. 
        /// This allows developers to define a specific time frame for analysis, enabling the tracking of emissions over time, 
        /// identifying trends, and evaluating the effectiveness of carbon reduction strategies.
        /// </summary>
        [JsonProperty("dateRange")]
        public DateRangeRequest DateRange { get; set; }

        //Currently not used
        //[JsonProperty("groupCategory")]
        //public string GroupCategory { get; set; }

        /// <summary>
        /// Determines the column name by which the returned items should be ordered. 
        /// This could refer to any relevant data field, such as total emissions, service name, etc. 
        /// Calling the results makes the data more readable and helps quickly identify the most significant contributors to 
        /// the carbon footprint.
        /// </summary>
        [JsonProperty("orderBy")]
        public string OrderBy { get; set; }

        /// <summary>
        /// Dictates the number of items to return in one response. This allows developers to manage the volume of data 
        /// produced by the API, facilitating easier data handling and analysis, especially when dealing with large sets 
        /// of emissions data.
        /// </summary>
        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        /// <summary>
        /// Specifies the type of report to generate. In this case, the report type is set to ItemDetailReport 
        /// (note: not ItemDetailsReport), which indicates that the query aims to retrieve detailed carbon emissions data. 
        /// This report type is crucial for deep-diving into specific items or categories to understand their 
        /// carbon emission levels.
        /// </summary>
        [JsonProperty("reportType")]
        public string ReportType { get; set; }

        /// <summary>
        /// This optional array specifies the resource group URLs for retrieving emissions data. 
        /// Including specific resource groups can further narrow the data retrieval to pertinent areas, 
        /// enabling a more focused analysis of particular segments of an Azure subscription.
        /// </summary>
        [JsonProperty("resourceGroupUrlList")]
        public string[] ResourceGroupUrlList { get; set; }

        /// <summary>
        /// Specifies the direction of sorting for the query results, such as ascending or descending. This affects the order 
        /// in which items are presented in the report, aiding in data interpretation by highlighting the highest or lowest 
        /// emitters based on the chosen sort criterion.
        /// </summary>
        [JsonProperty("sortDirection")]
        public string SortDirection { get; set; }

        /// <summary>
        /// An array of subscription IDs for which carbon emissions data should be retrieved. 
        /// This allows filtering the data to include only specific Azure subscriptions, 
        /// making the analysis relevant to the selected cloud resources and services.
        /// </summary>
        [JsonProperty("subscriptionList")]
        public string[] SubscriptionList { get; set; }

        /// <summary>
        /// This is used for pagination; it specifies the number of result items to skip. 
        /// This is particularly useful when dealing with large datasets, as it allows developers to navigate 
        /// through the results incrementally, retrieving manageable chunks of data at a time.
        /// </summary>
        [JsonProperty("skipToken")]
        public string SkipToken { get; set; }

        public static ItemDetailsQueryFilter Create(ItemDetailsQuery itemDetailsQuery)
        {
            var dateRangeRequest = new DateRangeRequest
            {
                StartDate = itemDetailsQuery.DateRange.StartDate.ToString("yyyy-MM-dd"),
                EndDate = itemDetailsQuery.DateRange.EndDate.ToString("yyyy-MM-dd")
            };

            return new ItemDetailsQueryFilter
            {
                CarbonScopeList = itemDetailsQuery.CarbonScopeList.Select(scope => scope.ToString()).ToArray(),
                CategoryType = itemDetailsQuery.CategoryType.ToString(),
                DateRange = dateRangeRequest,
                OrderBy = itemDetailsQuery.OrderBy.ToString(),
                PageSize = 10,
                ReportType = itemDetailsQuery.ReportType.ToString(),
                ResourceGroupUrlList = itemDetailsQuery.ResourceGroupUrlList ?? Array.Empty<string>(),
                SortDirection = itemDetailsQuery.SortDirection.ToString(),
                SubscriptionList = itemDetailsQuery.SubscriptionList,
                SkipToken = itemDetailsQuery.SkipToken
            };
        }
    }
}
