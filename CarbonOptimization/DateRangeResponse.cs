using Newtonsoft.Json;

namespace CarbonOptimization
{
    public class DateRangeResponse
    {
        [JsonProperty("startDate")]
        public string StartDate { get; set; }
        [JsonProperty("endDate")]
        public string EndDate { get; set; }
    }
}
