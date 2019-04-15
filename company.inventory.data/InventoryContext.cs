using company.inventory.data.Entities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace company.inventory.data
{
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductService> ProductServices { get; set; }
        public DbSet<ProductStock> Stock { get; set; }
    }
}