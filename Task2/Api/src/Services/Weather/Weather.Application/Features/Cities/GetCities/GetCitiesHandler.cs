using MediatR;
using Microsoft.EntityFrameworkCore;
using Weather.Application.Abstraction.Data;

namespace Weather.Application.Features.Cities.GetCities;
public class GetCitiesHandler(IDbContext context) : IRequestHandler<GetCitiesRequest, GetCitiesResponse>
{
    public async Task<GetCitiesResponse> Handle(GetCitiesRequest request, CancellationToken cancellationToken)
    {
        var result = await context.Cities
            .Where(city => city.CountryId == request.CountryId)
            .Where(city => string.IsNullOrWhiteSpace(request.SearchTerm) ||
                              city.Name.ToLower().Contains(request.SearchTerm.Trim().ToLower()))
            .Select(city => new GetCitiesResponse.City
            {
                Id = city.Id,
                Name = city.Name
            })
            .ToListAsync(cancellationToken);

        return new(result);
    }
}