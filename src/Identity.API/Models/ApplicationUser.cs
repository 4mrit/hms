using Microsoft.AspNetCore.Identity;
namespace hms.Identity.API.Models;

// {
//   "id": "string",
//   "userName": "string",
//   "email": "string",
//   "phoneNumber": "string",
// }
public class ApplicationUser : IdentityUser { }

public class ApplicationUserRequestDTO
{
  public string EmailOrUserName { get; set; } = null!;
  public string Password { get; set; } = null!;
  public string? PhoneNumber { get; set; } = null;
}

public class ApplicationUserResponseDTO
{
  public string Id { get; protected init; } = null!;
  public string UserNameOrEmail { get; protected init; } = null!;
  public string Email { get; protected init; } = null!;
  public string PhoneNumber { get; protected init; } = null!;
  public bool TwoFactorEnabled { get; protected init; }
}
// "id": "string",
// "userName": "string",
// "normalizedUserName": "string",
// "email": "string",
// "normalizedEmail": "string",
// "emailConfirmed": true,
// "passwordHash": "string",
// "securityStamp": "string",
// "concurrencyStamp": "string",
// "phoneNumber": "string",
// "phoneNumberConfirmed": true,
// "twoFactorEnabled": true,
// "lockoutEnd": "2024-04-11T03:35:12.942Z",
// "lockoutEnabled": true,
// "accessFailedCount": 0
