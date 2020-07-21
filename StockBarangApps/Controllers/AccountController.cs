using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockBarangApps.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace StockBarangApps.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
                                SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Daftar()
        {
            if(signInManager.IsSignedIn(User) && (User.IsInRole("Admin") || User.IsInRole("Manager")))
            {
                return RedirectToAction("Beranda", "Admin");
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Daftar(RegisViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                await userManager.AddToRoleAsync(user, "Admin");

                var userCreated = await userManager.CreateAsync(user, model.Password);

                if (userCreated.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Beranda", "Admin");
                }

                foreach (var err in userCreated.Errors)
                {
                    // Menambahkan error dengan method AddModelError()
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {

            if (signInManager.IsSignedIn(User) && (User.IsInRole("Admin") || User.IsInRole("Manager")))
            {
                return RedirectToAction("Beranda", "Admin");
            }

            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email,
                    model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Beranda", "Admin");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid E-Mail or Password");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }


        [HttpGet]
        public IActionResult GantiPassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> GantiPassword(GantiPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);

                var userChangePassword = 
                    await userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);

                if (!userChangePassword.Succeeded)
                {
                    foreach (var error in userChangePassword.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                await signInManager.RefreshSignInAsync(user);
                return View("KonfirmasiGantiPassword");

            }
            return View(model);
        }
    }
}
