using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartPhoneShop.ViewModels
{
    public class ProductCreateViewModel
    {
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Không được để ảnh trống")]
        public IFormFile Image { get; set; }
        [Required(ErrorMessage = "Nhãn hiệu không được để trống")]
        public int BrandId { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Giá không đúng định dạng")]
        public float Price { get; set; }
        [Required(ErrorMessage = "Số lượng sản phẩm không đúng định dạng")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "Không hợp lệ")]
        public int CategoryId { get; set; }
    }
}
