using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weather.Application.Abstraction;
using Weather.Application.Options;
using Weather.Application.Services;

namespace Weather.Application.Configuration;
internal static class ConfigureWeatherApi
{
    public static IServiceCollection AddWeatherApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<WeatherApiOptions>(configuration.GetSection(nameof(WeatherApiOptions)));

        services.AddHttpClient<IWeatherApiService, WeatherApiService>(client =>
        {
            client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/weather");
        });
        return services;
    }
}
