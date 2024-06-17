using System.Text.Json.Serialization;

namespace CarbonOptimization;

internal class OverallSummaryReportQueryFilter
{
    // Carbon emission scope for carbon emissions data
    [JsonPropertyName("carbonScopeList")]
    public string[] CarbonScopeList { get; set; } = [];

    // The start date and end date for carbon emissions data
    [JsonPropertyName("dateRange")]
    public DateRangeRequest DateRange { get; set; } = new();

    // Report type
    [JsonPropertyName("reportType")]
    public string ReportType { get; set; } = string.Empty;

    // SubscriptionId list for carbon emissions data
    [JsonPropertyName("subscriptionList")]
    public string[] SubscriptionList { get; set; } = [];

    //// resourceGroupUrl list for carbon emissions data
    //[JsonPropertyName("resourceGroupUrlList")]
    //public string[] ResourceGroupUrlList { get; set; }
}
