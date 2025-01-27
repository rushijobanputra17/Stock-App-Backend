using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class Portfoliotb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "663ab3fb-0259-4f4f-8cc4-52305ab4a8af");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf14c5f0-58b2-4002-8746-a2099b62a322");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f5615377-0267-44d5-a1c8-d6cfb316dc85");

            migrationBuilder.CreateTable(
                name: "UserPortfolio",
                columns: table => new
                {
                    PortfolioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockId = table.Column<int>(type: "int", nullable: false),
                    PurchasedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPortfolio", x => x.PortfolioId);
                    table.ForeignKey(
                        name: "FK_UserPortfolio_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserPortfolio_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "StockId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "437ee68f-95be-4886-a7b1-e97dea95ef3a", null, "SuperAdmin", "SUPERADMIN" },
                    { "df64f22c-e271-4408-9fb5-8ec7d0f91506", null, "user", "USER" },
                    { "e88870b5-241a-4d0a-b0cf-a336b63311ae", null, "CompanyAdmin", "COMPANYADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPortfolio_AppUserId",
                table: "UserPortfolio",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPortfolio_StockId",
                table: "UserPortfolio",
                column: "StockId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPortfolio");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "663ab3fb-0259-4f4f-8cc4-52305ab4a8af", null, "SuperAdmin", "SUPERADMIN" },
                    { "bf14c5f0-58b2-4002-8746-a2099b62a322", null, "user", "USER" },
                    { "f5615377-0267-44d5-a1c8-d6cfb316dc85", null, "CompanyAdmin", "COMPANYADMIN" }
                });
        }
    }
}
