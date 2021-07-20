using Newtonsoft.Json;
using System;

namespace MetaWeatherProject.Tests.DataTypes
{
    public class ConsolidatedWeatherResponse
    {
        [JsonProperty("applicable_date")]
        public DateTime ApplicableDate { get; set; }
        [JsonProperty("created")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty("the_temp")]
        public float Temperature { get; set; }
        [JsonProperty("weather_state_name")]
        public string WeatherStateName { get; set; }
    }
}
