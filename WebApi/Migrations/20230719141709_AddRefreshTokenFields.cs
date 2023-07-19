using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class AddRefreshTokenFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1e8e91ce-f50c-4dc9-8da1-bd1852bf7621", "c2c8632f-7c2a-4afa-90aa-15d47ccf22a2", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5ee2a0e3-c8a4-43ab-8909-c1637f80cd73", "9f8ffa82-8038-42d2-84ab-7858e2062c1e", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d29c8974-54f4-4f47-8bb8-7c00d9d05ab8", "839d04b8-620c-4af9-ba61-07796c5061ec", "Editor", "EDITOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1e8e91ce-f50c-4dc9-8da1-bd1852bf7621");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ee2a0e3-c8a4-43ab-8909-c1637f80cd73");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d29c8974-54f4-4f47-8bb8-7c00d9d05ab8");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

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
    }
}
