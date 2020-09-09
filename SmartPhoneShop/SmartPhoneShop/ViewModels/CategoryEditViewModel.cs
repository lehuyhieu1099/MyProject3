using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartPhoneShop.ViewModels
{
    public class CategoryEditViewModel
    {

        public int CategoryId { get; set; }
        public string ImagePath { get; set; }
        [Required(ErrorMessage = "Trường này là bắt buộc")]
        [Display(Name = "Hệ điều hành")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Trường này là bắt buộc")]
        [Display(Name = "Ảnh")]
        public IFormFile Image { get; set; }
    }
}
