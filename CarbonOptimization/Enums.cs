using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarbonOptimization
{
    public enum EmissionScopeEnum
    {
        Scope1,
        Scope2,
        Scope3
    }

    public enum CategoryTypeEnum
    {
        Location,
        Resource,
        ResourceGroup,
        ServiceType,
        Subscription
    }

    public enum SortDirectionEnum
    {
        Asc,
        Desc
    }

    public enum OrderByEnum
    {
        Name,
        TotalCarbonEmission,
        TotalCarbonEmissionLastMonth,
        SubscriptionId
    }

    public enum ReportTypeEnum
    {
        ItemDetailReport,
        OverallSummaryReport
    }

    public enum GroupCategoryEnum
    {
        ResourceGroup,
        SubscriptionId
    }
}
