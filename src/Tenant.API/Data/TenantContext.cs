using Microsoft.EntityFrameworkCore;
using hms.Tenant.API.Model;
using hms.Media.API.Model;
namespace hms.Tenant.API.Data;

public class TenantContext : DbContext
{
    public TenantContext(DbContextOptions options) : base(options) {}
    public DbSet<Color> Colors { get; set; } =null!;
    public DbSet<Scheme> Schemes { get; set; } =null!;
    public DbSet<Feature> Features { get; set; } =null!;
    public DbSet<MediaFile> MediaFiles { get; set; } =null!;
    public DbSet<HospitalTenant> Tenants { get; set; } =null!;
    public DbSet<TenantDatabase> TenantDatabases { get; set; } =null!;
}
