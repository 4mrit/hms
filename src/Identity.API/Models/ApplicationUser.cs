using Microsoft.AspNetCore.Identity;
namespace hms.Identity.API.Models;

// {
//   "id": "string",
//   "userName": "string",
//   "email": "string",
//   "phoneNumber": "string",
// }
public class ApplicationUser : IdentityUser {}

public class ApplicationUserRequestDTO {

  public string UserName { get; protected init; } = null!;

  public string Email { get; protected init; } = null!;

  public string Password { get; protected init; } = null!;

  public string? PhoneNumber { get; protected init; }
}
