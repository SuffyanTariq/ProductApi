using Microsoft.EntityFrameworkCore;
using ProductApi.Model;

namespace ProductApi.Data
{
    public class ProductDBContext : DbContext
    {
        public ProductDBContext(DbContextOptions<ProductDBContext> options) : base(options) { }

        public DbSet<Product> Product { get; set; }
    }
}
