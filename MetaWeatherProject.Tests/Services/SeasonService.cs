using MetaWeatherProject.Tests.DataTypes;
using System;

namespace MetaWeatherProject.Tests.Services
{
    public static class SeasonService
    {
        private enum Season
        {
            Winter,
            Spring,
            Summer,
            Autumn
        }

        private static Season GetSeasonByDate(DateTime date) => date.Month switch
        {
            _ when date.Month >= 3 && date.Month <= 5 => Season.Spring,
            _ when date.Month >= 6 && date.Month <= 8 => Season.Summer,
            _ when date.Month >= 9 && date.Month <= 11 => Season.Autumn,
            _ => Season.Winter
        };

        public static bool IsRightTemperatureForSeason(ConsolidatedWeatherResponse day, int minTemperature, int maxTemperature)
        {
            Season season = SeasonService.GetSeasonByDate(day.CreatedDate);

            if (season == Season.Summer)
            {
                if (day.Temperature > 0)
                {
                    return true;
                }
            }
            else if (season == Season.Winter)
            {
                if (day.Temperature < 0)
                {
                    return true;
                }
            }
            else if (season == Season.Spring || season == Season.Autumn)
            {
                if (day.Temperature > minTemperature && day.Temperature < maxTemperature)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
