using Microsoft.EntityFrameworkCore;
using EcoMeal.api.Entities;

namespace EcoMeal.api.Infrastructure;

public class EcoMealDbContext : DbContext
{
    public EcoMealDbContext(DbContextOptions<EcoMealDbContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Business> Businesses { get; set; } = null!;
    public DbSet<BusinessType> BusinessTypes { get; set; } = null!;
    public DbSet<PackageType> PackageTypes { get; set; } = null!;
    public DbSet<Package> Packages { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Business>()
            .HasOne(bt => bt.BusinessType)
            .WithMany()
            .HasForeignKey(b => b.BusinessTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Package>()
            .HasOne(pt => pt.PackageType)
            .WithMany()
            .HasForeignKey(p => p.PackageTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Package>()
            .HasOne(b => b.Business)
            .WithMany()
            .HasForeignKey(p => p.BusinessTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Order>()
            .HasOne(p=> p.Package)
            .WithMany()
            .HasForeignKey(o => o.PackageId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Order>()
            .HasOne(u => u.User)
            .WithMany()
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Restrict);


    }
}