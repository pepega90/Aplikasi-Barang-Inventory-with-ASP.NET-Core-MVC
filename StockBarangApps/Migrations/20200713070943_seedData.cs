using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockBarangApps.Migrations
{
    public partial class seedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BarangMasuk",
                columns: new[] { "Id", "JumlahBarang", "KodeBarang", "NamaBarang", "Tanggal" },
                values: new object[] { 1, 123, "A001", "Cireng", new DateTime(2020, 7, 13, 14, 9, 42, 534, DateTimeKind.Local).AddTicks(7079) });

            migrationBuilder.InsertData(
                table: "BarangMasuk",
                columns: new[] { "Id", "JumlahBarang", "KodeBarang", "NamaBarang", "Tanggal" },
                values: new object[] { 2, 200, "A002", "Tepung Terigu", new DateTime(2020, 7, 13, 14, 9, 42, 537, DateTimeKind.Local).AddTicks(7081) });

            migrationBuilder.InsertData(
                table: "BarangMasuk",
                columns: new[] { "Id", "JumlahBarang", "KodeBarang", "NamaBarang", "Tanggal" },
                values: new object[] { 3, 90, "A003", "Fried Chicken", new DateTime(2020, 7, 13, 14, 9, 42, 537, DateTimeKind.Local).AddTicks(7081) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BarangMasuk",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BarangMasuk",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BarangMasuk",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
