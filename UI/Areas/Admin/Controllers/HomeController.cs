using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Areas.Admin.Models;

namespace UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly UserManager<CustomUser> userManager;
        private readonly SignInManager<CustomUser> signInManager;

        public HomeController(UserManager<CustomUser> userManager, SignInManager<CustomUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {

                var user = await userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("", "Mail yada parola yanlış");
                    return View(model);
                }

                if (!await userManager.IsEmailConfirmedAsync(user))
                {
                    ModelState.AddModelError("", "Bu mail onaylanmamıştır. Lütfen mail box'ınızı kontrol ediniz");
                    return View(model);
                }
                //model.RememberMe
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, true);

                if (result.Succeeded)
                {

                    return RedirectToAction("dashbord", "Dashbord", new { area = "Admin" });
                }           
                else
                {
                   
                    ModelState.AddModelError("", "Mail yada parola yanlış");
                    return View(model);
                }

            }
        }


        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Redirect("~/");
        }

        public IActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                var user = new CustomUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Tc = model.TcKimlik
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

                    return RedirectToAction("Login", "Home");
                }

                ModelState.AddModelError("","Ters giden bişiler oldu");
                return View(model);
            }
        }

        
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
