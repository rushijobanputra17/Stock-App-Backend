using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserTB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "321313d4-efef-4c3d-bd84-6c2fd836305f", null, "user", "USER" },
                    { "72640cfc-9e6a-411d-96cc-9a42fadc3046", null, "SuperAdmin", "SUPERADMIN" },
                    { "d87ae6aa-9656-4d4b-98f5-69148aea6dcf", null, "CompanyAdmin", "COMPANYADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "321313d4-efef-4c3d-bd84-6c2fd836305f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72640cfc-9e6a-411d-96cc-9a42fadc3046");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d87ae6aa-9656-4d4b-98f5-69148aea6dcf");

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
    }
}
