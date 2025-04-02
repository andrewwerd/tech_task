using Microsoft.EntityFrameworkCore;
using Weather.Domain.Models;

namespace Weather.Application.Abstraction.Data;
public interface IDbContext
{
    DbSet<Country> Countries { get; }
    DbSet<City> Cities { get; }
    DbSet<CityWeather> CityWeathers { get; }
    void Insert<TEntity>(TEntity entity) where TEntity : class;
}
