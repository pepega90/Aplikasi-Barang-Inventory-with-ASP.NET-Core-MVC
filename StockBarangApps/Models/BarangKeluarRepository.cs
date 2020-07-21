using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockBarangApps.Models
{
    public class BarangKeluarRepository : IBarangKeluarRepository
    {
        private readonly AppDbContext context;

        public BarangKeluarRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void HapusKeluar(int id)
        {
            var barangKeluar = context.BarangKeluar.Find(id);
            context.BarangKeluar.Remove(barangKeluar);

            context.SaveChanges();
        }

        public BarangKeluar Save(BarangKeluar barang)
        {
            context.BarangKeluar.Add(barang);
            context.SaveChanges();
            return barang;
        }
    }
}
