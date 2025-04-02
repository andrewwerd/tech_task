using MediatR;
using Microsoft.EntityFrameworkCore;
using Weather.Application.Abstraction.Data;
using Weather.Application.Features.CityWeathers.FetchCityWeather;

namespace Weather.Api.BackgroundServices;
public class WeatherApiBackgroundService(IServiceScopeFactory scopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();

            var cityIds = await dbContext.Cities
                .Select(city => city.Id)
                .ToListAsync(stoppingToken);

            foreach (var cityId in cityIds)
                await mediator.Send(new FetchCityWeatherCommand { CityId = cityId }, CancellationToken.None);

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}