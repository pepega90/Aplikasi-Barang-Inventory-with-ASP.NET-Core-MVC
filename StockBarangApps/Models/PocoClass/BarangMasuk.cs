using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockBarangApps.Models
{
    public class BarangMasuk
    {
        public int Id { get; set; }
        public string KodeBarang { get; set; }
        public string NamaBarang { get; set; }
        public string AsalBarang { get; set; }
        public DateTime? Tanggal { get; set; }
        public int? JumlahBarang { get; set; }

    }
}
