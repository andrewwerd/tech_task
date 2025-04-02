using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Weather.Application.Abstraction.Data;

namespace Weather.Application.Features.CityWeathers.FetchCityWeather;

public class FetchCityWeatherCommandValidator : AbstractValidator<FetchCityWeatherCommand>
{
    private readonly IDbContext _context;

    public FetchCityWeatherCommandValidator(IDbContext context)
    {
        _context = context; 

        RuleFor(command => command.CityId)
            .NotEmpty()
            .WithMessage("City ID is required.");

        RuleFor(command => command.CityId)
            .MustAsync(CityExists)
            .WithMessage("The specified city does not exist.");
    }

    private async Task<bool> CityExists(Guid cityId, CancellationToken cancellationToken) =>
        await _context.Cities
            .AnyAsync(city => city.Id == cityId, cancellationToken);
}