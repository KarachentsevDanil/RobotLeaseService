using Newtonsoft.Json;

namespace RLS.WebApi.Models.Charts
{
    public class BarChartModel
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}