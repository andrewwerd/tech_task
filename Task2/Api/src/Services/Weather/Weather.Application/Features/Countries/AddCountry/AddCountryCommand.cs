using MediatR;

namespace Weather.Application.Features.Countries.AddCountry;
public class AddCountryCommand : IRequest 
{
    public string Name { get; init; } = null!;
    public string Code { get; init; } = null!;
}