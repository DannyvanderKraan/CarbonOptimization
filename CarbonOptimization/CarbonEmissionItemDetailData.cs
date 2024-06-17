using System.Text.Json.Serialization;

namespace CarbonOptimization;

public class CarbonEmissionItemDetailData
{
    [JsonPropertyName("dataType")]
    public string DataType { get; set; } = string.Empty;

    [JsonPropertyName("itemName")]
    public string ItemName { get; set; } = string.Empty;

    [JsonPropertyName("categoryType")]
    public string CategoryType { get; set; } = string.Empty;

    [JsonPropertyName("groupName")]
    public string GroupName { get; set; } = string.Empty;

    [JsonPropertyName("subscriptionId")]
    public string SubscriptionId { get; set; } = string.Empty;

    [JsonPropertyName("resourceGroup")]
    public string ResourceGroup { get; set; } = string.Empty;

    [JsonPropertyName("resourceId")]
    public string ResourceId { get; set; } = string.Empty;

    [JsonPropertyName("resourceType")]
    public string ResourceType { get; set; } = string.Empty;

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
