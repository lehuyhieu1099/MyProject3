using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartPhoneShop.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string ImagePath { get; set; }
        public List<Product> Products { get; set; }
        public List<Brand> Brands { get; set; }
        public bool IsDelete { get; set; }
    }
}
