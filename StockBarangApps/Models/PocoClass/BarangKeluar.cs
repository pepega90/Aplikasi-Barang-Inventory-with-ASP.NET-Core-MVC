using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockBarangApps.Models
{
    public class BarangKeluar
    {
        public int Id { get; set; }
        public string IdTransaksi { get; set; }
        public string KodeBarang { get; set; }
        public string NamaBarang { get; set; }
        public string Dikirim { get; set; }
        public DateTime? TanggalKeluar { get; set; }
        public int? JumlahBarang { get; set; }
    }
}
