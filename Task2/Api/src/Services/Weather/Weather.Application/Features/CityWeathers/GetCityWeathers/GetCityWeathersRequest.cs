using MediatR;

namespace Weather.Application.Features.CityWeathers.GetCityWeathers;
public class GetCityWeathersRequest : IRequest<GetCityWeathersResponse>
{
    public Guid CityId { get; init; }
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
}