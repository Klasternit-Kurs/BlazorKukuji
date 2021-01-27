using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorKukuji.Server.Migrations
{
    public partial class seededRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3f27834a-a7c4-45cd-b819-daa14d90dfcf", "fvew4tg", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f27834a-a7c4-45cd-b819-daa14d90dfcf");
        }
    }
}
