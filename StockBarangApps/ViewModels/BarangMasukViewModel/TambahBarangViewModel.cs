using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockBarangApps.ViewModels
{
    public class TambahBarangViewModel
    {
        [Required(ErrorMessage = "Nama Barang Harus Diisi")]
        [Display(Name = "Nama Barang")]
        public string NamaBarang { get; set; }

        [Required(ErrorMessage = "Asal Barang Harus Diisi")]
        [Display(Name = "Asal Barang")]
        public string AsalBarang { get; set; }

        [Required(ErrorMessage = "Tanggal Harus Diisi")]
        public DateTime? Tanggal { get; set; }

        [Required(ErrorMessage = "Jumlah Barang Harus Diisi")]
        [Display(Name = "Jumlah Barang")]
        public int? JumlahBarang { get; set; }
    }
}
