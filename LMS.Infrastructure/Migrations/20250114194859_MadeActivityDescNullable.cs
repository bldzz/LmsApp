using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MadeActivityDescNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1b5d159c-2f76-4847-8e73-f038a6c0c63f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20759171-c867-4a91-ab83-7c5045eadc9e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b888c39-d0da-4135-ab30-40cba9a9a7de");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1217fb71-b7c4-4387-a6c6-4213d80ce977", null, "Admin", "ADMIN" },
                    { "8d26a735-c5e8-49b5-945e-a7b861a20555", null, "Teacher", "TEACHER" },
                    { "95b0b2aa-f320-49bf-9da3-3bcfa75504ec", null, "Student", "STUDENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1217fb71-b7c4-4387-a6c6-4213d80ce977");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d26a735-c5e8-49b5-945e-a7b861a20555");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95b0b2aa-f320-49bf-9da3-3bcfa75504ec");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Activities",
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
                    { "1b5d159c-2f76-4847-8e73-f038a6c0c63f", null, "Teacher", "TEACHER" },
                    { "20759171-c867-4a91-ab83-7c5045eadc9e", null, "Admin", "ADMIN" },
                    { "8b888c39-d0da-4135-ab30-40cba9a9a7de", null, "Student", "STUDENT" }
                });
        }
    }
}
