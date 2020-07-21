using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockBarangApps.Models
{
    public interface IBarangKeluarRepository
    {
        BarangKeluar Save(BarangKeluar barang);

        void HapusKeluar(int id);
    }
}
