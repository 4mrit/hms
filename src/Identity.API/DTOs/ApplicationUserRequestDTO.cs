namespace thms.Identity.API.DTOs;
public class ApplicationUserRequestDTO {
  public string EmailOrUserName { get; set; } = null!;
  public string Password { get; set; } = null!;
  public string? PhoneNumber { get; set; } = null;
}
