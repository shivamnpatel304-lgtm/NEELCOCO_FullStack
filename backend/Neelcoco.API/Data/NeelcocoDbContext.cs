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
            new Category { Id = 1, Name = "Ice Pops", Slug = "ice-pops" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Blueberry Ice Pop", Slug = "blueberry-ice-pop", Description = "Refreshing and tangy blueberry ice gola pop bursting with chilled berry coolness.", Price = 20, MRP = 25, Unit = "1 pc", ImageUrl = "/blueberry.jpeg", IsFeatured = true, StockQuantity = 500, CategoryId = 1 },
            new Product { Id = 2, Name = "Cold Coco Milky Pop", Slug = "cold-coco-milky-pop", Description = "Signature rich cold cocoa milk indulgence frozen into a delicious milky pop.", Price = 25, MRP = 30, Unit = "1 pc", ImageUrl = "/coldcoco.jpeg", IsFeatured = true, StockQuantity = 500, CategoryId = 1 },
            new Product { Id = 3, Name = "Raw Mango Ice Pop", Slug = "raw-mango-ice-pop", Description = "Tangy and refreshing kachi keri raw mango ice gola pop with a zesty burst.", Price = 20, MRP = 25, Unit = "1 pc", ImageUrl = "/green.jpeg", IsFeatured = true, StockQuantity = 500, CategoryId = 1 },
            new Product { Id = 4, Name = "Kala Khatta Ice Pop", Slug = "kala-khatta-ice-pop", Description = "Classic sweet and tangy spiced jamun-flavour Kala Khatta ice gola pop.", Price = 20, MRP = 25, Unit = "1 pc", ImageUrl = "/kotha.jpeg", IsFeatured = true, StockQuantity = 500, CategoryId = 1 },
            new Product { Id = 5, Name = "Mango Milky Pop", Slug = "mango-milky-pop", Description = "Luscious alphonso mango combined with rich creamy milk in an ice pop.", Price = 25, MRP = 30, Unit = "1 pc", ImageUrl = "/mango.jpeg", IsFeatured = true, StockQuantity = 500, CategoryId = 1 },
            new Product { Id = 6, Name = "Orange Ice Pop", Slug = "orange-ice-pop", Description = "Vibrant citrus burst of chilled orange ice gola pop for total refreshment.", Price = 20, MRP = 25, Unit = "1 pc", ImageUrl = "/orange.jpeg", IsFeatured = true, StockQuantity = 500, CategoryId = 1 },
            new Product { Id = 7, Name = "Rajbhog Milky Pop", Slug = "rajbhog-milky-pop", Description = "Royal saffron-infused rich milk ice pop with traditional rich Indian flavours.", Price = 25, MRP = 30, Unit = "1 pc", ImageUrl = "/rajbhog.jpeg", IsFeatured = true, StockQuantity = 500, CategoryId = 1 }
        );
    }
}
