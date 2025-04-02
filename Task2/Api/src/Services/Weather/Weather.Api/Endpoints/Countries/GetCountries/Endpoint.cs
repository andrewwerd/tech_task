using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Weather.Application.Features.Countries.GetCountries;

namespace Weather.Api.Endpoints.Countries.GetCountries;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/countries", Handler)
            .Produces(StatusCodes.Status200OK, typeof(GetCountriesResponse))
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handler([FromQuery] string? searchTerm, IMediator mediator, CancellationToken cancellationToken)
    {
        var request = new GetCountriesRequest
        {
            SearchTerm = searchTerm
        };
        var result = await mediator.Send(request, cancellationToken);

        return Results.Ok(result);
    }
}