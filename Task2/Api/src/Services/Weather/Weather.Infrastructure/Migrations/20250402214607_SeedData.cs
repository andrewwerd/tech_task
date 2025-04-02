using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Weather.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { new Guid("2188929d-638b-40d3-a0a5-013ababf10ff"), "US", "United States of America" },
                    { new Guid("22cdf7b7-e569-4960-ac2d-917ad9fd6726"), "DE", "Germany" },
                    { new Guid("44b63bf0-e34d-4d00-b9f7-0cbfbb50a679"), "UK", "United Kingdom" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { new Guid("12e17112-a6e5-49fb-998a-69369eb40bf6"), new Guid("44b63bf0-e34d-4d00-b9f7-0cbfbb50a679"), 51.507399999999997, -0.1278, "London" },
                    { new Guid("2dbd20dc-8fc2-41d7-bf3d-3e1045727e82"), new Guid("2188929d-638b-40d3-a0a5-013ababf10ff"), 34.052199999999999, -118.2437, "Los Angeles" },
                    { new Guid("2f3a0134-4b72-4014-8dde-1ebb7968e90c"), new Guid("44b63bf0-e34d-4d00-b9f7-0cbfbb50a679"), 53.480800000000002, -2.2425999999999999, "Manchester" },
                    { new Guid("419841ea-14a6-4f86-a440-22aeb7c94bc2"), new Guid("22cdf7b7-e569-4960-ac2d-917ad9fd6726"), 52.520000000000003, 13.404999999999999, "Berlin" },
                    { new Guid("77e1a8b4-bb1d-4721-af80-ee788dcbc493"), new Guid("22cdf7b7-e569-4960-ac2d-917ad9fd6726"), 48.135100000000001, 11.582000000000001, "Munich" },
                    { new Guid("90fbf093-8d67-434b-8b4e-598f58e0abde"), new Guid("2188929d-638b-40d3-a0a5-013ababf10ff"), 40.712800000000001, -74.006, "New York" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("12e17112-a6e5-49fb-998a-69369eb40bf6"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("2dbd20dc-8fc2-41d7-bf3d-3e1045727e82"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("2f3a0134-4b72-4014-8dde-1ebb7968e90c"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("419841ea-14a6-4f86-a440-22aeb7c94bc2"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("77e1a8b4-bb1d-4721-af80-ee788dcbc493"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("90fbf093-8d67-434b-8b4e-598f58e0abde"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("2188929d-638b-40d3-a0a5-013ababf10ff"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("22cdf7b7-e569-4960-ac2d-917ad9fd6726"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("44b63bf0-e34d-4d00-b9f7-0cbfbb50a679"));
        }
    }
}
