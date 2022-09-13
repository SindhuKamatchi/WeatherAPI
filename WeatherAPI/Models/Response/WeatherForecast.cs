using System;

namespace WeatherAPI.Models.Response
{
    public class WeatherForecast
    {
        public string LocalTime { get; set; }

        public string Weather { get; set; }

        public Temperature temperature { get; set; }
    }
}
