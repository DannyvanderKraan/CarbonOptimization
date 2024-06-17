using System.Text.Json.Serialization;

namespace CarbonOptimization;

internal class ItemDetailsQueryFilter
{
    [JsonPropertyName("carbonScopeList")]
    public string[] CarbonScopeList { get; set; } = [];

    [JsonPropertyName("categoryType")]
    public string CategoryType { get; set; } = string.Empty;

    [JsonPropertyName("dateRange")]
    public DateRangeRequest? DateRange { get; set; }

    [JsonPropertyName("groupCategory")]
    public string GroupCategory { get; set; } = string.Empty;

    [JsonPropertyName("orderBy")]
    public string? OrderBy { get; set; }

    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }

    [JsonPropertyName("reportType")]
    public string ReportType { get; set; } = string.Empty;

    [JsonPropertyName("sortDirection")]
    public string SortDirection { get; set; } = "Asc";

    [JsonPropertyName("subscriptionList")]
    public string[] SubscriptionList { get; set; } = [];

    [JsonPropertyName("resourceGroupUrlList")]
    public string[] ResourceGroupUrlList { get; set; } = [];

    [JsonPropertyName("skipToken")]
    public string? SkipToken { get; set; }
}
