using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShopSmarfone.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4db222cd-b41b-4ec3-8fb0-666d110c2123", "ebc72753-b156-4fa8-8f63-881f0a17bf45", "User", "USER" },
                    { "e2937c2c-c2f5-4e0e-bc9c-677a0aad79df", "9c807914-4b4b-4821-8c1a-54c5c2504508", "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4db222cd-b41b-4ec3-8fb0-666d110c2123");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2937c2c-c2f5-4e0e-bc9c-677a0aad79df");
        }
    }
}
