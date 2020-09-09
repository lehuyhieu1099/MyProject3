using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartPhoneShop.ViewModels
{
    public class EditUserByYourself
    {
        public string Id { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Gender { get; set; }
    
        public string PhoneNumber { get; set; }
        public string DoB { get; set; }
     
        public string Company { get; set; }
        
        public string Facebook { get; set; }
     
        public string DTW { get; set; }
     
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }
        public string RoleId { get; set; }
    }
}
