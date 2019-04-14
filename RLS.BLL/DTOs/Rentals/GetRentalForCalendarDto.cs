using Newtonsoft.Json;

namespace RLS.BLL.DTOs.Rentals
{
    public class GetRentalForCalendarDto
    {
        [JsonProperty(PropertyName = "start")]
        public string Start { get; set; }

        [JsonProperty(PropertyName = "end")]
        public string End { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }
    }
}