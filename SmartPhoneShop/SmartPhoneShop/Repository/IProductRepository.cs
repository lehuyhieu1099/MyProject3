using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartPhoneShop.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> Gets();
        Product Create(Product product);
        Product Get(int id);
        Product Edit(Product product);
        bool Delete(int id);
        
    }
}
