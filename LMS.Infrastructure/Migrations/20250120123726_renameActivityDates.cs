using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class renameActivityDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "StartTime",
                table: "Activities",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "Activities",
                newName: "EndDate");

            migrationBuilder.RenameIndex(
                name: "IX_Activities_ModuleId_StartTime_EndTime",
                table: "Activities",
                newName: "IX_Activities_ModuleId_StartDate_EndDate");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9c552325-955d-4f01-a71e-b088cbd584e3", null, "Admin", "ADMIN" },
                    { "9f38782a-6063-4e76-8e37-35c5ffc58512", null, "Student", "STUDENT" },
                    { "a094a624-658a-4ed6-aa96-7de8b3def65e", null, "Teacher", "TEACHER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c552325-955d-4f01-a71e-b088cbd584e3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f38782a-6063-4e76-8e37-35c5ffc58512");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a094a624-658a-4ed6-aa96-7de8b3def65e");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Activities",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Activities",
                newName: "EndTime");

            migrationBuilder.RenameIndex(
                name: "IX_Activities_ModuleId_StartDate_EndDate",
                table: "Activities",
                newName: "IX_Activities_ModuleId_StartTime_EndTime");

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
    }
}
