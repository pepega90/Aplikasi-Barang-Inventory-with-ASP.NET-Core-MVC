using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockBarangApps.Models
{
    public interface IBarangMasukRepository
    {
        IEnumerable<BarangMasuk> GetAllBarang();

        BarangMasuk BuatBarang(BarangMasuk barang);

        void HapusMasuk(int id);

        BarangMasuk UpdateBarang(BarangMasuk barang);

        BarangMasuk GetSingleBarang(int id);
    }
}
