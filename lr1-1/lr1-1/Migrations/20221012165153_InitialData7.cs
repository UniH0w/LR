using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lr1_1.Migrations
{
    public partial class InitialData7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("319d5467-6dec-4cad-8dc8-602ff6fe431a"),
                column: "Price",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("be68df46-fd31-4b46-80f2-2ad3fa621e82"),
                column: "Price",
                value: 0);
        }
    }
}
