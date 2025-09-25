using System.Text.Json.Serialization;

namespace CarbonOptimization;

internal class DateRangeResponse
{
    [JsonPropertyName("startDate")]
    public string StartDate { get; set; } = string.Empty;

    [JsonPropertyName("endDate")]
    public string EndDate { get; set; } = string.Empty;
}
