using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Weather.Application.Abstraction.Data;

namespace Weather.Application.Features.Countries.AddCountry;

public class AddCountryCommandValidator : AbstractValidator<AddCountryCommand>
{
    private readonly IDbContext _context;

    public AddCountryCommandValidator(IDbContext context)
    {
        _context = context;

        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("Country name is required.");

        RuleFor(command => command.Code)
            .NotEmpty()
            .WithMessage("Country code is required.");

        RuleFor(command => command.Name)
            .MustAsync(BeUniqueName)
            .WithMessage("A country with the same name already exists.");

        RuleFor(command => command.Code)
            .MustAsync(BeUniqueCode)
            .WithMessage("A country with the same code already exists.");
    }

    private async Task<bool> BeUniqueName(string countryName, CancellationToken cancellationToken) =>
        !await _context.Countries
            .AnyAsync(country => country.Name == countryName, cancellationToken);

    private async Task<bool> BeUniqueCode(string countryCode, CancellationToken cancellationToken) =>
        !await _context.Countries
            .AnyAsync(country => country.Code == countryCode, cancellationToken);
}