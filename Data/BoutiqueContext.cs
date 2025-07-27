using Microsoft.EntityFrameworkCore;
using BoutiqueApi.Models;

namespace BoutiqueApi.Data
{
    public class BoutiqueContext : DbContext
    {
        public BoutiqueContext(DbContextOptions<BoutiqueContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Flavor> Flavors { get; set; }

    }
}
