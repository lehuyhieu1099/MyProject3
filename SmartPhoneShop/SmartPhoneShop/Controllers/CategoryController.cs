using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SmartPhoneShop.Models;
using SmartPhoneShop.ViewModels;

namespace SmartPhoneShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ICategoryRepository categoryRepository;
        public IProductRepository productRepository { get; }

        public CategoryController(IWebHostEnvironment webHostEnvironment,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var Categories = categoryRepository.Gets().ToList();
            return View(Categories);
        }
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CategoryCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category()
                {
                    CategoryName = model.CategoryName,
                    IsDelete = false
                };
                var fileName = string.Empty;
                if (model.CategoryImg != null)
                {
                    string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/Category");
                    fileName = $"{Guid.NewGuid()}_{model.CategoryImg.FileName}";
                    var filePath = Path.Combine(uploadFolder, fileName);
                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        model.CategoryImg.CopyTo(fs);
                    }
                }
                category.ImagePath = fileName;
                categoryRepository.Create(category);
                ViewBag.Categories = categoryRepository.Gets();
                return RedirectToAction("Index", "Category");
            }
            ViewBag.Categories = categoryRepository.Gets();
            return View();
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            try
            {
                var category = categoryRepository.Get(int.Parse(id));
                if (category != null && !category.IsDelete)
                {
                    var edit = new CategoryEditViewModel()
                    {
                        ImagePath = category.ImagePath,
                        CategoryName = category.CategoryName,
                        CategoryId = category.CategoryId
                    };
                    return View(edit);
                }
                else
                {
                    ViewBag.id = id;
                    return View("~/Views/Error/CategoryNotFound.cshtml");
                }
            }
            catch (Exception)
            {
                ViewBag.id = id;
                return View("~/Views/Error/CategoryNotFound.cshtml");
            }
        }

        [HttpPost]
        public IActionResult Edit(CategoryEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category()
                {
                    CategoryName = model.CategoryName,
                    CategoryId = model.CategoryId,
                    ImagePath = model.ImagePath
                };
                var fileName = string.Empty;
                if (model.Image != null)
                {
                    string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/Category");
                    fileName = $"{Guid.NewGuid()}_{model.Image.FileName}";
                    var filePath = Path.Combine(uploadFolder, fileName);
                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        model.Image.CopyTo(fs);
                    }
                    category.ImagePath = fileName;
                    if (!string.IsNullOrEmpty(model.ImagePath))
                    {
                        string delFile = Path.Combine(webHostEnvironment.WebRootPath,
                                            "images/Category", model.ImagePath);
                        System.IO.File.Delete(delFile);
                    }
                }
                else
                {
                    fileName = model.ImagePath;
                }
                category.ImagePath = fileName;
                var editEmp = categoryRepository.Edit(category);
                if (editEmp != null)
                {
                    return RedirectToAction("Index", "Category");
                }
            }

            return View();
        }
     
        public IActionResult Delete(int id)
        {
            
            if (categoryRepository.Delete(id))
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
