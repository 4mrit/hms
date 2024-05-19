namespace thms.Identity.API.DTOs;

public class ApplicationUserResetPasswordDTO
{
  public string UserId { get; set; } = null!;
  public string Token { get; set; } = null!;
  public string Password { get; set; } = null!;
}
