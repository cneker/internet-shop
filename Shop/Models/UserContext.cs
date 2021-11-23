using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Shop.Models
{
    public class UserContext : IdentityDbContext<UserModel>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<CartModel> Cart { get; set; }
        public UserContext(DbContextOptions<UserContext> dbContextOptions) : base(dbContextOptions)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
