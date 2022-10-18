using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lr1_1.Migrations
{
    public partial class InitialData1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Manufacturer",
                columns: table => new
                {
                    ManufacturerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameManufacturer = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ManufacturerId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturer", x => x.ManufacturerId);
                    table.ForeignKey(
                        name: "FK_Manufacturer_Manufacturer_ManufacturerId1",
                        column: x => x.ManufacturerId1,
                        principalTable: "Manufacturer",
                        principalColumn: "ManufacturerId");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ManufacturerId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 30, nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    NameModels = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Manufacturer_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturer",
                        principalColumn: "ManufacturerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturer_ManufacturerId1",
                table: "Manufacturer",
                column: "ManufacturerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ManufacturerId",
                table: "Products",
                column: "ManufacturerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Manufacturer");
        }
    }
}
