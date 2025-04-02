using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Weather.Application.Features.Cities.GetCities;

namespace Weather.Api.Endpoints.Countries.GetCountryCities;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/countries/{countryId}/cities", Handler)
            .Produces(StatusCodes.Status200OK, typeof(GetCitiesResponse))
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handler([FromRoute] Guid countryId,
        [FromQuery] string? searchTerm, IMediator mediator, CancellationToken cancellationToken)
    {
        var request = new GetCitiesRequest
        {
            SearchTerm = searchTerm,
            CountryId = countryId
        };
        var result = await mediator.Send(request, cancellationToken);

        return Results.Ok(result);
    }
}