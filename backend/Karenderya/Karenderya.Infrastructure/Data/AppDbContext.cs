using Karenderya.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Karenderya.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<MenuItem> MenuItems => Set<MenuItem>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
    }
}
