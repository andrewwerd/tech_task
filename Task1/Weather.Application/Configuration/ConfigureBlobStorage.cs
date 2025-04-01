using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weather.Application.Abstraction;
using Weather.Application.Options;
using Weather.Application.Services;

namespace Weather.Application.Configuration;
internal static class ConfigureBlobStorage
{
    private const string BlobStorageConnectionString = "AzureBlobStorage";
    public static IServiceCollection AddBlobStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BlobOptions>(configuration.GetSection(nameof(BlobOptions)));

        services.AddAzureClients(clientBuilder =>
        {
            clientBuilder.AddBlobServiceClient(configuration.GetConnectionString(BlobStorageConnectionString) ??
                                               throw new ArgumentNullException(nameof(BlobStorageConnectionString)));
        });


        services.AddScoped<IBlobStorageService, BlobStorageService>();

        return services;
    }
}