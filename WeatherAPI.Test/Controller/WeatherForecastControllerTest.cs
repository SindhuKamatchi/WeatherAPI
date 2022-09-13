using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Net;
using WeatherAPI.Client;
using WeatherAPI.Controllers;
using WeatherAPI.Models.ConfigData;
using WeatherAPI.Models.Response;
using WeatherAPI.Test.Fixtures;
using WeatherAPI.Tests.Fixtures;
using Xunit;

namespace WeatherAPI.Test
{
    public class WeatherForecastControllerTest
    {
        private readonly Mock<IWeatherClient> _mock_weatherClient;
        private readonly Mock<ILogger<WeatherForecastController>> _mockLogger;
        private readonly Mock<IOptions<TemperatureType>> _mockConfig;
        private WeatherForecastController _underTest;
        string city = "London";
        string country = "United Kingdom";

        public WeatherForecastControllerTest()
        {
            _mockLogger = new Mock<ILogger<WeatherForecastController>>();
            _mockConfig = new Mock<IOptions<TemperatureType>>();
            _mock_weatherClient = new Mock<IWeatherClient>();
            _mockConfig.Setup(config => config.Value).Returns(ConfigFixtures.TempConfig());
            _underTest = new WeatherForecastController(_mockLogger.Object, _mock_weatherClient.Object,_mockConfig.Object);
        }

        [Fact]
        public async Task GetWeatherDetails_With_city_country()
        {
            
            _mock_weatherClient.Setup(client =>
                    client.GetCurrentWeatherAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(RootResponseFixture.ValidRootResponse);

            var response = await _underTest.Get(city, country);
            var result = response.Result as OkObjectResult;
            var resultValue = result.Value as WeatherForecast;
            resultValue.LocalTime.Should().NotBeEmpty();
            resultValue.Weather.Should().NotBeEmpty();
            resultValue.temperature.temperature.Should().Be(20);
            resultValue.temperature.temperatureType.Should().Be("Celcius");
        }

        [Fact]
        public async Task GetWeatherDetails_With_citynull_country()
        {
            _mock_weatherClient.Setup(client =>
                    client.GetCurrentWeatherAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(RootResponseFixture.ErrorRootResponse);

            var response = await _underTest.Get(String.Empty, country);
            var result = response.Result as BadRequestObjectResult;
            result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetWeatherDetails_With_citynull_countrynull()
        {
            _mock_weatherClient.Setup(client =>
                    client.GetCurrentWeatherAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(RootResponseFixture.ErrorRootResponse);

            var response = await _underTest.Get(String.Empty, String.Empty);
            var result = response.Result as BadRequestObjectResult ;
            result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}