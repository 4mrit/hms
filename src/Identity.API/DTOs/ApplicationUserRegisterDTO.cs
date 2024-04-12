namespace hms.Identity.API.DTOs;
public class ApplicationUserRegisterDTO {
  public string? Email { get; set; } = null!;
  public string? UserName { get; set; } = null!;
  public string Password { get; set; } = null!;
  public string? PhoneNumber { get; set; }
}
