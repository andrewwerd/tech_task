using Weather.Application.Models;

namespace Weather.Application.Abstraction;
public interface IWeatherApiService
{
    Task<WeatherPayload> GetWeatherForecastAsync(double lon, double lat, CancellationToken cancellationToken);
}