using Microsoft.EntityFrameworkCore;
using Weather.Application.Abstraction.Data;
using Weather.Domain.Models;

namespace Weather.Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IUnitOfWork, IDbContext
{
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<City> Cities => Set<City>();
    public DbSet<CityWeather> CityWeathers => Set<CityWeather>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    } 

    public void Insert<TEntity>(TEntity entity)
        where TEntity : class =>
        Set<TEntity>().Add(entity);

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }
}
