using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Weather.Domain.Models;

namespace Weather.Infrastructure.EntityConfigurations;
internal class CitiesConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder
            .HasMany(entity => entity.CityWeathers)
            .WithOne(child => child.City)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(entity => entity.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasData(Seeder.Cities.NewYork);
        builder.HasData(Seeder.Cities.LosAngeles);
        builder.HasData(Seeder.Cities.London);
        builder.HasData(Seeder.Cities.Manchester);
        builder.HasData(Seeder.Cities.Berlin);
        builder.HasData(Seeder.Cities.Munich);
    }
}