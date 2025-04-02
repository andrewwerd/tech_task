using MediatR;
using Microsoft.EntityFrameworkCore;
using Weather.Application.Abstraction;
using Weather.Application.Abstraction.Data;
using Weather.Domain.Models;

namespace Weather.Application.Features.CityWeathers.FetchCityWeather;
public class FetchCityWeatherHandler(IWeatherApiService weatherApiService, IDbContext context, IUnitOfWork unitOfWork) :
    IRequestHandler<FetchCityWeatherCommand>
{
    public async Task Handle(FetchCityWeatherCommand request, CancellationToken cancellationToken)
    {
        var city = await context.Cities
            .FirstAsync(item => item.Id == request.CityId, cancellationToken);

        var weatherPayload =
          await weatherApiService.GetWeatherForecastAsync(city.Longitude, city.Latitude, cancellationToken);

        var cityWeather = new CityWeather
        {
            CityId = request.CityId,
            Timestamp = DateTimeOffset.Now,
            MinTemperature = weatherPayload.Main.TempMin,
            MaxTemperature = weatherPayload.Main.TempMax
        };

        context.Insert(cityWeather);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}