using Weather.Domain.Models;

namespace Weather.Infrastructure;

internal static class Seeder
{
    internal static class Countries
    {
        public static Country UsaCountry = new Country
        {
            Id = Guid.Parse("2188929d-638b-40d3-a0a5-013ababf10ff"),
            Name = "United States of America",
            Code = "US"
        };

        public static Country UkCountry = new Country
        {
            Id = Guid.Parse("44b63bf0-e34d-4d00-b9f7-0cbfbb50a679"),
            Name = "United Kingdom",
            Code = "UK"
        };

        public static Country Germany = new Country
        {
            Id = Guid.Parse("22cdf7b7-e569-4960-ac2d-917ad9fd6726"),
            Name = "Germany",
            Code = "DE"
        };
    }

    internal static class Cities
    {
        public static City NewYork = new City
        {
            Id = Guid.Parse("90fbf093-8d67-434b-8b4e-598f58e0abde"),
            CountryId = Countries.UsaCountry.Id,
            Name = "New York",
            Latitude = 40.7128,
            Longitude = -74.0060
        };

        public static City LosAngeles = new City
        {
            Id = Guid.Parse("2dbd20dc-8fc2-41d7-bf3d-3e1045727e82"),
            CountryId = Countries.UsaCountry.Id,
            Name = "Los Angeles",
            Latitude = 34.0522,
            Longitude = -118.2437
        };

        public static City London = new City
        {
            Id = Guid.Parse("12e17112-a6e5-49fb-998a-69369eb40bf6"),
            CountryId = Countries.UkCountry.Id,
            Name = "London",
            Latitude = 51.5074,
            Longitude = -0.1278
        };

        public static City Manchester = new City
        {
            Id = Guid.Parse("2f3a0134-4b72-4014-8dde-1ebb7968e90c"),
            CountryId = Countries.UkCountry.Id,
            Name = "Manchester",
            Latitude = 53.4808,
            Longitude = -2.2426
        };

        public static City Berlin = new City
        {
            Id = Guid.Parse("419841ea-14a6-4f86-a440-22aeb7c94bc2"),
            CountryId = Countries.Germany.Id,
            Name = "Berlin",
            Latitude = 52.5200,
            Longitude = 13.4050
        };

        public static City Munich = new City
        {
            Id = Guid.Parse("77e1a8b4-bb1d-4721-af80-ee788dcbc493"),
            CountryId = Countries.Germany.Id,
            Name = "Munich",
            Latitude = 48.1351,
            Longitude = 11.5820
        };
    }
}

