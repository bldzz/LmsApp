using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MadeNavPropsNullableInModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d00610f-72df-44b7-8937-abdacd4d2fc5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42eda7d8-08e6-4877-896c-ee18a953b543");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e4a7563-f000-4f6b-b4e5-7a665fe6885e");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1d00610f-72df-44b7-8937-abdacd4d2fc5", null, "Student", "STUDENT" },
                    { "42eda7d8-08e6-4877-896c-ee18a953b543", null, "Admin", "ADMIN" },
                    { "6e4a7563-f000-4f6b-b4e5-7a665fe6885e", null, "Teacher", "TEACHER" }
                });
        }
    }
}
