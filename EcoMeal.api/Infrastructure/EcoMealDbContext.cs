using Microsoft.EntityFrameworkCore;
using EcoMeal.api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EcoMeal.api.Infrastructure;


public class EcoMealDbContext : IdentityDbContext<User, IdentityRole<int>, int>

{
    public EcoMealDbContext(DbContextOptions<EcoMealDbContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Business> Businesses { get; set; } = null!;
    public DbSet<BusinessType> BusinessTypes { get; set; } = null!;
    public DbSet<PackageType> PackageTypes { get; set; } = null!;
    public DbSet<Package> Packages { get; set; } = null!;
   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Business>()
            .HasOne(bt => bt.BusinessType)
            .WithMany()
            .HasForeignKey(b => b.BusinessTypeId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
        
        modelBuilder.Entity<Package>()
            .HasOne(pt => pt.PackageType)
            .WithMany()
            .HasForeignKey(p => p.PackageTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Package>()
            .HasOne(b => b.Business)
            .WithMany(b => b.Packages)
            .HasForeignKey(p => p.BusinessId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Order>()
            .HasOne(p=> p.Package)
            .WithMany()
            .HasForeignKey(o => o.PackageId)
            .OnDelete(DeleteBehavior.Restrict);
        /*modelBuilder.Entity<Order>()
            .HasOne(u => u.User)
            .WithMany()
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Restrict);

*/
    }
}