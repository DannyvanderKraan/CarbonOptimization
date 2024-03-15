using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarbonOptimization
{
    public interface ICarbonEmissionReport
    {
        public ReportTypeEnum ReportType { get; }
    }
}
