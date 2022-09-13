using System;
using WeatherAPI.Models;
using WeatherAPI.Models.ConfigData;

namespace WeatherAPI.Tests.Fixtures
{
    public class ConfigFixtures
    {
        public static ServiceSettings DefaultConfig() => new ServiceSettings
        {
            OpenWeatherHost = new("http://example.com"),
            ApiKey = new("http://example.com")
        };

        public static TemperatureType TempConfig() => new TemperatureType
        {
           Celcius= new("Celcius"),
           Farenheit = new("Farenheit")
        };

    }
}

