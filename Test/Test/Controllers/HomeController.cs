using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Test.Models;
using Test.ViewModel;

namespace Test.Controllers
{
    public class HomeController : Controller
    {

        private readonly TestDbContext testDbContext;
        public HomeController(TestDbContext testDbContext)
        {
            this.testDbContext = testDbContext;
        }
        public IActionResult ShowUser()
        {
            var users = testDbContext.Users.ToList();
            var provinces = testDbContext.Provinces.ToList();
            var districts = testDbContext.Districts.ToList();
            var wards = testDbContext.Wards.ToList();
            var data = (from u in users
                        join p in provinces on u.ProvinceId equals p.ProvinceId
                        join d in districts on u.DistrictId equals d.DistrictId
                        join w in wards on u.WardId equals w.WardId
                        select (new UserView()
                        {
                            UserId = u.UserId,
                            FullName = u.FullName,
                            WardName = $"{w.Prefix} {w.Name}",
                            Address = u.Address,
                            DistrictName = $"{d.Prefix} {d.Name}",
                            Email = u.Email,
                            ProvinceName = p.Name,
                            PhoneNumber = u.PhoneNumber
                        })).ToList();
            return View(data);

        }


        public IActionResult Index()
        {
            return View();

        }
        [HttpGet]
        [Route("~/Huyen/{id}")]
        public JsonResult ShowHuyen(int id)
        {
            List<District> diss = testDbContext.Districts.ToList();
            List<District> districtsById = new List<District>();
            foreach(District dis in diss)
            {
                if (dis.ProvinceId == id)
                {
                    districtsById.Add(dis);
                }
            }
            return Json(new { districtsById });
        }
        [HttpGet]
        [Route("~/Xa/{id}")]
        public JsonResult ShowXa(int id)
        {
            
            List<District> diss = testDbContext.Districts.ToList();
            List<Ward> wards = testDbContext.Wards.ToList();
            List<Ward> wardsById = new List<Ward>();
            foreach (Ward ward in wards)
            {
                if (ward.DistricId == id)
                {
                    wardsById.Add(ward);
                }
            }
            return Json(new { wardsById });
        }

        [HttpPost]
        public async Task<IActionResult> Create(User model)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Password = model.Password,
                    ConfirmPassword = model.ConfirmPassword,
                    WardId=model.WardId,
                    DistrictId = model.DistrictId,
                    ProvinceId = model.ProvinceId,
                    Address = model.Address
                };
                testDbContext.Add(user);
                await testDbContext.SaveChangesAsync();
                return RedirectToAction("ShowUser");
            }
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
