using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class renameActivityName : Migration
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

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Activities",
                newName: "ActivityName");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6cc1f921-8de4-40af-b443-5656a7540bfd", null, "Teacher", "TEACHER" },
                    { "92bfca65-079d-441d-a288-72e68b49747c", null, "Student", "STUDENT" },
                    { "f3da0269-958d-4c00-b968-c69fad47c578", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6cc1f921-8de4-40af-b443-5656a7540bfd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "92bfca65-079d-441d-a288-72e68b49747c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f3da0269-958d-4c00-b968-c69fad47c578");

            migrationBuilder.RenameColumn(
                name: "ActivityName",
                table: "Activities",
                newName: "Name");

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
