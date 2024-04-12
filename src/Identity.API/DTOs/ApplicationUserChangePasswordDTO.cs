namespace hms.Identity.API.DTOs;
public class ApplicationUserChangePasswordDTO {
  public string EmailOrUserName { get; set; } = null!;
  public string OldPassword { get; set; } = null!;
  public string NewPassword { get; set; } = null!;
}
