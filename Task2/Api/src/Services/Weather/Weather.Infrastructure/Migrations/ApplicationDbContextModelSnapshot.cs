﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Weather.Infrastructure;

#nullable disable

namespace Weather.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Weather.Domain.Models.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = new Guid("90fbf093-8d67-434b-8b4e-598f58e0abde"),
                            CountryId = new Guid("2188929d-638b-40d3-a0a5-013ababf10ff"),
                            Latitude = 40.712800000000001,
                            Longitude = -74.006,
                            Name = "New York"
                        },
                        new
                        {
                            Id = new Guid("2dbd20dc-8fc2-41d7-bf3d-3e1045727e82"),
                            CountryId = new Guid("2188929d-638b-40d3-a0a5-013ababf10ff"),
                            Latitude = 34.052199999999999,
                            Longitude = -118.2437,
                            Name = "Los Angeles"
                        },
                        new
                        {
                            Id = new Guid("12e17112-a6e5-49fb-998a-69369eb40bf6"),
                            CountryId = new Guid("44b63bf0-e34d-4d00-b9f7-0cbfbb50a679"),
                            Latitude = 51.507399999999997,
                            Longitude = -0.1278,
                            Name = "London"
                        },
                        new
                        {
                            Id = new Guid("2f3a0134-4b72-4014-8dde-1ebb7968e90c"),
                            CountryId = new Guid("44b63bf0-e34d-4d00-b9f7-0cbfbb50a679"),
                            Latitude = 53.480800000000002,
                            Longitude = -2.2425999999999999,
                            Name = "Manchester"
                        },
                        new
                        {
                            Id = new Guid("419841ea-14a6-4f86-a440-22aeb7c94bc2"),
                            CountryId = new Guid("22cdf7b7-e569-4960-ac2d-917ad9fd6726"),
                            Latitude = 52.520000000000003,
                            Longitude = 13.404999999999999,
                            Name = "Berlin"
                        },
                        new
                        {
                            Id = new Guid("77e1a8b4-bb1d-4721-af80-ee788dcbc493"),
                            CountryId = new Guid("22cdf7b7-e569-4960-ac2d-917ad9fd6726"),
                            Latitude = 48.135100000000001,
                            Longitude = 11.582000000000001,
                            Name = "Munich"
                        });
                });

            modelBuilder.Entity("Weather.Domain.Models.CityWeather", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("MaxTemperature")
                        .HasColumnType("float");

                    b.Property<double>("MinTemperature")
                        .HasColumnType("float");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("CityWeathers");
                });

            modelBuilder.Entity("Weather.Domain.Models.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2188929d-638b-40d3-a0a5-013ababf10ff"),
                            Code = "US",
                            Name = "United States of America"
                        },
                        new
                        {
                            Id = new Guid("44b63bf0-e34d-4d00-b9f7-0cbfbb50a679"),
                            Code = "UK",
                            Name = "United Kingdom"
                        },
                        new
                        {
                            Id = new Guid("22cdf7b7-e569-4960-ac2d-917ad9fd6726"),
                            Code = "DE",
                            Name = "Germany"
                        });
                });

            modelBuilder.Entity("Weather.Domain.Models.City", b =>
                {
                    b.HasOne("Weather.Domain.Models.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Weather.Domain.Models.CityWeather", b =>
                {
                    b.HasOne("Weather.Domain.Models.City", "City")
                        .WithMany("CityWeathers")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("Weather.Domain.Models.City", b =>
                {
                    b.Navigation("CityWeathers");
                });

            modelBuilder.Entity("Weather.Domain.Models.Country", b =>
                {
                    b.Navigation("Cities");
                });
#pragma warning restore 612, 618
        }
    }
}
