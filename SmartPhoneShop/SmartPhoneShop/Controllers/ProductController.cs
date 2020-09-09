using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SmartPhoneShop.Models;
using SmartPhoneShop.Repository;
using SmartPhoneShop.ViewModels;

namespace SmartPhoneShop.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository productRepository;
        private ICategoryRepository categoryRepository;
        private IBrandRepository brandRepository;
        private IWebHostEnvironment webHostEnvironment;

        public ProductController(IProductRepository productRepository,
                                ICategoryRepository categoryRepository,
                                IBrandRepository brandRepository,
                                IWebHostEnvironment webHostEnvironment)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.brandRepository = brandRepository;
        }
        [HttpGet]
        [Route("Brand/{id}")]
        public JsonResult ShowBrand(int id)
        {
            List<Brand> brands = brandRepository.Gets().ToList();
            List<Brand> brandbyId = new List<Brand>();
            foreach (Brand brand in brands)
            {
                if (brand.CategoryId == id)
                {
                    brandbyId.Add(brand);
                }
            }
            return Json(new { brandbyId });
        }
        [HttpGet]
        public ViewResult Create()
        {
            ViewBag.Categories = categoryRepository.Gets().ToList();
            ViewBag.Brands = brandRepository.Gets().ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductCreateViewModel model)
        {

            if (ModelState.IsValid)
            {
                var product = new Product()
                {
                    ProductName = model.ProductName,
                    BrandId = model.BrandId,
                    Stock = model.Stock,
                    Description = model.Description,
                    Price = model.Price,
                    CategoryId = model.CategoryId,
                    IsDelete = false
                };

                var fileName = string.Empty;
                if (model.Image != null)
                {
                    string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/Product");
                    fileName = $"{Guid.NewGuid()}_{model.Image.FileName}";
                    var filePath = Path.Combine(uploadFolder, fileName);
                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        model.Image.CopyTo(fs);
                    }
                }
                product.PathImage = fileName;
                productRepository.Create(product);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public IActionResult Index()
        {
            List<Product> products = productRepository.Gets().ToList();
            List<Category> categories = categoryRepository.Gets().ToList();
            List<Brand> brands = brandRepository.Gets().ToList();
            var result = (from p in products
                         join c in categories on p.CategoryId equals c.CategoryId
                         join b in brands on p.BrandId equals b.BrandId
                         where c.IsDelete == false && b.IsDelete == false && p.IsDelete == false
                         select (new ProductViewModel()
                         {
                             ProductId = p.ProductId,
                             ProductName = p.ProductName,
                             Price = p.Price,
                             Stock = p.Stock,
                             BrandName = b.BrandName,
                             CategoryName = c.CategoryName,
                             ImagePath = p.PathImage
                         })).ToList();

            return View(result);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            ViewBag.Categories = categoryRepository.Gets().ToList();
            ViewBag.Brands = brandRepository.Gets().ToList();
            try
            {
                var product = productRepository.Get(int.Parse(id));
                if (product != null && !product.IsDelete)
                {
                        var edit = new ProductEditViewModel()
                        {
                            ProductId = product.ProductId,
                            ProductName = product.ProductName,
                            CategoryId = product.CategoryId,
                            BrandId = product.BrandId,
                            Description = product.Description,
                            Price = product.Price,
                            Stock = product.Stock,
                            PathImage = product.PathImage
                        };
                        return View(edit); 
                }
                else
                {
                    ViewBag.id = id;
                    return View("~/Views/Error/ProductNotFound.cshtml");
                }
            }
            catch(Exception)
            {
                ViewBag.id = id;
                return View("~/Views/Error/ProductNotFound.cshtml");
            }
        }

        [HttpPost]
        public IActionResult Edit(ProductEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product()
                {
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    CategoryId = model.CategoryId,
                    BrandId = model.BrandId,
                    Description = model.Description,
                    Price = model.Price,
                    Stock = model.Stock,
                    PathImage = model.PathImage
                };
                var fileName = string.Empty;
                if (model.Image != null)
                {
                    string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/Product");
                    fileName = $"{Guid.NewGuid()}_{model.Image.FileName}";
                    var filePath = Path.Combine(uploadFolder, fileName);
                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        model.Image.CopyTo(fs);
                    }
                    product.PathImage = fileName;
                    if (!string.IsNullOrEmpty(model.PathImage))
                    {
                        string delFile = Path.Combine(webHostEnvironment.WebRootPath,
                                            "images/Product", model.PathImage);
                        System.IO.File.Delete(delFile);
                    }
                }
                var editEmp = productRepository.Edit(product);
                if (editEmp != null)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            if (productRepository.Delete(id))
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
