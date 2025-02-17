using Microsoft.EntityFrameworkCore;
using Elysian.Models;

namespace Elysian.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Billboard> Billboards { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Size> Sizes{ get; set; }
        public DbSet<Color> Colors{ get; set; }
        public DbSet<Order> Order{ get; set; }
        public DbSet<Image> Image{ get; set; }
        public DbSet<OrderItem> OrderItem{ get; set; }
    }
}
