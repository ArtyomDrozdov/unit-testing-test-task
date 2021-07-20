using Newtonsoft.Json;

namespace MetaWeatherProject.Tests.DataTypes
{
    public class LocationSearchResponse
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("location_type")]
        public string LocationType { get; set; }
        [JsonProperty("latt_long")]
        public string LattLong { get; set; }
        [JsonProperty("woeid")]
        public int Woeid { get; set; }
    }
}
