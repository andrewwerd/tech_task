using Weather.Domain.Entities;

namespace Weather.Application.Abstraction;

public interface ILogStorageService
{
    Task SaveSuccessLog(Guid id, DateTime timestamp, CancellationToken cancellationToken = default);
    Task SaveFailureLog(Guid id, DateTime timestamp, string errorMessage, CancellationToken cancellationToken = default);
    IAsyncEnumerable<WeatherLog> GetWeatherLogs(DateTimeOffset start, DateTimeOffset end, CancellationToken cancellationToken = default);
}