using MediatR;

namespace Weather.Application.Features.Cities.GetCities;
public class GetCitiesRequest : IRequest<GetCitiesResponse>
{
    public string? SearchTerm { get; init; }
    public Guid? CountryId { get; init; }
}