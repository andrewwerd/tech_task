using Weather.Domain.Entities;
using Weather.Domain.Shared;

namespace Weather.Application.Abstraction;
public interface IWeatherService
{
    Task<Result<WeatherPayload>> GetWeatherAsync(CancellationToken cancellationToken = default);
}