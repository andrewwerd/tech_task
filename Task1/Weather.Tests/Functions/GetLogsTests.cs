using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Weather.Api.Functions;
using Weather.Application.Abstraction;
using Weather.Domain.Entities;

namespace Weather.Tests.Functions;
public class GetLogsTests
{
    private readonly Mock<ILogStorageService> _logStorageServiceMock;
    private readonly GetLogs _getLogs;

    public GetLogsTests()
    {
        _logStorageServiceMock = new Mock<ILogStorageService>();
        _getLogs = new GetLogs(_logStorageServiceMock.Object);
    }

    [Fact]
    public async Task Run_ShouldReturnBadRequest_WhenDateFormatIsInvalid()
    {
        // Arrange
        var httpRequestMock = new Mock<HttpRequest>();
        httpRequestMock.Setup(req => req.Query["start"]).Returns("invalid-date");
        httpRequestMock.Setup(req => req.Query["end"]).Returns("invalid-date");

        // Act
        var result = await _getLogs.Run(httpRequestMock.Object, CancellationToken.None);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Invalid date format", badRequestResult.Value);
    }

    [Fact]
    public async Task Run_ShouldReturnOkObjectResult_WhenDateFormatIsValid()
    {
        // Arrange
        var periodStart = DateTimeOffset.Now.AddDays(-1);
        var periodEnd = DateTimeOffset.Now;
        var httpRequestMock = new Mock<HttpRequest>();
        httpRequestMock.Setup(req => req.Query["start"]).Returns(periodStart.ToString());
        httpRequestMock.Setup(req => req.Query["end"]).Returns(periodEnd.ToString());

        var weatherLogs = new List<WeatherLog>
            {
                new () { Timestamp = DateTimeOffset.Now, Content = "Log1" },
                new () { Timestamp = DateTimeOffset.Now.AddMinutes(-1), Content = "Log2" }
            };
        _logStorageServiceMock
            .Setup(ls => ls.GetWeatherLogs(It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>(), It.IsAny<CancellationToken>()))
            .Returns(GetAsyncEnumerable(weatherLogs));

        // Act
        var result = await _getLogs.Run(httpRequestMock.Object, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedLogs = Assert.IsType<List<WeatherLog>>(okResult.Value);
        Assert.Equal(2, returnedLogs.Count);
        Assert.Equal("Log1", returnedLogs[0].Content);
        Assert.Equal("Log2", returnedLogs[1].Content);
    }

    private static async IAsyncEnumerable<WeatherLog> GetAsyncEnumerable(IEnumerable<WeatherLog> logs)
    {
        foreach (var log in logs)
        {
            yield return log;
            await Task.Yield();
        }
    }
}