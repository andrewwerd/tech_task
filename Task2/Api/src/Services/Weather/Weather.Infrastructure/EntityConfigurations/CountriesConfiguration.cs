using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Weather.Domain.Models;

namespace Weather.Infrastructure.EntityConfigurations;
internal class CountriesConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder
            .HasMany(entity => entity.Cities)
            .WithOne(child => child.Country)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(entity => entity.Code)
            .IsUnique();

        builder.Property(entity => entity.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(entity => entity.Code)
            .HasMaxLength(2)
            .IsRequired();

        builder.HasData(Seeder.Countries.UsaCountry);
        builder.HasData(Seeder.Countries.UkCountry);
        builder.HasData(Seeder.Countries.Germany);
    }
}