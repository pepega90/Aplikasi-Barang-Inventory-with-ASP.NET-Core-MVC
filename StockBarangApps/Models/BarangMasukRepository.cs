using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace StockBarangApps.Models
{
    public class BarangMasukRepository : IBarangMasukRepository
    {
        private readonly AppDbContext context;

        public BarangMasukRepository(AppDbContext context)
        {
            this.context = context;
        }

        public BarangMasuk BuatBarang(BarangMasuk barang)
        {
            context.BarangMasuk.Add(barang);
            context.SaveChanges();
            return barang;
        }

        public IEnumerable<BarangMasuk> GetAllBarang()
        {
            return context.BarangMasuk;
        }

        public BarangMasuk GetSingleBarang(int id)
        {
            return context.BarangMasuk.Find(id);
        }

        public void HapusMasuk(int id)
        {
            var item = context.BarangMasuk.Find(id);
            context.BarangMasuk.Remove(item);
            context.SaveChanges();
        }

        public BarangMasuk UpdateBarang(BarangMasuk barang)
        {
            var editBarang = context.BarangMasuk.Attach(barang);
            editBarang.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return barang;
        }
    }
}
