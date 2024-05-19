using System.ComponentModel.DataAnnotations.Schema;
namespace thms.Media.API.Model;
public class Color {

  [Column("color_id")]
  public int Id { get; set; }

  [Column("hex_value")]
  public string HexValue { get; set; } = null!;
}
