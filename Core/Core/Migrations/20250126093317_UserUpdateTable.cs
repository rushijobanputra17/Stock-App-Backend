using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class UserUpdateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14a048d5-0b8e-41d8-98aa-92ff45fce21f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18bb101a-5ecc-4cda-9aae-06a3e1f50cde");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a7236f1-2d7f-4c88-81b9-040d07b17b46");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b35c2c2d-0484-497a-8190-0cee8dacb4fd", null, "user", "USER" },
                    { "e51d32ec-9975-4099-9035-998d3b8d34bd", null, "CompanyAdmin", "COMPANYADMIN" },
                    { "f10908bb-5994-4680-a26b-c5b748f3c603", null, "SuperAdmin", "SUPERADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b35c2c2d-0484-497a-8190-0cee8dacb4fd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e51d32ec-9975-4099-9035-998d3b8d34bd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f10908bb-5994-4680-a26b-c5b748f3c603");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "14a048d5-0b8e-41d8-98aa-92ff45fce21f", null, "SuperAdmin", "SUPERADMIN" },
                    { "18bb101a-5ecc-4cda-9aae-06a3e1f50cde", null, "user", "USER" },
                    { "5a7236f1-2d7f-4c88-81b9-040d07b17b46", null, "CompanyAdmin", "COMPANYADMIN" }
                });
        }
    }
}
