using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockBarangApps.Migrations
{
    public partial class AddDatabaseBarangKeluar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BarangKeluar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTransaksi = table.Column<string>(nullable: true),
                    KodeBarang = table.Column<string>(nullable: true),
                    NamaBarang = table.Column<string>(nullable: true),
                    Dikirim = table.Column<string>(nullable: true),
                    TanggalKeluar = table.Column<DateTime>(nullable: true),
                    JumlahBarang = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarangKeluar", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BarangKeluar");
        }
    }
}
