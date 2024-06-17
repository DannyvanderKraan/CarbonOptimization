﻿using System.Text.Json.Serialization;

namespace CarbonOptimization;

public class CarbonEmissionDataListResult
{
    [JsonPropertyName("skipToken")]
    public string SkipToken { get; set; }

    [JsonPropertyName("value")]
    public CarbonEmissionItemDetailData[] Value { get; set; }
}
