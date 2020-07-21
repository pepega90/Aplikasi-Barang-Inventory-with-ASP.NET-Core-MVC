using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockBarangApps.ViewModels
{
    public class GantiPasswordViewModel
    {
        [Required(ErrorMessage = "Masukkan Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Masukkan Password Baru")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Konfirmasi Password Harus Diisi")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Konfirmasi Password Tidak Sama Dengan Password!")]
        public string KonfirmasiPassword { get; set; }

    }
}
