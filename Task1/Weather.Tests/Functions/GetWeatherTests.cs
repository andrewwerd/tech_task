using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Weather.Api.Functions;
using Weather.Application.Abstraction;
using Weather.Domain.Entities;

namespace Weather.Tests.Functions;
public class GetWeatherTests
{
    private readonly Mock<IBlobStorageService> _blobStorageServiceMock;
    private readonly GetWeather _getWeather;

    public GetWeatherTests()
    {
        _blobStorageServiceMock = new Mock<IBlobStorageService>();
        _getWeather = new GetWeather(_blobStorageServiceMock.Object);
    }

    [Fact]
    public async Task Run_ShouldReturnBadRequest_WhenLogIdIsInvalid()
    {
        // Arrange
        var httpRequestMock = new Mock<HttpRequest>();
        httpRequestMock.Setup(req => req.Query["logId"]).Returns("invalid-guid"); 

        // Act
        var result = await _getWeather.Run(httpRequestMock.Object, CancellationToken.None);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Invalid log id format", badRequestResult.Value);
    }

    [Fact]
    public async Task Run_ShouldReturnOkObjectResult_WhenLogIdIsValid()
    {
        // Arrange
        var logId = Guid.NewGuid();
        var httpRequestMock = new Mock<HttpRequest>();
        httpRequestMock.Setup(req => req.Query["logId"]).Returns(logId.ToString()); 

        var weatherPayload = new WeatherPayload();
        _blobStorageServiceMock
            .Setup(bs => bs.GetWeatherPayload(logId, CancellationToken.None))
            .ReturnsAsync(weatherPayload);

        // Act
        var result = await _getWeather.Run(httpRequestMock.Object, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(weatherPayload, okResult.Value);
    }
}