namespace CarbonOptimization;

public class DateRange
{
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }

    internal static DateRange Create(DateRangeResponse dateRangeResponse)
    {
        return new DateRange
        {
            StartDate = DateTimeOffset.Parse(dateRangeResponse.StartDate),
            EndDate = DateTimeOffset.Parse(dateRangeResponse.EndDate)
        };
    }
}
