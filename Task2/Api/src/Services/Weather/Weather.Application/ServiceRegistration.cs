using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weather.Application.Configuration;

namespace Weather.Application;
public static class ServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddMediatr()
            .AddWeatherApi(configuration);

        return services;
    }
}