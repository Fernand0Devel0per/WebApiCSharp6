using Flunt.Notifications;
using IWantApp.Domain.Orders;
using IWantApp.Domain.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IWantApp.Infra.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{

    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Order> Orders { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Ignore<Notification>();
        builder.Entity<Product>().Property(prod => prod.Name).IsRequired();
        builder.Entity<Product>().Property(prod => prod.Description).HasMaxLength(255);
        builder.Entity<Product>().Property(prod => prod.Price).HasColumnType("decimal(10,2)").IsRequired();
        
        builder.Entity<Category>().Property(cat => cat.Name).IsRequired();

        builder.Entity<Order>().Property(prod => prod.ClientId).IsRequired();
        builder.Entity<Order>().Property(prod => prod.DeliveryAddress).IsRequired();
        builder.Entity<Order>().HasMany(o => o.Products).WithMany(p => p.Orders)
            .UsingEntity(x => x.ToTable("OrderProducts"));
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configuration)
    {
        configuration.Properties<string>().HaveMaxLength(100);
    }

}
