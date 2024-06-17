using System.Text.Json.Serialization;

namespace CarbonOptimization
{
    internal class DateRangeRequest
    {
        [JsonPropertyName("start")]
        public string StartDate { get; set; }
        [JsonPropertyName("end")]
        public string EndDate { get; set; }
    }
}
