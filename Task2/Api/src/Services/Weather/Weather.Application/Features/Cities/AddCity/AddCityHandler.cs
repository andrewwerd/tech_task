using MediatR;
using Weather.Application.Abstraction.Data;
using Weather.Domain.Models;

namespace Weather.Application.Features.Cities.AddCity;
public class AddCityHandler(IDbContext context, IUnitOfWork unitOfWork) : IRequestHandler<AddCityCommand>
{
    public async Task Handle(AddCityCommand request, CancellationToken cancellationToken)
    {
        var city = new City
        {
            Name = request.Name,
            CountryId = request.CountryId,
            Latitude = request.Latitude,
            Longitude = request.Longitude
        };

        context.Cities.Add(city);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}