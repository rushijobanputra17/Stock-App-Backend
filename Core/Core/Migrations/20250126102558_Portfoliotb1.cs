using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class Portfoliotb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "437ee68f-95be-4886-a7b1-e97dea95ef3a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df64f22c-e271-4408-9fb5-8ec7d0f91506");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e88870b5-241a-4d0a-b0cf-a336b63311ae");

            migrationBuilder.AlterColumn<decimal>(
                name: "PurchasedPrice",
                table: "UserPortfolio",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "UserPortfolio",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "UserPortfolio");

            migrationBuilder.AlterColumn<decimal>(
                name: "PurchasedPrice",
                table: "UserPortfolio",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "437ee68f-95be-4886-a7b1-e97dea95ef3a", null, "SuperAdmin", "SUPERADMIN" },
                    { "df64f22c-e271-4408-9fb5-8ec7d0f91506", null, "user", "USER" },
                    { "e88870b5-241a-4d0a-b0cf-a336b63311ae", null, "CompanyAdmin", "COMPANYADMIN" }
                });
        }
    }
}
