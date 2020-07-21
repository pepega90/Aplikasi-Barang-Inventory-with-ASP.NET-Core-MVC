using Microsoft.EntityFrameworkCore.Migrations;

namespace StockBarangApps.Migrations
{
    public partial class removeUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408d4648-50f1-44b5-acad-0f0d7176f662");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "408d4648-50f1-44b5-acad-0f0d7176f662", 0, "736a0ad6-7fe4-493f-9ee9-762ca1c49bee", "asuarif5@gmail.com", false, false, null, null, null, "devikinal90", null, false, "e7bd9ac6-4aa0-4542-a0f2-fd6dfebf5a3b", false, "Aji Mustofa" });
        }
    }
}
