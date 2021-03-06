﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartPhoneShop.ViewModels
{
    public class BrandCreate
    {
        [Required(ErrorMessage ="Trường này là bắt buộc")]
        [Display(Name = "Hệ điều hành")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Trường này là bắt buộc")]
        [Display(Name = "Nhãn hiệu")]
        public string BrandName { get; set; }
        [Required(ErrorMessage = "Trường này là bắt buộc")]
        [Display(Name = "Ảnh")]
        public IFormFile BrandImg { get; set; }
    }
}
