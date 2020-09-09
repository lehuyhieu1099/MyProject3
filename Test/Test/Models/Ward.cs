using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Models
{
    public class Ward
    {
        public int WardId { get; set; }
        public string Name { get; set; }
        public string Prefix { get; set; }
        public int ProvinceId { get; set; }
        public int DistricId { get; set; }
    }
}
