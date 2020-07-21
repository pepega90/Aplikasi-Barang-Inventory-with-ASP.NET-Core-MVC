using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockBarangApps.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Username Tidak Boleh Kosong")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "E-Mail Tidak Boleh Kosong!")]
        public string Email { get; set; }

        public List<string> Roles { get; set; } = new List<string>();
    }
}
