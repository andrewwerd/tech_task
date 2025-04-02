using MediatR;

namespace Weather.Application.Features.Countries.GetCountries;
public class GetCountriesRequest : IRequest<GetCountriesResponse>
{
    public string? SearchTerm { get; init; }
}