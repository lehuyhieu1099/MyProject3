using SmartPhoneShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartPhoneShop.Repository
{
    public interface IBrandRepository
    {
        IEnumerable<Brand> Gets();
        Brand Create(Brand brand);
        Brand Get(int id);
        Brand Edit(Brand brand);
        bool Delete(int id);
    }
}
