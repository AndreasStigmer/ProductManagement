using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class ProductDbContext:DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> opt):base(opt)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductProperty> Properties { get; set; }
    }
}
