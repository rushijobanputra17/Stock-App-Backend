using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class UpateUserTb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15960514-3e69-4dd6-86ec-5db0ac95ccac");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72d8db9f-3907-4a36-8070-46ebb28c1b83");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e41fb67c-e0de-4d2c-801c-4ca836283436");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "15960514-3e69-4dd6-86ec-5db0ac95ccac", null, "CompanyAdmin", "COMPANYADMIN" },
                    { "72d8db9f-3907-4a36-8070-46ebb28c1b83", null, "user", "USER" },
                    { "e41fb67c-e0de-4d2c-801c-4ca836283436", null, "SuperAdmin", "SUPERADMIN" }
                });
        }
    }
}
