using MediatR;
using Weather.Application.Abstraction.Data;
using Weather.Domain.Models;

namespace Weather.Application.Features.Countries.AddCountry;
public class AddCountryHandler(IDbContext context, IUnitOfWork unitOfWork) : IRequestHandler<AddCountryCommand>
{
    public async Task Handle(AddCountryCommand request, CancellationToken cancellationToken)
    {
        var country = new Country
        {
            Code = request.Code,
            Name = request.Name,
        };

        context.Countries.Add(country);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}