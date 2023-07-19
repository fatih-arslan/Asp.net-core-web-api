using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class AddRolesToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "59c4b1a2-ebb5-4fb6-bd5a-f2bef3d389e8", "4f73b560-89f5-44ed-a7c5-b63b09ea6639", "Editor", "EDITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "67de411f-bb97-4f3c-8623-ee87fc4d77b8", "62356948-2ac3-47b5-a46e-102237422e0c", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9b7035a9-fcc6-4ee6-ac7b-c82a6a1ab1e2", "ed042c6d-636a-40f2-a3d8-c5ce2c1535cc", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "59c4b1a2-ebb5-4fb6-bd5a-f2bef3d389e8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67de411f-bb97-4f3c-8623-ee87fc4d77b8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b7035a9-fcc6-4ee6-ac7b-c82a6a1ab1e2");
        }
    }
}
