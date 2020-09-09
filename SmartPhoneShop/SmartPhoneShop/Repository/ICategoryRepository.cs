using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartPhoneShop.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Gets();
        Category Create(Category category);
        Category Get(int id);
        Category Edit(Category category);
       
        bool Delete(int id);
    }
}
