using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Models
{
    public class User
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Confirm password not match")]
        public string ConfirmPassword { get; set; }
       
        public int WardId { get; set; }
        
        public int DistrictId { get; set; }
        
        public int ProvinceId { get; set; }
        
        public string Address { get; set; }
    }
}
