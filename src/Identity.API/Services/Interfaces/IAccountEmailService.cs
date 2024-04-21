namespace hms.Identity.API.Services.Interfaces;
public interface IAccountEmailService
{
  public Task<string> SendConfirmationEmail(string Email);
  public Task<string> ConfirmEmail(string userId, string code);
}
