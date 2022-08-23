using IWantApp.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace IWantApp.Infra.Data;

public class ApplicationDbContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().Property(prod => prod.Name).IsRequired();
            builder.Entity<Product>().Property(prod => prod.Description).HasMaxLength(255);
            builder.Entity<Category>().Property(cat => cat.Name).IsRequired();
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configuration)
        {
            configuration.Properties<string>().HaveMaxLength(100);
        }
    }
}
