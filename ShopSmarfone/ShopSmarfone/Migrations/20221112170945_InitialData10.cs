using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopSmarfone.Migrations
{
    /// <inheritdoc />
    public partial class InitialData10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Storages",
                keyColumn: "StrorageId",
                keyValue: new Guid("26b89402-b58f-4e0f-9095-85abea9c6a08"),
                columns: new[] { "FullNameProduct", "ProductId" },
                values: new object[] { "Samsung S22", new Guid("319d5467-6dec-4cad-8dc8-602ff6fe431a") });

            migrationBuilder.UpdateData(
                table: "Storages",
                keyColumn: "StrorageId",
                keyValue: new Guid("93362477-fbdf-4909-8a17-707e6617f306"),
                column: "ProductId",
                value: new Guid("be68df46-fd31-4b46-80f2-2ad3fa621e82"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Storages",
                keyColumn: "StrorageId",
                keyValue: new Guid("26b89402-b58f-4e0f-9095-85abea9c6a08"),
                columns: new[] { "FullNameProduct", "ProductId" },
                values: new object[] { "Samsung 20S", new Guid("280ddbaf-77ba-452f-8674-524d838e359a") });

            migrationBuilder.UpdateData(
                table: "Storages",
                keyColumn: "StrorageId",
                keyValue: new Guid("93362477-fbdf-4909-8a17-707e6617f306"),
                column: "ProductId",
                value: new Guid("ffdc95a1-cc97-4f72-94b3-33b98aa69e23"));
        }
    }
}
