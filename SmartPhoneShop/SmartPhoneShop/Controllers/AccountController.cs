using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartPhoneShop.Models;
using SmartPhoneShop.ViewModels;

namespace SmartPhoneShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<Account> userManager;
        private readonly SignInManager<Account> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<Account> userManager, 
                                 IWebHostEnvironment webHostEnvironment,
                                 SignInManager<Account> signInManager,
                                 RoleManager<IdentityRole> roleManager)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }


        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var user = new Account()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    FullName = model.FullName,
                    Gender = model.Gender,
                    Address = model.Address
                };
                var fileName = string.Empty;
                if (model.Image != null)
                {
                    string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/Avatar");
                    fileName = $"{Guid.NewGuid()}_{model.Image.FileName}";
                    var filePath = Path.Combine(uploadFolder, fileName);
                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        model.Image.CopyTo(fs);
                    }
                } 
                else
                {
                    fileName = "~/images/Avatar/nonAvatar.jfif";
                }
                user.ImagePath = fileName;
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Login", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                        userName: model.Email,
                        password: model.PassWord,
                        isPersistent: model.Rememberme,
                        lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await userManager.FindByNameAsync(model.Email);
                    var rolename = await userManager.GetRolesAsync(user);
                    if (rolename.Count>0)
                    {
                        return RedirectToAction("Index","home");
                    }
                    //if (!string.IsNullOrEmpty(model.returnUrl))
                    //{
                    //    return Redirect(model.returnUrl);
                    //}
                    return RedirectToAction("Trangchu", "Home");
                }
                else
                {
                    if (!string.IsNullOrEmpty(model.returnUrl))
                    {
                        return Redirect(model.returnUrl);
                    }
                    ModelState.AddModelError("", "Invalid login attemp");
                }
            };
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }

}

