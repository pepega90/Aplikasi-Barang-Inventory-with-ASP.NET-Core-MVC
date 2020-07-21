using Microsoft.AspNetCore.Mvc.Rendering;
using StockBarangApps.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockBarangApps.ViewModels
{
    public class KirimBarangViewModel
    {

        public string IdTransaksi { get; set; }
        public List<SelectListItem> KodeBarang { get; set; }
        public string NamaBarang { get; set; }
        [Required(ErrorMessage = "Tujuan Pengiriman Harus Diisi")]
        [Display(Name = "Tujuan Barang")]
        public string Dikirim { get; set; }
        [Display(Name = "Tanggal Keluar")]
        public DateTime? TanggalKeluar { get; set; }
        [Required(ErrorMessage = "Jumlah Barang Harus Diisi")]
        [Display(Name = "Jumlah Barang")]
        public int? JumlahBarang { get; set; }
    }
}
