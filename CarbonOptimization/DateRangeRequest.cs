using Newtonsoft.Json;

namespace CarbonOptimization
{
    public class DateRangeRequest
    {
        [JsonProperty("start")]
        public string StartDate { get; set; }
        [JsonProperty("end")]
        public string EndDate { get; set; }
    }
}
