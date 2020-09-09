using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartPhoneShop.Models
{
    public class Brand
    {
        public int BrandId { get; set; }
        [Required]
        public string BrandName { get; set; }
        public string ImagePath { get; set; }
        public int CategoryId { get; set; }
        public bool IsDelete { get; set; }

    }
}
