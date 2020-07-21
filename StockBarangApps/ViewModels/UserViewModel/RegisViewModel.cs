using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockBarangApps.ViewModels
{
    public class RegisViewModel
    {

        [EmailAddress]
        [Required(ErrorMessage = "Email Harus Diisi!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Harus Diisi!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password Tidak Sama!")]
        [Display(Name = "Konfirmasi Password")]
        public string KonfirmasiPassword { get; set; }
    }
}
