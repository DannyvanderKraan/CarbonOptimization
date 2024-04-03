using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarbonOptimization
{
    public class CarbonEmissionDataListResult
    {
        [JsonProperty("skipToken")]
        public string SkipToken { get; set; }
        [JsonProperty("value")]
        public CarbonEmissionItemDetailData[] Value { get; set; }
    }
}
