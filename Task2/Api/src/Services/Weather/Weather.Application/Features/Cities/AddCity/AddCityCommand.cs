using MediatR;

namespace Weather.Application.Features.Cities.AddCity;
public class AddCityCommand : IRequest
{
    public string Name { get; init; } = null!;
    public Guid CountryId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}