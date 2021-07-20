using MetaWeatherProject.Tests.DataTypes;
using MetaWeatherProject.Tests.Services;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MetaWeatherProject.Tests
{
    [TestFixture]
    public class MetaWeatherApiTests
    {
        private HttpClient client;
        private readonly string baseUrl = @"https://www.metaweather.com/api";

        [OneTimeSetUp]
        public void Setup()
        {
            client = new HttpClient();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            if (client != null)
            {
                client.Dispose();
            }
        }

        public async Task<HttpResponseMessage> SendRequest(string anotherUrl)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"{baseUrl}/{anotherUrl}"));
            var response = await client.SendAsync(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $"Invalid request: \"{anotherUrl}\".");

            return response;
        }

        [Test, Order(1)]
        [TestCase("Minsk", "min")]
        public async Task GetCityOutOfMatchesWithPattern(string city, string pattern)
        {
            var response = await SendRequest($"location/search/?query={pattern}");

            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<LocationSearchResponse>>(jsonResult);

            Assert.That(result.Exists(x => x.Title == city), $"City {city} is not found by pattern \"{pattern}\".");
        }

        [Test, Order(2)]
        [TestCase("53.90255", "27.563101", "Minsk")]
        public async Task CheckLatitudeAndLongitudeMatchingWithRealData(string latitude, string longitude, string city)
        {
            var response = await SendRequest($"location/search/?query={city}");

            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<LocationSearchResponse>>(jsonResult);

            Assert.That(
                result.Exists(x => x.LattLong == $"{latitude},{longitude}" && x.Title == city),
                $"Coordinates {latitude},{longitude} is not matched to city {city}."
                );
        }

        [Test, Order(3)]
        [TestCase(834463)]
        public async Task SearchTodayWeatherForCityByWoeid(int woeid)
        {
            var todayDate = DateTime.Now.ToString("yyyy/M/d", CultureInfo.InvariantCulture);
            var response = await SendRequest($"location/{woeid}/{todayDate}");

            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<LocationSearchResponse>>(jsonResult);

            Assert.That(result.Count > 0, $"Weather data are not found for woeid={woeid}.");
        }

        [Test, Order(4)]
        [TestCase(834463)]
        public async Task CheckTemperatureForAllDays_DependingOnSeason(int woeid, int minTemperature = 0, int maxTemperature = 0)
        {
            var todayDate = DateTime.Now.ToString("yyyy/M/d", CultureInfo.InvariantCulture);
            var response = await SendRequest($"location/{woeid}/{todayDate}");

            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ConsolidatedWeatherResponse>>(jsonResult);

            bool isRight = result.All(x => SeasonService.IsRightTemperatureForSeason(x, minTemperature, maxTemperature));

            Assert.IsTrue(isRight, $"Temperature is not valid for all days.");
        }

        [Test, Order(5)]
        [TestCase(834463, 5)]
        [TestCase(834463, 1)]
        public async Task CheckNthYearsOldWeatherData_IfWeatherStateNameValueIsMatchedWithToday(int woeid, int yearsAgo)
        {
            var date = DateTime.Now.AddYears(-yearsAgo).ToString("yyyy/M/d", CultureInfo.InvariantCulture);
            var response = await SendRequest($"location/{woeid}/{date}");

            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ConsolidatedWeatherResponse>>(jsonResult);

            bool isExisted = result.Any(x => x.WeatherStateName == "Clear");

            Assert.IsTrue(isExisted, $"There is no any \"weather_state_name\" value {yearsAgo} years ago that would match today's.");
        }
    }
}