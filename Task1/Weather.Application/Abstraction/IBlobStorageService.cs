using Weather.Domain.Entities;

namespace Weather.Application.Abstraction;
public interface IBlobStorageService
{
    Task SaveWeatherPayload(Guid id, WeatherPayload entity, CancellationToken cancellationToken = default);
    Task<WeatherPayload?> GetWeatherPayload(Guid id, CancellationToken cancellationToken = default);
}