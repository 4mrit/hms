using System.ComponentModel.DataAnnotations.Schema;
namespace hms.Tenant.API.Model;
public class HospitalTenant {
  // allow null
  // public string? Address { get; set; }
  // doesnt allow null
  // public string Address { get; set; } = null!;

  // format : [Column (string name, Properties:[Order = int],[TypeName =
  // string])

  [Column("tenant_id")]
  public int Id { get; set; }

  [Column("name")]
  public string Name { get; set; } = null!;

  [Column("address")]
  public string Address { get; set; } = null!;

  [Column("url")]
  public string Url { get; set; } = null!;

  [Column("media_root_path")]
  public string MediaRootPath { get; set; } = null!;

  // virtual for lazy loading
  public virtual Scheme? Scheme { get; set; }
  public virtual ICollection<Feature>? Features { get; set; }
  public virtual ICollection<TenantDatabase>? Databases { get; set; } = null!;
}
