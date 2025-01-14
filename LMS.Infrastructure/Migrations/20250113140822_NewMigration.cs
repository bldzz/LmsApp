using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "46bd41d2-6153-43ad-820b-774b095e5895");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a988b02a-fc81-44ea-8c32-5aa7288b6c51");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd2283a9-791f-4a1e-ad5f-71eee1eb7ec6");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Courses");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "46bd41d2-6153-43ad-820b-774b095e5895", null, "Admin", "ADMIN" },
                    { "a988b02a-fc81-44ea-8c32-5aa7288b6c51", null, "Teacher", "TEACHER" },
                    { "dd2283a9-791f-4a1e-ad5f-71eee1eb7ec6", null, "Student", "STUDENT" }
                });
        }
    }
}
