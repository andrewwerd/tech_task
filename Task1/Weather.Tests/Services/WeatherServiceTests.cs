using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;
using Weather.Application.Options;
using Weather.Application.Services;
using Weather.Domain.Entities;

namespace Weather.Tests.Services
{
    public class WeatherServiceTests
    {
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly WeatherService _weatherService;

        public WeatherServiceTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            var optionsMock = new Mock<IOptions<WeatherApiOptions>>();

            optionsMock.Setup(o => o.Value).Returns(new WeatherApiOptions { ApiKey = "test-api-key", City = "TestCity" });

            var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com")
            };

            _weatherService = new WeatherService(httpClient, optionsMock.Object);
        }

        [Fact]
        public async Task GetWeatherAsync_ShouldReturnWeatherPayload_WhenSuccessful()
        {
            // Arrange
            var weatherPayload = new WeatherPayload { ApiId = 1, Name = "Test" };
            var content = JsonConvert.SerializeObject(weatherPayload);
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(content)
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(responseMessage);

            // Act
            var result = await _weatherService.GetWeatherAsync();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(weatherPayload.ApiId, result.Value.ApiId);
        }

        [Fact]
        public async Task GetWeatherAsync_ShouldReturnFailure_WhenHttpRequestExceptionOccurs()
        {
            // Arrange
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ThrowsAsync(new HttpRequestException("Request failed"));

            // Act
            var result = await _weatherService.GetWeatherAsync();

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Request failed", result.Error);
        }

        [Fact]
        public async Task GetWeatherAsync_ShouldReturnFailure_WhenExceptionOccurs()
        {
            // Arrange
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ThrowsAsync(new Exception("Unexpected error"));

            // Act
            var result = await _weatherService.GetWeatherAsync();

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Unexpected error", result.Error);
        }

        [Fact]
        public async Task GetWeatherAsync_ShouldReturnFailure_WhenDeserializationFails()
        {
            // Arrange
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("Invalid JSON")
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(responseMessage);

            // Act
            var result = await _weatherService.GetWeatherAsync();

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Failed to deserialize weather payload.", result.Error);
        }
    }
}

