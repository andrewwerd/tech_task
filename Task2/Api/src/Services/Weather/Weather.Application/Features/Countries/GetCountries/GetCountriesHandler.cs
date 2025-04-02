using MediatR;
using Microsoft.EntityFrameworkCore;
using Weather.Application.Abstraction.Data;

namespace Weather.Application.Features.Countries.GetCountries;
public class GetCountriesHandler(IDbContext context) : IRequestHandler<GetCountriesRequest, GetCountriesResponse>
{
    public async Task<GetCountriesResponse> Handle(GetCountriesRequest request, CancellationToken cancellationToken)
    {
        var result = await context.Countries
            .Where(country => string.IsNullOrWhiteSpace(request.SearchTerm) ||
                              country.Name.ToLower().Contains(request.SearchTerm.Trim().ToLower()) ||
                              country.Code.ToLower().Contains(request.SearchTerm.Trim().ToLower()))
            .Select(country => new GetCountriesResponse.Country
            {
                Id = country.Id,
                Name = country.Name
            })
            .ToListAsync(cancellationToken);

        return new(result);
    }
}