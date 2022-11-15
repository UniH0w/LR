using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopSmarfone.Migrations
{
    /// <inheritdoc />
    public partial class InitialData55 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("43a0b53b-8eff-45a7-a0cd-724eaa2eefc3"),
                column: "Price",
                value: 3000);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("815eaeb2-2a27-45f0-b9b8-235bbe1fd08c"),
                column: "Price",
                value: 1500);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Orders");
        }
    }
}
