using Microsoft.AspNetCore.Identity;

namespace hms.Identity.API.Services.Interfaces;
public interface IAccountEmailService
{
  public Task<string> SendConfirmationEmail(string Email);
  public Task<IdentityResult> ConfirmEmail(string userId, string code);
}
