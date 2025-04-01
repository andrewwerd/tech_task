using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weather.Application.Abstraction;
using Weather.Application.Constants;
using Weather.Application.Services;

namespace Weather.Application.Configuration;
internal static class ConfigureLogStorage
{
    private const string LogStorageConnectionString = "AzureTableStorage";
    public static IServiceCollection AddLogStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(_ =>
            new TableServiceClient(configuration.GetConnectionString(LogStorageConnectionString) ??
                                   throw new ArgumentNullException(nameof(LogStorageConnectionString))));

        services.AddSingleton(sp =>
        {
            var tableServiceClient = sp.GetRequiredService<TableServiceClient>();
            var tableName = Storage.WeatherLogsTable;
            var tableClient = tableServiceClient.GetTableClient(tableName);
            tableClient.CreateIfNotExists();
            return tableClient;
        });

        services.AddScoped<ILogStorageService, LogStorageService>();

        return services;
    }
}