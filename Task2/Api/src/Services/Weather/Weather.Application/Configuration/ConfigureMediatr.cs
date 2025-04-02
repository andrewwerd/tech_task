using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Weather.Application.Features.Countries.AddCountry;
using Weather.Application.Pipelines;

namespace Weather.Application.Configuration;
internal static class ConfigureMediatr
{
    public static IServiceCollection AddMediatr(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssemblyContaining<AddCountryCommandValidator>(ServiceLifetime.Scoped); 
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}