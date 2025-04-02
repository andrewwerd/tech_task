using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Weather.Application.Features.Cities.AddCity;

namespace Weather.Api.Endpoints.Countries.AddCountryCity;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/countries/{countryId}/cities", Handler)
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handler([FromRoute] Guid countryId, [FromBody] AddCityCommand command,
        IMediator mediator, CancellationToken cancellationToken)
    {
        command.CountryId = countryId;

        await mediator.Send(command, cancellationToken);

        return Results.Created();
    }
}