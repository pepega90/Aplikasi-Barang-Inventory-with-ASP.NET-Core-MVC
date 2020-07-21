using Microsoft.EntityFrameworkCore.Migrations;

namespace StockBarangApps.Migrations
{
    public partial class AddAsalBarangProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AsalBarang",
                table: "BarangMasuk",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AsalBarang",
                table: "BarangMasuk");
        }
    }
}
