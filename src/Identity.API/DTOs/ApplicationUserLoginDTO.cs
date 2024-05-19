namespace thms.Identity.API.DTOs;
public class ApplicationUserLoginDTO {
  public string EmailOrUserName { get; set; } = null!;
  public string Password { get; set; } = null!;
}
