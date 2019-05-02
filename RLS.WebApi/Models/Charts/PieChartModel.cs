using Newtonsoft.Json;

namespace RLS.WebApi.Models.Charts
{
    public class PieChartModel
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}