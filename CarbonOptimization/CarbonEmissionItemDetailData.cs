using Newtonsoft.Json;

namespace CarbonOptimization
{
    public class CarbonEmissionItemDetailData
    {
        [JsonProperty("dataType")]
        public string DataType { get; set; }

        [JsonProperty("itemName")]
        public string ItemName { get; set; }

        [JsonProperty("categoryType")]
        public string CategoryType { get; set; }

        [JsonProperty("groupName")]
        public string GroupName { get; set; }

        [JsonProperty("subscriptionId")]
        public string SubscriptionId { get; set; }

        [JsonProperty("resourceGroup")]
        public string ResourceGroup { get; set; }

        [JsonProperty("resourceId")]
        public string ResourceId { get; set; }

        [JsonProperty("resourceType")]
        public string ResourceType { get; set; }

        [JsonProperty("totalCarbonEmission")]
        public double TotalCarbonEmission { get; set; }

        [JsonProperty("totalCarbonEmission12MonthsAgo")]
        public double TotalCarbonEmission12MonthsAgo { get; set; }

        [JsonProperty("totalCarbonEmissionLastMonth")]
        public double TotalCarbonEmissionLastMonth { get; set; }

        [JsonProperty("changeRatioFor12Months")]
        public double ChangeRatioFor12Months { get; set; }

        [JsonProperty("changeRatioForLastMonth")]
        public double ChangeRatioForLastMonth { get; set; }

        [JsonProperty("changeValueMonthOverMonth")]
        public double ChangeValueMonthOverMonth { get; set; }
    }
}
