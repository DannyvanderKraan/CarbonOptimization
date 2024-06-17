using Azure.Identity;
using CarbonOptimization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

// Set up services.
var services = new ServiceCollection();
services.AddHttpClient();

// Set up configuration sources.
//var configuration = new ConfigurationBuilder()
//    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
//    .AddJsonFile("appsettings.json", true)
//    .AddUserSecrets<Program>()
//    .Build();
//services.Configure<CarbonOptimizationClientOptions>(configuration.GetSection("CarbonOptimizationClient"));

// or:

services.AddSingleton(Options.Create(new CarbonOptimizationClientOptions { TenantId = "", Scopes = [""] }));

services.AddSingleton(p => CarbonOptimizationClient.WithTokenCredential(new DefaultAzureCredential(), p.GetRequiredService<IOptions<CarbonOptimizationClientOptions>>(), p.GetRequiredService<IHttpClientFactory>()));

// Build the service provider.
var serviceProvider = services.BuildServiceProvider();

// Use the service.
var client = serviceProvider.GetRequiredService<CarbonOptimizationClient>();

var dateRange = await client.GetCarbonEmissionDataAvailableDateRange();

Console.WriteLine($"Carbon emission data is available from {dateRange?.StartDate} to {dateRange?.EndDate}.");

var subscriptions = serviceProvider.GetRequiredService<IOptions<CarbonOptimizationClientOptions>>().Value.Scopes;

ItemDetailsQuery itemDetailsQuery = new ItemDetailsQuery
{
    CarbonScopeList = [EmissionScopeEnum.Scope1, EmissionScopeEnum.Scope2, EmissionScopeEnum.Scope3],
    CategoryType = CategoryTypeEnum.Resource,
    DateRange = new DateRange
    {
        StartDate = new DateTime(2024, 1, 1),
        EndDate = new DateTime(2024, 1, 1)
    },
    OrderBy = OrderByEnum.TotalCarbonEmission,
    PageSize = 10,
    ResourceGroupUrlList = Array.Empty<string>(),
    SortDirection = SortDirectionEnum.Asc,
    SubscriptionList = subscriptions,
};

var carbonEmissionDataListResult = await client.GetCarbonEmissionReports(itemDetailsQuery, CancellationToken.None);
foreach (var carbonEmissionData in carbonEmissionDataListResult?.Value)
{
    Console.WriteLine($"Item: {carbonEmissionData.ItemName}, Emission: {carbonEmissionData.TotalCarbonEmission}");
}
