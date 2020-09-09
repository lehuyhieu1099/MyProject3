using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartPhoneShop.Models;
using SmartPhoneShop.ViewModels;

namespace SmartPhoneShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<Account> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public SignInManager<Account> signInManager { get; }

       
        private readonly IProductRepository productRepository;
        public  ICategoryRepository categoryRepository { get; }
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(IProductRepository productRepository,
                                ICategoryRepository categoryRepository,
                                IWebHostEnvironment webHostEnvironment,
                                UserManager<Account> userManager,
                                SignInManager<Account> signInManager,
                                  RoleManager<IdentityRole> roleManager)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository; 
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;

        }
 
        [AllowAnonymous]
         public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserByYourself model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.FullName = model.FullName;
                    user.Gender = model.Gender;
                    user.Address = model.Address;
                    user.Email = model.Email;
                    user.Company = model.Company;
                    user.DTW = model.DTW;
                    user.DoB = model.DoB;
                    user.Facebook = model.Facebook;
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
                        user.ImagePath = fileName;
                        if (!string.IsNullOrEmpty(model.ImagePath))
                        {
                            string delFile = Path.Combine(webHostEnvironment.WebRootPath,
                                                "images/Avatar", model.ImagePath);
                            System.IO.File.Delete(delFile);
                        }
                    }
                    else
                    {
                        fileName = model.ImagePath;
                    }
                    user.ImagePath = fileName;

                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                       
                        return RedirectToAction("index", "Home");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
    }
}
