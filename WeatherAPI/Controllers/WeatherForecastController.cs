using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WeatherAPI.Client;
using WeatherAPI.Models.ConfigData;
using WeatherAPI.Models.Response;
using WeatherAPI.Utils;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("v1/weather")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherClient _client;
        private readonly TemperatureType _type;
        TemperatureConversion tc = new TemperatureConversion();

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherClient client, IOptions<TemperatureType> options)
        {
            _logger = logger;
            _client = client;
            _type = options.Value;
        }

        [HttpGet("city={city}&country={country}")]
        public async Task<ActionResult<WeatherForecast>> Get(string city, string country)
        {
            try
            {
                _logger.LogInformation("Request received {city}{country}", city, country);
                var forecast = await _client.GetCurrentWeatherAsync(city, country);
                if (forecast.error ==null)
                {
                    WeatherForecast weatherForecast = new WeatherForecast
                    {
                        Weather = forecast.current.condition.text,
                        LocalTime = DateTime.Parse(forecast.location.localtime).ToString("HH:mm"),
                        temperature = tc.ConvertTemperature(_type.Celcius,forecast.current.temp_f)
                        
                    };
                    return Ok(weatherForecast);
                }
                else
                {
                    return BadRequest(forecast.error.message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Calling {city}{country} at {date}", city, country, DateTime.Now);

                return BadRequest(ex.Message);
            }
        }
    }
}
