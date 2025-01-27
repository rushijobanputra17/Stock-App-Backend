using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "30e42902-74c1-4bf9-aa19-f5d10170b331", null, "CompanyAdmin", "COMPANYADMIN" },
                    { "85139aaf-e89d-4b61-83dc-8db8a00ddf05", null, "user", "USER" },
                    { "dfc123df-e4f9-400c-8fbf-266302a2083b", null, "SuperAdmin", "SUPERADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30e42902-74c1-4bf9-aa19-f5d10170b331");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85139aaf-e89d-4b61-83dc-8db8a00ddf05");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dfc123df-e4f9-400c-8fbf-266302a2083b");
        }
    }
}
