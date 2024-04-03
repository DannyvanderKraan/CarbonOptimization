using Newtonsoft.Json;

namespace CarbonOptimization
{
    internal class OverallSummaryReportQueryFilter
    {
            // Carbon emission scope for carbon emissions data
            [JsonProperty("carbonScopeList")]
            public string[] CarbonScopeList { get; set; }

            // The start date and end date for carbon emissions data
            [JsonProperty("dateRange")]
            public DateRangeRequest DateRange { get; set; }

            // Report type
            [JsonProperty("reportType")]
            public string ReportType { get; set; }

            // SubscriptionId list for carbon emissions data
            [JsonProperty("subscriptionList")]
            public string[] SubscriptionList { get; set; }

            //// resourceGroupUrl list for carbon emissions data
            //[JsonProperty("resourceGroupUrlList")]
            //public string[] ResourceGroupUrlList { get; set; }

    }
}
