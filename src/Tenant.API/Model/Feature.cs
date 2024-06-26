using System.ComponentModel.DataAnnotations.Schema;
namespace thms.Tenant.API.Model;
public class Feature {

  [Column("feature_id")]
  public int Id { get; set; }

  [Column("name")]
  public string Name { get; set; } = null!;
}
