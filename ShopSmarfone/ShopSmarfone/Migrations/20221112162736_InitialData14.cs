using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopSmarfone.Migrations
{
    /// <inheritdoc />
    public partial class InitialData14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("319d5467-6dec-4cad-8dc8-602ff6fe431a"),
                column: "Price",
                value: 15000);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("be68df46-fd31-4b46-80f2-2ad3fa621e82"),
                column: "Price",
                value: 20000);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Products",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("319d5467-6dec-4cad-8dc8-602ff6fe431a"),
                column: "Price",
                value: 15000.0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("be68df46-fd31-4b46-80f2-2ad3fa621e82"),
                column: "Price",
                value: 20000.0);
        }
    }
}
