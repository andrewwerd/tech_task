using System.Linq.Expressions;
using Azure;
using Azure.Data.Tables;
using Microsoft.Extensions.Options;
using Moq;
using Weather.Application.Options;
using Weather.Application.Services;
using Weather.Domain.Entities;

namespace Weather.Tests.Services;
public class LogStorageServiceTests
{
    private readonly Mock<TableClient> _tableClientMock;
    private readonly LogStorageService _logStorageService;

    public LogStorageServiceTests()
    {
        _tableClientMock = new Mock<TableClient>();
        var optionsMock = new Mock<IOptions<WeatherApiOptions>>();

        optionsMock.Setup(o => o.Value).Returns(new WeatherApiOptions { City = "TestCity" });

        _logStorageService = new LogStorageService(_tableClientMock.Object, optionsMock.Object);
    }

    [Fact]
    public async Task SaveSuccessLog_ShouldSaveSuccessfully()
    {
        // Arrange
        var id = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        _tableClientMock
            .Setup(t => t.AddEntityAsync(It.IsAny<WeatherLog>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Response.FromValue(Mock.Of<Response>(), Mock.Of<Response>()));

        // Act
        await _logStorageService.SaveSuccessLog(id, timestamp);

        // Assert
        _tableClientMock.Verify(t => t.AddEntityAsync(It.Is<WeatherLog>(log =>
            log.RowKey == id.ToString() &&
            log.PartitionKey == "TestCity" &&
            log.Content.Contains("SUCCESS")), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SaveFailureLog_ShouldSaveSuccessfully()
    {
        // Arrange
        var id = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;
        var errorMessage = "Test error message";

        _tableClientMock
            .Setup(t => t.AddEntityAsync(It.IsAny<WeatherLog>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Response.FromValue(Mock.Of<Response>(), Mock.Of<Response>()));

        // Act
        await _logStorageService.SaveFailureLog(id, timestamp, errorMessage);

        // Assert
        _tableClientMock.Verify(t => t.AddEntityAsync(It.Is<WeatherLog>(log =>
            log.RowKey == id.ToString() &&
            log.PartitionKey == "TestCity" &&
            log.Content.Contains("ERROR")), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetWeatherLogs_ShouldReturnLogsWithinDateRange()
    {
        // Arrange
        var start = DateTimeOffset.UtcNow.AddDays(-1);
        var end = DateTimeOffset.UtcNow;
        var logs = new List<WeatherLog>
            {
                new() { PartitionKey = "TestCity", RowKey = Guid.NewGuid().ToString(), Timestamp = DateTimeOffset.UtcNow, Content = "Log 1" },
                new() { PartitionKey = "TestCity", RowKey = Guid.NewGuid().ToString(), Timestamp = DateTimeOffset.UtcNow, Content = "Log 2" }
            };

        _tableClientMock
            .Setup(t => t.QueryAsync(It.IsAny<Expression<Func<WeatherLog, bool>>>(), null, null, It.IsAny<CancellationToken>()))
            .Returns(new MockAsyncPageable<WeatherLog>(logs));

        // Act
        var result = _logStorageService.GetWeatherLogs(start, end, CancellationToken.None);

        // Assert
        var resultList = new List<WeatherLog>();
        await foreach (var log in result)
        {
            resultList.Add(log);
        }

        Assert.Equal(logs.Count, resultList.Count);
    }
}

public class MockAsyncPageable<T>(IEnumerable<T> items) : AsyncPageable<T> where T : class
{
    public override IAsyncEnumerable<Page<T>> AsPages(string? continuationToken = null, int? pageSizeHint = null)
    {
        return ToAsyncEnumerable([Page<T>.FromValues(items.ToList(), null, Mock.Of<Response>())]);
    }

    private static async IAsyncEnumerable<Page<T>> ToAsyncEnumerable(IEnumerable<Page<T>> pages)
    {
        foreach (var page in pages)
        {
            yield return page;
            await Task.Yield();
        }
    }
}
