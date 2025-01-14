using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MadeDescriptionNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f76a69d-c0f5-460b-b134-bbcdd220b05c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "396f0ac1-7ef0-4637-865c-52d81429950b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b361ba5-0688-46d5-8438-6e7e06760fc6");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Modules",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "72d1e6fb-98c6-4e1d-9ab7-a37fdd70d9b8", null, "Student", "STUDENT" },
                    { "a1998b0f-c9e5-4f16-b149-ba0f0dc4632a", null, "Teacher", "TEACHER" },
                    { "ba944d94-06df-4c18-8a94-2ffb91dca35f", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72d1e6fb-98c6-4e1d-9ab7-a37fdd70d9b8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1998b0f-c9e5-4f16-b149-ba0f0dc4632a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba944d94-06df-4c18-8a94-2ffb91dca35f");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Modules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2f76a69d-c0f5-460b-b134-bbcdd220b05c", null, "Admin", "ADMIN" },
                    { "396f0ac1-7ef0-4637-865c-52d81429950b", null, "Teacher", "TEACHER" },
                    { "7b361ba5-0688-46d5-8438-6e7e06760fc6", null, "Student", "STUDENT" }
                });
        }
    }
}
