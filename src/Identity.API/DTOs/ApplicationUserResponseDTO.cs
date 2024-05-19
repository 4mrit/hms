namespace thms.Identity.API.DTOs;
public class ApplicationUserResponseDTO {
  public string Id { get; protected init; } = null!;
  public string UserNameOrEmail { get; protected init; } = null!;
  public string Email { get; protected init; } = null!;
  public string PhoneNumber { get; protected init; } = null!;
  public bool TwoFactorEnabled { get; protected init; }
}
