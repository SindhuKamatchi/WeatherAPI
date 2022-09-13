using Microsoft.Extensions.Options;
using Moq;
using System;
using WeatherAPI.Client;
using WeatherAPI.Models.ConfigData;
using WeatherAPI.Controllers;
using WeatherAPI.Models.Response;
using WeatherAPI.Test.Fixtures;
using Xunit;
using Microsoft.Extensions.Logging;
using Moq.Contrib.HttpClient;
using WeatherAPI.Tests.Fixtures;
using System.Text.Json;
using FluentAssertions;
using System.Net.Mime;
using Moq.Protected;
using System.Net;

namespace WeatherAPI.Test
{
    public class WeatherClientTests
    {
        private readonly Mock<HttpMessageHandler> _mockMessageHandler;
        private readonly Mock<IOptions<ServiceSettings>> _mockConfig;
        private readonly Mock<ILogger<WeatherClient>> _mockLogger;
        private readonly IWeatherClient _underTest;
        private string city = "London";
        private string country = "United Kingdom";


        public WeatherClientTests()
        {
            _mockMessageHandler = new Mock<HttpMessageHandler>();
            _mockConfig = new Mock<IOptions<ServiceSettings>>();
            _mockLogger = new Mock<ILogger<WeatherClient>>();
            _mockConfig.Setup(config => config.Value).Returns(ConfigFixtures.DefaultConfig());
            var client = _mockMessageHandler.CreateClient();
            _underTest = new WeatherClient(client, _mockConfig.Object, _mockLogger.Object);

        }
        [Fact]
        public async Task WhenWeatherClientIsCalled_WithCity()
        {

            _mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(RootResponseFixture.ValidRootResponse))
                });

            ////Arrange
            //_mockMessageHandler.SetupRequest(ConfigFixtures.DefaultConfig().OpenWeatherHost)
            //    .ReturnsJsonResponse(new HttpResponseMessage
            //    {
            //        StatusCode = HttpStatusCode.OK,
            //        Content = new StringContent(JsonSerializer.Serialize(RootResponseFixture.ValidRootResponse))
            //   });

                    // Act
            var actualResponse = await _underTest.GetCurrentWeatherAsync(city, country);

            // Assert
            _mockMessageHandler.VerifyAnyRequest(Times.Exactly(1));
            actualResponse.Should().BeEquivalentTo(RootResponseFixture.ValidRootResponse);
        }

        [Fact]
        public async Task WhenWeatherClientIsCalled_WithOutCity()
        {
            _mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(RootResponseFixture.ErrorRootResponse))
                });

            // Act
            var actualResponse = await _underTest.GetCurrentWeatherAsync(String.Empty, country);

            // Assert
            //_mockMessageHandler.VerifyAnyRequest(Times.Exactly(1));
            actualResponse.Should().BeEquivalentTo(RootResponseFixture.ErrorRootResponse);
        }
    }
}
