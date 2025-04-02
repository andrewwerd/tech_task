using MediatR;
using Microsoft.EntityFrameworkCore;
using Weather.Application.Abstraction.Data;

namespace Weather.Application.Features.CityWeathers.GetCityWeathers;
public class GetCityWeathersHandler(IDbContext context) : IRequestHandler<GetCityWeathersRequest, GetCityWeathersResponse>
{
    public async Task<GetCityWeathersResponse> Handle(GetCityWeathersRequest request, CancellationToken cancellationToken)
    {
        var result = await context.CityWeathers
            .Where(cityWeather => cityWeather.CityId == request.CityId)
            .Where(cityWeather => cityWeather.Timestamp >= request.Start &&
                                  cityWeather.Timestamp <= request.End)
            .Select(cityWeather => new GetCityWeathersResponse.CityWeather
            {
                Timestamp = cityWeather.Timestamp,
                MinTemperature = cityWeather.MinTemperature,
                MaxTemperature = cityWeather.MaxTemperature
            })
            .OrderByDescending(cityWeather => cityWeather.Timestamp)
            .ToListAsync(cancellationToken);

        return new(result);
    }
}