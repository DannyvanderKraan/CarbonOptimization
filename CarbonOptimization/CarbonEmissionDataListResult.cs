using Newtonsoft.Json;

namespace CarbonOptimization
{
    public class CarbonEmissionDataListResult
    {
        [JsonProperty("skipToken")]
        public string SkipToken { get; set; }
        [JsonProperty("value")]
        public CarbonEmissionItemDetailData[] Value { get; set; }
    }
}
