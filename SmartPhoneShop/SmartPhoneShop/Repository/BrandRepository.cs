using Microsoft.EntityFrameworkCore;
using SmartPhoneShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartPhoneShop.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly AppDbConText context;

        public BrandRepository(AppDbConText context)
        {
            this.context = context;
        }
        public Brand Create(Brand brand)
        {
            context.Brands.Add(brand);
            context.SaveChanges();
            return brand;
        }

        public bool Delete(int id)
        {
            Brand brand = Get(id);
            if (brand != null)
            {
                brand.IsDelete = true;
                context.SaveChanges();
                return true;
            }
            return false;
        }


        public Brand Edit(Brand brand)
        {
            var edit = context.Brands.Attach(brand);
            edit.State = EntityState.Modified;
            context.SaveChanges();
            return brand;
        }

        public Brand Get(int id)
        {
            Brand brand = context.Brands.Find(id);
            return brand;
        }

        public IEnumerable<Brand> Gets()
        {
            return context.Brands.ToList();
        }
    }
}
