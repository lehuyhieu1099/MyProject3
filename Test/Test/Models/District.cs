using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Models
{
    public class District
    {
        public int DistrictId { get; set; }
        public string Name { get; set; }
        public string Prefix { get; set; }
        public int ProvinceId { get; set; }
        

    }
}
