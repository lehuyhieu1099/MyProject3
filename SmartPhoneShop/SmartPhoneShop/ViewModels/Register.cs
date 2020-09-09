using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartPhoneShop.ViewModels
{
    public class Register
    {
        [Required(ErrorMessage ="Email là bắt buộc")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Xác nhận mật khẩu là bắt buộc")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không khớp")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Tên là bắt buộc")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Địa chỉ là bắt buộc")]
        public string Address { get; set; }
        public string Gender { get; set; }
        public IFormFile Image { get; set; }
    }
}
