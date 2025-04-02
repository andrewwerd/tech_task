using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Weather.Application.Abstraction.Data;

namespace Weather.Application.Features.Cities.AddCity;

public class AddCityCommandValidator : AbstractValidator<AddCityCommand>
{
    private readonly IDbContext _context;

    public AddCityCommandValidator(IDbContext context)
    {
        _context = context;

        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("City name is required.");

        RuleFor(command => command.Latitude)
            .NotEmpty()
            .WithMessage("Latitude is required.");

        RuleFor(command => command.Longitude)
            .NotEmpty()
            .WithMessage("Longitude is required.");

        RuleFor(command => command.CountryId)
            .NotEmpty()
            .WithMessage("Country ID is required.");

        RuleFor(command => command.CountryId)
            .MustAsync(CountryExists)
            .WithMessage("The specified country does not exist.");
    }

    private async Task<bool> CountryExists(Guid countryId, CancellationToken cancellationToken) =>
        await _context.Countries
            .AnyAsync(country => country.Id == countryId, cancellationToken);
}