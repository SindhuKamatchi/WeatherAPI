using WeatherAPI.Models.Response;

namespace WeatherAPI.Test.Fixtures
{
    public class WeatherForecastResponseFixture
    {
        public static WeatherForecast ValidResponse => new WeatherForecast()
        {
            LocalTime = "07:23",
            Weather = "Light rain shower",
            temperature = new Temperature
            {
                temperatureType = "Celcius",
                temperature = 20
            }
        };

        public static string ErrorResponse
             = "Call to weather API failed";
       
    }
}
