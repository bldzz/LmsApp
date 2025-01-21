using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22e53da2-873e-44ef-872e-c36424e5821d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61e57b8b-e67b-4a45-b773-700fec203703");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "713d3078-9e62-4f0d-be5d-6de192bfdbef");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "17ec3249-1095-4531-86a4-72e68d6620cc", null, "Teacher", "TEACHER" },
                    { "91783140-84c8-4e74-ae5d-baeb4facd33e", null, "Student", "STUDENT" },
                    { "ec0fa1ed-98f2-4d7c-9f50-3243949dc45d", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "17ec3249-1095-4531-86a4-72e68d6620cc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "91783140-84c8-4e74-ae5d-baeb4facd33e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec0fa1ed-98f2-4d7c-9f50-3243949dc45d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "22e53da2-873e-44ef-872e-c36424e5821d", null, "Student", "STUDENT" },
                    { "61e57b8b-e67b-4a45-b773-700fec203703", null, "Admin", "ADMIN" },
                    { "713d3078-9e62-4f0d-be5d-6de192bfdbef", null, "Teacher", "TEACHER" }
                });
        }
    }
}
