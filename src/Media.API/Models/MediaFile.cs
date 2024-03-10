using System.ComponentModel.DataAnnotations.Schema;
namespace hms.Media.API.Model;
public class MediaFile{
    
    [Column("media_id")]
    public int Id { get; set;}

    [Column("path")]
    public string Path { get; set; } = null!;

}