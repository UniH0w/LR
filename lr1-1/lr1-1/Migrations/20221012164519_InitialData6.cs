using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lr1_1.Migrations
{
    public partial class InitialData6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Buyers",
                columns: new[] { "BuyerId", "Family", "MiddleName", "Name" },
                values: new object[,]
                {
                    { new Guid("280ddbaf-77ba-452f-8674-524d838e359a"), "Salticov", "Alexandrovich", "Ivan" },
                    { new Guid("ffdc95a1-cc97-4f72-94b3-33b98aa69e23"), "Kosov", "Alexandrovich", "Sergey" }
                });

            migrationBuilder.InsertData(
                table: "Manufacturers",
                columns: new[] { "ManufacturerId", "NameManufacturer" },
                values: new object[,]
                {
                    { new Guid("5e37de8c-6df8-43f0-9cf9-c6963a479fc5"), "Apple" },
                    { new Guid("61b5cc85-d737-4ab9-80f1-dc173809797a"), "Samsung" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "IdBuyer1", "IdProduct1" },
                values: new object[,]
                {
                    { new Guid("43a0b53b-8eff-45a7-a0cd-724eaa2eefc3"), new Guid("280ddbaf-77ba-452f-8674-524d838e359a"), new Guid("319d5467-6dec-4cad-8dc8-602ff6fe431a") },
                    { new Guid("815eaeb2-2a27-45f0-b9b8-235bbe1fd08c"), new Guid("ffdc95a1-cc97-4f72-94b3-33b98aa69e23"), new Guid("be68df46-fd31-4b46-80f2-2ad3fa621e82") }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ManufacturerId", "NameModels", "Price" },
                values: new object[,]
                {
                    { new Guid("319d5467-6dec-4cad-8dc8-602ff6fe431a"), new Guid("61b5cc85-d737-4ab9-80f1-dc173809797a"), "S22", 0 },
                    { new Guid("be68df46-fd31-4b46-80f2-2ad3fa621e82"), new Guid("5e37de8c-6df8-43f0-9cf9-c6963a479fc5"), "Iphone x", 0 }
                });

            migrationBuilder.InsertData(
                table: "Storages",
                columns: new[] { "StrorageId", "BuyerID", "Quantity" },
                values: new object[,]
                {
                    { new Guid("26b89402-b58f-4e0f-9095-85abea9c6a08"), new Guid("280ddbaf-77ba-452f-8674-524d838e359a"), 45 },
                    { new Guid("93362477-fbdf-4909-8a17-707e6617f306"), new Guid("ffdc95a1-cc97-4f72-94b3-33b98aa69e23"), 20 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Buyers",
                keyColumn: "BuyerId",
                keyValue: new Guid("280ddbaf-77ba-452f-8674-524d838e359a"));

            migrationBuilder.DeleteData(
                table: "Buyers",
                keyColumn: "BuyerId",
                keyValue: new Guid("ffdc95a1-cc97-4f72-94b3-33b98aa69e23"));

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "ManufacturerId",
                keyValue: new Guid("5e37de8c-6df8-43f0-9cf9-c6963a479fc5"));

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "ManufacturerId",
                keyValue: new Guid("61b5cc85-d737-4ab9-80f1-dc173809797a"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("43a0b53b-8eff-45a7-a0cd-724eaa2eefc3"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("815eaeb2-2a27-45f0-b9b8-235bbe1fd08c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("319d5467-6dec-4cad-8dc8-602ff6fe431a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("be68df46-fd31-4b46-80f2-2ad3fa621e82"));

            migrationBuilder.DeleteData(
                table: "Storages",
                keyColumn: "StrorageId",
                keyValue: new Guid("26b89402-b58f-4e0f-9095-85abea9c6a08"));

            migrationBuilder.DeleteData(
                table: "Storages",
                keyColumn: "StrorageId",
                keyValue: new Guid("93362477-fbdf-4909-8a17-707e6617f306"));
        }
    }
}
