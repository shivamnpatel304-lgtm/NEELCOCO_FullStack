using Microsoft.EntityFrameworkCore;
using Neelcoco.API.Models;

namespace Neelcoco.API.Data;

public class NeelcocoDbContext(DbContextOptions<NeelcocoDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<ContactInquiry> ContactInquiries => Set<ContactInquiry>();
    public DbSet<AdminUser> AdminUsers => Set<AdminUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().Property(x => x.Price).HasPrecision(18, 2);
        modelBuilder.Entity<Product>().Property(x => x.MRP).HasPrecision(18, 2);
        modelBuilder.Entity<Order>().Property(x => x.TotalAmount).HasPrecision(18, 2);
        modelBuilder.Entity<OrderItem>().Property(x => x.UnitPrice).HasPrecision(18, 2);

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Kulfi", Slug = "kulfi" },
            new Category { Id = 2, Name = "Ice Pops", Slug = "ice-pops" },
            new Category { Id = 3, Name = "Dairy Desserts", Slug = "dairy-desserts" },
            new Category { Id = 4, Name = "Mukhwas", Slug = "mukhwas" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Malai Kulfi", Slug = "malai-kulfi", Description = "Creamy traditional milk kulfi.", Price = 40, MRP = 50, Unit = "1 pc", ImageUrl = "https://images.unsplash.com/photo-1563805042-7684c019e1cb?auto=format&fit=crop&w=900&q=80", IsFeatured = true, StockQuantity = 500, CategoryId = 1 },
            new Product { Id = 2, Name = "Chocolate Splash", Slug = "chocolate-splash", Description = "Rich chocolate dairy dessert.", Price = 35, MRP = 40, Unit = "200 ml", ImageUrl = "https://images.unsplash.com/photo-1575377427642-087cf684f29d?auto=format&fit=crop&w=900&q=80", IsFeatured = true, StockQuantity = 400, CategoryId = 3 },
            new Product { Id = 3, Name = "Milky Pop", Slug = "milky-pop", Description = "Fun creamy frozen treat.", Price = 15, MRP = 20, Unit = "1 pc", ImageUrl = "https://images.unsplash.com/photo-1570197788417-0e82375c9371?auto=format&fit=crop&w=900&q=80", IsFeatured = true, StockQuantity = 1000, CategoryId = 2 },
            new Product { Id = 4, Name = "Jamun Seed Mukhwas", Slug = "jamun-seed-mukhwas", Description = "Aromatic mouth freshener inspired by Indian ingredients.", Price = 80, MRP = 100, Unit = "100 g", ImageUrl = "https://images.unsplash.com/photo-1601050690597-df0568f70950?auto=format&fit=crop&w=900&q=80", IsFeatured = false, StockQuantity = 300, CategoryId = 4 }
        );
    }
}
