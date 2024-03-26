using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalks.API.Migrations.NZWalksAuthDb
{
    /// <inheritdoc />
    public partial class Updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "03c8f9d2-ff6e-4afc-9006-622d191040f7",
                column: "NormalizedName",
                value: "READER");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b3571b36-ad34-4ea4-a3f8-464ee992c587",
                column: "NormalizedName",
                value: "WRITER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "03c8f9d2-ff6e-4afc-9006-622d191040f7",
                column: "NormalizedName",
                value: "03c8f9d2-ff6e-4afc-9006-622d191040f7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b3571b36-ad34-4ea4-a3f8-464ee992c587",
                column: "NormalizedName",
                value: "b3571b36-ad34-4ea4-a3f8-464ee992c587");
        }
    }
}
