using System.Text.Json.Serialization;

namespace CarbonOptimization;

public class CarbonEmissionItemDetailData
{
    [JsonPropertyName("dataType")]
    public string DataType { get; set; }

    [JsonPropertyName("itemName")]
    public string ItemName { get; set; }

    [JsonPropertyName("categoryType")]
    public string CategoryType { get; set; }

    [JsonPropertyName("groupName")]
    public string GroupName { get; set; }

    [JsonPropertyName("subscriptionId")]
    public string SubscriptionId { get; set; }

    [JsonPropertyName("resourceGroup")]
    public string ResourceGroup { get; set; }

    [JsonPropertyName("resourceId")]
    public string ResourceId { get; set; }

    [JsonPropertyName("resourceType")]
    public string ResourceType { get; set; }

    [JsonPropertyName("totalCarbonEmission")]
    public double TotalCarbonEmission { get; set; }

    [JsonPropertyName("totalCarbonEmission12MonthsAgo")]
    public double TotalCarbonEmission12MonthsAgo { get; set; }

    [JsonPropertyName("totalCarbonEmissionLastMonth")]
    public double TotalCarbonEmissionLastMonth { get; set; }

    [JsonPropertyName("changeRatioFor12Months")]
    public double ChangeRatioFor12Months { get; set; }

    [JsonPropertyName("changeRatioForLastMonth")]
    public double ChangeRatioForLastMonth { get; set; }

    [JsonPropertyName("changeValueMonthOverMonth")]
    public double ChangeValueMonthOverMonth { get; set; }
}
