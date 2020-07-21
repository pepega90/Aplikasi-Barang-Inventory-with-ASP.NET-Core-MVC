using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualBasic;
using StockBarangApps.Models;
using StockBarangApps.Utility;
using StockBarangApps.ViewModels;
using StockBarangApps.ViewModels.UserViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StockBarangApps.Controllers
{

    public class AdminController : Controller
    {
        private readonly IBarangMasukRepository barangMasukRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly AppDbContext appDbContext;
        private readonly IBarangKeluarRepository barangKeluarRepository;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IDataProtector protector;
        private static int idBarang;

        public AdminController(IBarangMasukRepository barangMasukRepository,
                                UserManager<IdentityUser> userManager,
                                SignInManager<IdentityUser> signInManager,
                                IDataProtectionProvider dataProtectionProvider,
                                DataProtectionPurposeString dataProtectionPurposeString,
                                AppDbContext appDbContext,
                                IBarangKeluarRepository barangKeluarRepository,
                                RoleManager<IdentityRole> roleManager)
        {
            this.barangMasukRepository = barangMasukRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.appDbContext = appDbContext;
            this.barangKeluarRepository = barangKeluarRepository;
            this.roleManager = roleManager;
            this.protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeString.BarangRouteValue);
        }

        // GET: /<controller>/
        public IActionResult Beranda()
        {
            return View();
        }

        public async Task<IActionResult> StockBarang(int page = 1)
        {
            return View(await PaginatedList<BarangMasuk>.CreateAsync(appDbContext.BarangMasuk, page, 5));
        }

    
        public IActionResult ListUser()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            if(user == null)
            {
                ViewBag.ErrMessage = $"Maaf, User dengan id = {Id} tidak ditemukan";
                return View("Not Found");
            }
            var roles = await userManager.GetRolesAsync(user);
            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = roles.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.Id);

                if(user == null)
                {
                    ViewBag.ErrMessage = $"Maaf, User dengan id = {model.Id} tidak ditemukan";
                    return View("Not Found");
                }

                user.UserName = model.UserName;
                user.Email = model.Email;

                var updateUser = await userManager.UpdateAsync(user);

                if (updateUser.Succeeded)
                {
                    return RedirectToAction("ListUser");
                }
                foreach (var error in updateUser.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddUser(TambahUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var createdUser = await userManager.CreateAsync(user, model.Password);

                if (createdUser.Succeeded)
                {
                    if (signInManager.IsSignedIn(User))
                    {
                        return RedirectToAction("ListUser");
                    }
                }

                foreach (var error in createdUser.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> HapusUser(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);

            if(user == null)
            {
                ViewBag.ErrMessage = $"Maaf, User dengan id = {Id} tidak ditemukan";
                return View("Not Found");
            }

            var deletedUser = await userManager.DeleteAsync(user);

            if (deletedUser.Succeeded)
            {
                return RedirectToAction("ListUser");
            }

            foreach (var error in deletedUser.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View("ListUser");

        }

        [HttpGet]
        public IActionResult AddBarang()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBarang(TambahBarangViewModel model)
        {
            if (ModelState.IsValid)
            {

                BarangMasuk barang = new BarangMasuk()
                {
                    NamaBarang = model.NamaBarang,
                    AsalBarang = model.AsalBarang,
                    KodeBarang = "A" + DateTime.Now.Year + Guid.NewGuid().ToString().Substring(3, 3),
                    Tanggal = model.Tanggal,
                    JumlahBarang = model.JumlahBarang
                };


                barangMasukRepository.BuatBarang(barang);
                return RedirectToAction("ListBarangMasuk");
            }
            return View(model);
        }

        public IActionResult HapusMasuk(int id)
        {
            barangMasukRepository.HapusMasuk(id);
            return RedirectToAction("ListBarangMasuk");
        }

        public IActionResult HapusKeluar(int id)
        {
            var barangKeluar = appDbContext.BarangKeluar.Find(id);
            // Jangan pernah melakukan savechanges di dalam foreach. maka dari itu disini kita mengubah
            // appDbContext.BarangMasuk menjadi List untuk mendapatkan barang barang masuk sesuai kondisi
            var item = appDbContext.BarangMasuk.ToList().Find(e => e.KodeBarang == barangKeluar.KodeBarang);

            item.JumlahBarang = item.JumlahBarang + barangKeluar.JumlahBarang;
            barangMasukRepository.UpdateBarang(item);

            barangKeluarRepository.HapusKeluar(id);
            return RedirectToAction("ListBarangKeluar");

        }

        [HttpGet]
        public IActionResult EditBarang(int id)
        {

            BarangMasuk barang = barangMasukRepository.GetSingleBarang(id);

            if(barang == null)
            {
                ViewBag.ErrMessage = $"Maaf, Barang Masuk dengan id = {id} tidak ditemukan";
                return View("Not Found");
            }

            EditBarangViewModel editBarangViewModel = new EditBarangViewModel
            {
                Id = id,
                NamaBarang = barang.NamaBarang,
                AsalBarang = barang.AsalBarang,
                Tanggal = barang.Tanggal,
                JumlahBarang = barang.JumlahBarang
            };

            return View(editBarangViewModel);
        }

        [HttpPost]
        public IActionResult EditBarang(EditBarangViewModel model)
        {
            if (ModelState.IsValid)
            {
                BarangMasuk barang = barangMasukRepository.GetSingleBarang(Convert.ToInt32(model.Id));

                if(barang == null)
                {
                    ViewBag.ErrMessage = $"Maaf, Barang Masuk dengan id = {model.Id} tidak ditemukan";
                    return View("Not Found");
                }

                barang.NamaBarang = model.NamaBarang;
                barang.AsalBarang = model.AsalBarang;
                barang.Tanggal = model.Tanggal;
                barang.JumlahBarang = model.JumlahBarang;

                BarangMasuk editedBarang = barangMasukRepository.UpdateBarang(barang);

                return RedirectToAction("ListBarangMasuk");
            }
            return View(model);
        }

        public async Task<IActionResult> ListBarangMasuk(int page = 1)
        {
            return View(await PaginatedList<BarangMasuk>.CreateAsync(appDbContext.BarangMasuk, page, 4));
        }

        // Section Barang Keluar

        public async Task<IActionResult> ListBarangKeluar(int page = 1)
        {
            return View(await PaginatedList<BarangKeluar>.CreateAsync(appDbContext.BarangKeluar, page, 4));
        }

        [HttpGet]
        public IActionResult KirimBarang()
        {
            var model = new KirimBarangViewModel();
            model.KodeBarang = appDbContext.BarangMasuk
                                .Select(a => new SelectListItem()
                                {
                                    Value = a.Id.ToString(),
                                    Text = a.KodeBarang
                                }).ToList();

            return View(model);
        }
        [HttpPost]
        public IActionResult KirimBarang(KirimBarangViewModel model)
        {
            if (ModelState.IsValid)
            {

                var barangMasuk = appDbContext.BarangMasuk.Find(idBarang);
                barangMasuk.JumlahBarang = barangMasuk.JumlahBarang - model.JumlahBarang;
                barangMasukRepository.UpdateBarang(barangMasuk);

                BarangKeluar barangKeluar = new BarangKeluar
                {
                    IdTransaksi = "TB" + "-" + DateTime.Now.Year + Guid.NewGuid().ToString().Substring(3, 2),
                    Dikirim = model.Dikirim,
                    NamaBarang = model.NamaBarang,
                    KodeBarang = barangMasuk.KodeBarang,
                    TanggalKeluar = DateTime.Today,
                    JumlahBarang = model.JumlahBarang
                };

                barangKeluarRepository.Save(barangKeluar);
                return RedirectToAction("ListBarangKeluar");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            ViewBag.userId = userId;

            var user = await userManager.FindByIdAsync(userId);

            if(user == null)
            {
                ViewBag.ErrMessage = $"Maaf, User dengan id = {userId} tidak ditemukan";
                return View("Not Found");
            }

            var model = new List<UserRoleViewModel>();

            foreach (var role in roleManager.Roles.ToList())
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                if (!await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(List<UserRoleViewModel> model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            // dapatkan semua roles dari variabel user tadi
            var roles = await userManager.GetRolesAsync(user);
            // hapus semua role yang ada user tersebut
            var result = await userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }

            // tambahkan role jika user tersebut memiliki properti isSelected bernilai true
            // lalu pilih rolename properti untuk menampilkan ke view
            result = await userManager.AddToRolesAsync(user,
                model.Where(e => e.IsSelected).Select(x => x.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }

            return RedirectToAction("EditUser", new { Id = userId });
        }

        public IActionResult CetakBarangMasuk()
        {
            var model = appDbContext.BarangMasuk;
            return View(model);
        }

        public IActionResult CetakBarangKeluar()
        {
            var model = appDbContext.BarangKeluar;
            return View(model);
        }

        // Function Untuk mendapatkan barang sesuai id
        public IActionResult GetData(int kode)
        {
            var barang = appDbContext.BarangMasuk.Find(kode);
            idBarang = barang.Id;
            return Json(barang);
        }

        // Function Untuk Mengdekripsi Route id
        //private int Decrypted(string enkripsiId)
        //{
        //    string hasilEnkripsi = protector.Unprotect(enkripsiId);
        //    int realId = Convert.ToInt32(hasilEnkripsi);
        //    return realId;
        //}

    }
}
