using System.ComponentModel.DataAnnotations.Schema;
namespace hms.Tenant.API.Model;

public class TenantDatabase{
    
    [Column("database_id")]
    public int Id { get; set;}

    [Column("conncection_string")]
    public String ConnectionString { get; set; } = null!;

}
