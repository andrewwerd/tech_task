using Weather.Api.BackgroundServices;

namespace Weather.Api.Configuration;
internal static class ConfigureBackgroundService
{
    public static IServiceCollection AddBackgroundService(this IServiceCollection services)
    {
        services.AddHostedService<WeatherApiBackgroundService>();
        return services;
    }
}