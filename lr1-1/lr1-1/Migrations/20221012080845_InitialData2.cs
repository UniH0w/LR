using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lr1_1.Migrations
{
    public partial class InitialData2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Manufacturer_ManufacturerId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Manufacturer");

            migrationBuilder.DropIndex(
                name: "IX_Products_ManufacturerId",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Manufacturer",
                columns: table => new
                {
                    ManufacturerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ManufacturerId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NameManufacturer = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Products_ManufacturerId",
                table: "Products",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturer_ManufacturerId1",
                table: "Manufacturer",
                column: "ManufacturerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Manufacturer_ManufacturerId",
                table: "Products",
                column: "ManufacturerId",
                principalTable: "Manufacturer",
                principalColumn: "ManufacturerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
