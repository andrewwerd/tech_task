using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Weather.Application.Features.Countries.AddCountry;

namespace Weather.Api.Endpoints.Countries.AddCountry;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/countries", Handler)
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handler([FromBody] AddCountryCommand command, IMediator mediator, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);

        return Results.Created();
    }
}