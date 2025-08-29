using Microsoft.EntityFrameworkCore;
using ProductApi.Models;

namespace ProductApi.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    
    //Fluent Api
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Category
        modelBuilder.Entity<Category>().HasKey(c=> c.CategoryId);
        
        modelBuilder.Entity<Category>().
            Property(c=> c.Name).
            HasMaxLength(100).
            IsRequired();
        
        //Product
        modelBuilder.Entity<Product>().
            Property(c=> c.Description).
            HasMaxLength(100).
            IsRequired();
        
        modelBuilder.Entity<Product>().
            Property(c=> c.Name).
            HasMaxLength(100).
            IsRequired();
        
        modelBuilder.Entity<Product>().
            Property(c=> c.ImageUrl).
            HasMaxLength(255).
            IsRequired();
        
        modelBuilder.Entity<Product>().
            Property(c=> c.Price).
            HasPrecision(12, 2).
            IsRequired();
        
        modelBuilder.Entity<Category>().
            HasMany(g=> g.Products).
            WithOne(g=> g.Category).
            IsRequired().
            OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                CategoryId = 1,
                Name = "Material Escolar"
            },
            new Category
            {
                CategoryId = 2,
                Name = "Acessórios"
            });
    }
}