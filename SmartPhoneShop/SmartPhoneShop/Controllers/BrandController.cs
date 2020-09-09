using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SmartPhoneShop.Models;
using SmartPhoneShop.Repository;
using SmartPhoneShop.ViewModels;

namespace SmartPhoneShop.Controllers
{
    public class BrandController : Controller
    {

        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ICategoryRepository categoryRepository;
        private readonly IBrandRepository brandRepository;
        public IProductRepository productRepository { get; }

        public BrandController(IWebHostEnvironment webHostEnvironment,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
           IBrandRepository brandRepository)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.productRepository = productRepository;
            this.brandRepository = brandRepository;
            this.categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        { 
            List<Category> categories = categoryRepository.Gets().ToList();
            List<Brand> brands = brandRepository.Gets().ToList();
            var result = (
                          from c in categories
                          join b in brands on c.CategoryId equals b.CategoryId
                          where c.IsDelete == false && b.IsDelete == false 
                          select (new BrandView()
                          {
                              BrandId = b.BrandId,
                              BrandName = b.BrandName,
                              CategoryName = c.CategoryName,
                              ImagePath = b.ImagePath   
                          })).ToList();

            return View(result);
        }

        public ViewResult Create()
        {
            ViewBag.Categories = categoryRepository.Gets();
            return View();
        }
        [HttpPost]
        public IActionResult Create(BrandCreate model)
        {
            if (ModelState.IsValid)
            {
                var brand = new Brand()
                {
                    BrandName = model.BrandName,
                    IsDelete = false,
                    CategoryId = model.CategoryId
                };
                var fileName = string.Empty;
                if (model.BrandImg != null)
                {
                    string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/Brand");
                    fileName = $"{Guid.NewGuid()}_{model.BrandImg.FileName}";
                    var filePath = Path.Combine(uploadFolder, fileName);
                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        model.BrandImg.CopyTo(fs);
                    }
                }
                brand.ImagePath = fileName;
                brandRepository.Create(brand);
                return RedirectToAction("Index", "Product");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            ViewBag.Categories = categoryRepository.Gets().ToList();
            
            try
            {
                var brand = brandRepository.Get(int.Parse(id));
                if (brand != null && !brand.IsDelete)
                {
                    var edit = new BrandEdit()
                    {
                        BrandId = brand.BrandId,
                        BrandName = brand.BrandName,
                        CategoryId = brand.CategoryId,
                        ImagePath = brand.ImagePath
                    };
                    return View(edit);
                }
                else
                {
                    ViewBag.id = id;
                    return View("~/Views/Error/BrandNotFound.cshtml");
                }
            }
            catch (Exception)
            {
                ViewBag.id = id;
                return View("~/Views/Error/BrandNotFound.cshtml");
            }
        }
        //public ViewResult Edit(int id)
        //{
        //    ViewBag.Categories = categoryRepository.Gets().ToList();
        //    var brand = brandRepository.Get(id);
        //    if (brand == null || brand.IsDelete)
        //    {
        //        ViewBag.id = id;
        //        return View("~/Views/Error/BrandNotFound.cshtml");
        //    }
        //    var brandEdit = new BrandEdit()
        //    {
        //        BrandId = brand.BrandId,
        //        BrandName = brand.BrandName,
        //        CategoryId = brand.CategoryId,
        //        ImagePath = brand.ImagePath
        //    };
        //    return View(brandEdit);
        //}

        [HttpPost]
        public IActionResult Edit(BrandEdit model)
        {
            if (ModelState.IsValid)
            {
                var brand = new Brand()
                {
                    BrandId =model.BrandId,
                    BrandName = model.BrandName,
                    CategoryId = model.CategoryId,
                    ImagePath = model.ImagePath,
                };
                var fileName = string.Empty;
                if (model.Image != null)
                {
                    string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/Brand");
                    fileName = $"{Guid.NewGuid()}_{model.Image.FileName}";
                    var filePath = Path.Combine(uploadFolder, fileName);
                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        model.Image.CopyTo(fs);
                    }
                    brand.ImagePath = fileName;
                    if (!string.IsNullOrEmpty(model.ImagePath))
                    {
                        string delFile = Path.Combine(webHostEnvironment.WebRootPath,
                                            "images/Brand", model.ImagePath);
                        System.IO.File.Delete(delFile);
                    }
                }
                else
                {
                    fileName = model.ImagePath;
                }
                brand.ImagePath = fileName;
               
                var editBrand = brandRepository.Edit(brand);
                if (editBrand != null)
                {
                    return RedirectToAction("Index", "Brand");
                }
            }
            return View();
        }

        public IActionResult Delete(int id)
        {

            if (brandRepository.Delete(id))
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
