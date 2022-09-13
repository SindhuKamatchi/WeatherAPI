using WeatherAPI.Models.Response;

namespace WeatherAPI.Utils
{
    public class TemperatureConversion
    {
        public Temperature ConvertTemperature(string type, float temperature)
        {
            Temperature temp = new Temperature();
            if (temperature != null)
            {
                switch (type)
                {
                    case "Farenheit":
                        temp.temperature = (int)(temperature * 9) / 5 + 32;
                        temp.temperatureType = "Farenheit";
                        return temp;
                    case "Celcius":
                        temp.temperature = (int)(temperature - 32) * 5 / 9;
                        temp.temperatureType = "Celcius";
                        return temp;
                }
            }
            return temp;
        }
    }
}
