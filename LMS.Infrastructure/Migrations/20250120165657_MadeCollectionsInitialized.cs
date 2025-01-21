using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MadeCollectionsInitialized : Migration
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

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2eb53f87-cb21-42c3-bedb-6751bfca7832", null, "Admin", "ADMIN" },
                    { "5c29b189-4a49-4e68-8302-d2f430680a1c", null, "Student", "STUDENT" },
                    { "947ce5fe-3b75-44f3-be3e-f53e21c5915f", null, "Teacher", "TEACHER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2eb53f87-cb21-42c3-bedb-6751bfca7832");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5c29b189-4a49-4e68-8302-d2f430680a1c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "947ce5fe-3b75-44f3-be3e-f53e21c5915f");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Courses",
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
                    { "22e53da2-873e-44ef-872e-c36424e5821d", null, "Student", "STUDENT" },
                    { "61e57b8b-e67b-4a45-b773-700fec203703", null, "Admin", "ADMIN" },
                    { "713d3078-9e62-4f0d-be5d-6de192bfdbef", null, "Teacher", "TEACHER" }
                });
        }
    }
}
