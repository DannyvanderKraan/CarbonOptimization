using System.Net.Http.Headers;
using System.Text.Json;
using Azure.Core;
using CarbonOptimization.Authentication;
using Microsoft.Extensions.Options;

namespace CarbonOptimization;

/// <summary>
/// Client to interact with the Carbon Optimization API
/// </summary>
public class CarbonOptimizationClient
{
    private const string CarbonEmissionDataAvailableDateRangeUrl = "https://management.azure.com/providers/Microsoft.Carbon/queryCarbonEmissionDataAvailableDateRange?api-version=2023-04-01-preview";
    private const string CarbonEmissionReportsUrl = "https://management.azure.com/providers/Microsoft.Carbon/carbonEmissionReports?api-version=2023-04-01-preview";

    private readonly IAuthenticator _authenticator;
    private readonly IHttpClientFactory _httpClientFactory;

    private static readonly JsonSerializerOptions Web = new(JsonSerializerDefaults.Web);

    private CarbonOptimizationClient(IAuthenticator authenticator, IHttpClientFactory httpClientFactory)
    {
        _authenticator = authenticator;
        _httpClientFactory = httpClientFactory;
    }

    public static CarbonOptimizationClient WithClientCredentials(IOptions<CarbonOptimizationClientOptions> options, IHttpClientFactory httpClientFactory)
    {
        return new(new ClientCredentialAuthenticator(options.Value), httpClientFactory);
    }

    public static CarbonOptimizationClient WithTokenCredential(TokenCredential tokenCredential, IOptions<CarbonOptimizationClientOptions> options, IHttpClientFactory httpClientFactory)
    {
        return new(new TokenCredentialAuthenticator(tokenCredential, options.Value), httpClientFactory);
    }

    /// <summary>
    /// Get the date range for which carbon emission data is available
    /// </summary>
    /// <returns></returns>
    public async Task<DateRange?> GetCarbonEmissionDataAvailableDateRange(CancellationToken cancellationToken = default)
    {
        using var client = _httpClientFactory.CreateClient("CarbonOptimization");
        var accessToken = await _authenticator.GetAccessToken(cancellationToken);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        using var response = await client.PostAsync(CarbonEmissionDataAvailableDateRangeUrl, null, cancellationToken);

        using var responseContent = await response.Content.ReadAsStreamAsync(cancellationToken);

        var dateRangeResponse = await JsonSerializer.DeserializeAsync<DateRangeResponse>(responseContent, Web, cancellationToken: cancellationToken);
        if (dateRangeResponse != null)
        {
            return DateRange.Create(dateRangeResponse);
        }
        return null;
    }

    /// <summary>
    /// Get carbon emission reports for the given date range
    /// </summary>
    /// <param name="itemDetailsQuery">Item details report type query</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">An argument exception if the start and end date are not in the same month</exception>
    public async Task<CarbonEmissionDataListResult?> GetCarbonEmissionReports(ItemDetailsQuery itemDetailsQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(itemDetailsQuery, nameof(itemDetailsQuery));

        //Currently, the API only supports querying for data within the same month
        if (itemDetailsQuery.DateRange.StartDate.Year != itemDetailsQuery.DateRange.EndDate.Year ||
            itemDetailsQuery.DateRange.StartDate.Month != itemDetailsQuery.DateRange.EndDate.Month)
        {
            throw new ArgumentException("Start date and end date should be in the same month");
        }

        var itemDetailsQueryFilter = new ItemDetailsQueryFilter()
        {
            CarbonScopeList = itemDetailsQuery.CarbonScopeList.Select(x => x.ToString()).ToArray(),
            CategoryType = itemDetailsQuery.CategoryType.ToString(),
            DateRange = new() { StartDate = itemDetailsQuery.DateRange.StartDate.ToString("yyyy-MM-dd"), EndDate = itemDetailsQuery.DateRange.EndDate.ToString("yyyy-MM-dd") },
            GroupCategory = "",
            OrderBy = itemDetailsQuery.OrderBy.ToString(),
            PageSize = itemDetailsQuery.PageSize,
            ReportType = itemDetailsQuery.ReportType.ToString(),
            ResourceGroupUrlList = itemDetailsQuery.ResourceGroupUrlList,
            SkipToken = itemDetailsQuery.SkipToken,
            SortDirection = itemDetailsQuery.SortDirection.ToString(),
            SubscriptionList = itemDetailsQuery.SubscriptionList
        };

        using var stream = new MemoryStream();
        await JsonSerializer.SerializeAsync(stream, itemDetailsQueryFilter, Web, cancellationToken);
        stream.Position = 0;
        using var content = new StreamContent(stream);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        using var client = _httpClientFactory.CreateClient("CarbonOptimization");
        var accessToken = await _authenticator.GetAccessToken(cancellationToken);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        using var response = await client.PostAsync(CarbonEmissionReportsUrl, content, cancellationToken);

        using var responseContent = await response.Content.ReadAsStreamAsync(cancellationToken);
        var carbonEmissionDataListResult = await JsonSerializer.DeserializeAsync<CarbonEmissionDataListResult>(responseContent, Web, cancellationToken: cancellationToken);
        return carbonEmissionDataListResult;
    }
}
