using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weather.Application.Configuration;

namespace Weather.Application;
public static class RegisterApplication
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddBlobStorage(configuration)
            .AddLogStorage(configuration)
            .AddWeatherService(configuration);

        return services;
    }
}