using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Models
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options)
           : base(options)
        {
        }
        public  DbSet<Province> Provinces { get; set; }
        public  DbSet<District> Districts { get; set; }
        public  DbSet<Ward> Wards { get; set; }
        public  DbSet<User> Users { get; set; }
    }
}
