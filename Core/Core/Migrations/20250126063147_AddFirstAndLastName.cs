using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class AddFirstAndLastName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

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
    }
}
