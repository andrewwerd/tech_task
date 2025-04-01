using Azure.Data.Tables;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;
using Weather.Application.Abstraction;
using Weather.Application.Options;
using Weather.Domain.Entities;

namespace Weather.Application.Services;
public class LogStorageService(TableClient tableClient,
    IOptions<WeatherApiOptions> weatherApiOptions) : ILogStorageService
{
    public async Task SaveSuccessLog(Guid id, DateTime timestamp, CancellationToken cancellationToken = default)
    {
        var entity = new WeatherLog
        {
            RowKey = id.ToString(),
            PartitionKey = weatherApiOptions.Value.City,
            Content = $"{timestamp} - SUCCESS: Weather data retrieved successfully.",
            Timestamp = DateTimeOffset.UtcNow
        };

        await tableClient.AddEntityAsync(entity, cancellationToken);
    }

    public async Task SaveFailureLog(Guid id, DateTime timestamp, string errorMessage, CancellationToken cancellationToken = default)
    {
        var entity = new WeatherLog
        {
            RowKey = id.ToString(),
            PartitionKey = weatherApiOptions.Value.City,
            Content = $"{timestamp} - ERROR: Failed to retrieve weather data. Error: {errorMessage}",
            Timestamp = DateTimeOffset.UtcNow
        };

        await tableClient.AddEntityAsync(entity, cancellationToken);
    }

    public async IAsyncEnumerable<WeatherLog> GetWeatherLogs(DateTimeOffset start, DateTimeOffset end,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var page = tableClient
            .QueryAsync<WeatherLog>(weatherLog => weatherLog.PartitionKey == weatherApiOptions.Value.City &&
                                                  weatherLog.Timestamp >= start &&
                                                  weatherLog.Timestamp <= end,
                cancellationToken: cancellationToken);

        await foreach (var log in page)
        {
            yield return log;
        }
    }
}