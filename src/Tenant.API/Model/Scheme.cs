using System.ComponentModel.DataAnnotations.Schema;
using hms.Media.API.Model;
namespace hms.Tenant.API.Model;

public class Scheme{
    
    [Column("scheme_id")]
    public int Id { get; set;}

    [Column("primary_color")]
    public Color PrimaryColor { get; set; } = null!;

    [Column("secondary_color")]
    public Color SecondaryColor { get; set; } = null!;

}