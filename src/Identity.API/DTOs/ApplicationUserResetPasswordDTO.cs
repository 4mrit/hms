namespace hms.Identity.API.DTOs;

public class ApplicationUserResetPasswordDTO
{
  public string Email { get; set; } = null!;
  public string Token { get; set; } = null!;
  public string Password { get; set; } = null!;
}
