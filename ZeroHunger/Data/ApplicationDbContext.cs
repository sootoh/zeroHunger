using ZeroHunger.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ZeroHunger.Data
{
    public class ApplicationDbContext : DbContext
    {   
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}
        public DbSet<Delivery> Delivery { get; set; }
        public DbSet<Product>Product { get; set; }
    }
}
