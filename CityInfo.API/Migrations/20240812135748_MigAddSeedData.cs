using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CityInfo.API.Migrations
{
    /// <inheritdoc />
    public partial class MigAddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Here is karaj", "Karaj" },
                    { 2, "Here is Tehran", "Tehran" },
                    { 3, "Here is Mashad", "Mashad" }
                });

            migrationBuilder.InsertData(
                table: "PointOfIntrests",
                columns: new[] { "Id", "CityId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Baraghan" },
                    { 2, 1, "Talaghan" },
                    { 3, 2, "Lavasan" },
                    { 4, 2, "Azadi" },
                    { 5, 3, "Idont know abad" },
                    { 6, 3, "Idont know abad 2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PointOfIntrests",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PointOfIntrests",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PointOfIntrests",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PointOfIntrests",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PointOfIntrests",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PointOfIntrests",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
