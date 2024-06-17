namespace CarbonOptimization;

public class CarbonOptimizationClientOptions
{
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
    public string TenantId { get; set; } = string.Empty;
    public string[] Scopes { get; set; } = [];
}
