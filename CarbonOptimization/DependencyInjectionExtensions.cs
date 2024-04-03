using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarbonOptimization
{
    public static class DependencyInjectionExtensions
    {
        public static void RegisterCarbonOptimizationClient(this IServiceCollection services, string clientId, string clientSecret, string tenantId)
        {
            services.AddSingleton<CarbonOptimizationClient>();
        }
    }
}
