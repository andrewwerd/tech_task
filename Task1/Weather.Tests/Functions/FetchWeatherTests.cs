using Microsoft.Azure.Functions.Worker;
using Moq;
using Weather.Api.Functions;
using Weather.Application.Abstraction;
using Weather.Domain.Entities;
using Weather.Domain.Shared;

namespace Weather.Tests.Functions;
public class FetchWeatherTests
{
    private readonly Mock<IWeatherService> _weatherServiceMock;
    private readonly Mock<ILogStorageService> _logStorageServiceMock;
    private readonly Mock<IBlobStorageService> _blobStorageServiceMock;
    private readonly FetchWeather _fetchWeather;

    public FetchWeatherTests()
    {
        _weatherServiceMock = new Mock<IWeatherService>();
        _logStorageServiceMock = new Mock<ILogStorageService>();
        _blobStorageServiceMock = new Mock<IBlobStorageService>();
        _fetchWeather = new FetchWeather(
            _weatherServiceMock.Object,
            _logStorageServiceMock.Object,
            _blobStorageServiceMock.Object);
    }

    [Fact]
    public async Task Run_ShouldSaveSuccessLogAndWeatherPayload_WhenWeatherServiceSucceeds()
    {
        // Arrange
        var timerInfo = new TimerInfo();

        var weatherPayload = new WeatherPayload();
        _weatherServiceMock
            .Setup(ws => ws.GetWeatherAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(weatherPayload));

        // Act
        await _fetchWeather.Run(timerInfo, CancellationToken.None);

        // Assert
        _logStorageServiceMock.Verify(ls => ls.SaveSuccessLog(It.IsAny<Guid>(), It.IsAny<DateTime>(), CancellationToken.None), Times.Once);
        _blobStorageServiceMock.Verify(bs => bs.SaveWeatherPayload(It.IsAny<Guid>(), weatherPayload, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Run_ShouldSaveFailureLog_WhenWeatherServiceFails()
    {
        // Arrange
        var timerInfo = new TimerInfo();

        var errorMessage = "Error fetching weather";
        _weatherServiceMock
            .Setup(ws => ws.GetWeatherAsync(CancellationToken.None))
            .ReturnsAsync(Result.Failure<WeatherPayload>(errorMessage));

        // Act
        await _fetchWeather.Run(timerInfo, It.IsAny<CancellationToken>());

        // Assert
        _logStorageServiceMock.Verify(ls => ls.SaveFailureLog(It.IsAny<Guid>(), It.IsAny<DateTime>(), errorMessage, CancellationToken.None), Times.Once);
        _blobStorageServiceMock.Verify(bs => bs.SaveWeatherPayload(It.IsAny<Guid>(), It.IsAny<WeatherPayload>(), CancellationToken.None), Times.Never);
    }
}