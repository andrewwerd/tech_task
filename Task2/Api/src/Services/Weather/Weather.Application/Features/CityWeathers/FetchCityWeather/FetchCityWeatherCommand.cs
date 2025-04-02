using MediatR;

namespace Weather.Application.Features.CityWeathers.FetchCityWeather;
public class FetchCityWeatherCommand : IRequest
{
    public Guid CityId { get; set; }
}