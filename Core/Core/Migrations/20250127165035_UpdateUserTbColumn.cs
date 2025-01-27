using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserTbColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f3dd065-e503-4835-954a-ac3de9bbb0e5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c4b36e1-5d68-4f8a-a8cb-230cc7f9b6b7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f7ded58-88dc-4c6e-8de1-586187ad0c7c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "580cae08-120d-44a7-b86d-adeda18f8dde", null, "SuperAdmin", "SUPERADMIN" },
                    { "dcefdacf-fd3d-4618-a712-9b05709df642", null, "user", "USER" },
                    { "f9f8bd4e-0249-4e2a-b516-9ed5a36c1ede", null, "CompanyAdmin", "COMPANYADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "580cae08-120d-44a7-b86d-adeda18f8dde");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dcefdacf-fd3d-4618-a712-9b05709df642");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9f8bd4e-0249-4e2a-b516-9ed5a36c1ede");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4f3dd065-e503-4835-954a-ac3de9bbb0e5", null, "CompanyAdmin", "COMPANYADMIN" },
                    { "7c4b36e1-5d68-4f8a-a8cb-230cc7f9b6b7", null, "SuperAdmin", "SUPERADMIN" },
                    { "8f7ded58-88dc-4c6e-8de1-586187ad0c7c", null, "user", "USER" }
                });
        }
    }
}
