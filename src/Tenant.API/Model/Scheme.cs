using System.ComponentModel.DataAnnotations.Schema;
using thms.Media.API.Model;
namespace thms.Tenant.API.Model;

public class Scheme {

  [Column("scheme_id")]
  public int Id { get; set; }

  [Column("primary_color")]
  public virtual Color PrimaryColor { get; set; } = null!;

  [Column("secondary_color")]
  public virtual Color SecondaryColor { get; set; } = null!;

  public int TenantId { get; set; }
}
