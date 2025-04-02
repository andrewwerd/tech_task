using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Weather.Application.Features.CityWeathers.GetCityWeathers;

namespace Weather.Api.Endpoints.Countries.GetCityWeathers;
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/countries/{countryId}/cities/{cityId}", Handler)
            .Produces(StatusCodes.Status200OK, typeof(GetCityWeathersResponse))
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handler([FromRoute] Guid countryId, [FromRoute] Guid cityId,
        [FromQuery] DateTimeOffset start, [FromQuery] DateTimeOffset end, IMediator mediator, CancellationToken cancellationToken)
    {
        var request = new GetCityWeathersRequest
        { 
            CityId = cityId,
            Start = start,
            End = end
        }; 
        var result = await mediator.Send(request, cancellationToken);

        return Results.Ok(result);
    }
}