using System.Text.Json.Serialization;

namespace CarbonOptimization;

internal class DateRangeResponse
{
    [JsonPropertyName("startDate")]
    public string StartDate { get; set; }
    [JsonPropertyName("endDate")]
    public string EndDate { get; set; }
}
