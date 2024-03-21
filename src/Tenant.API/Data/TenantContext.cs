using Microsoft.EntityFrameworkCore;
using hms.Tenant.API.Model;
using hms.Media.API.Model;
namespace hms.Tenant.API.Data;

public class TenantContext : DbContext {
  public TenantContext(DbContextOptions options) : base(options) {}
  public DbSet<Color> Colors { get; set; } = null!;
  public DbSet<Scheme> Schemes { get; set; } = null!;
  public DbSet<Feature> Features { get; set; } = null!;
  public DbSet<MediaFile> MediaFiles { get; set; } = null!;
  public DbSet<HospitalTenant> Tenants { get; set; } = null!;
  public DbSet<TenantDatabase> TenantDatabases { get; set; } = null!;

  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    // Configure relationship between HospitalTenant and Feature
    // modelBuilder.Entity<HospitalTenant>()
    //     .HasMany(h => h.Features)           // HospitalTenant has many
    //     Features .WithOne()                          // Feature has one
    //     HospitalTenant .HasForeignKey("HospitalTenantId"); // Foreign key
    //     property in Feature
    //
    // modelBuilder.Entity<Feature>()
    //     .HasOne(f => f.Tenant)      // Feature has one HospitalTenant
    //     .WithMany()                 // HospitalTenant has many Features
    //     .HasForeignKey("TenantId"); // Foreign key property in Feature
    //
    modelBuilder.Entity<HospitalTenant>().HasMany(h => h.Features).WithMany();
  }
}
