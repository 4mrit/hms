namespace hms.Identity.API.Services.Interfaces;
public interface IPasswordService
{
  public Task<Microsoft.AspNetCore.Identity.IdentityResult>
  ChangePassword(string emailOrUserName, string oldPassword,
                 string newPassword);

  public Task<string> ForgetPassword(string Email);
  public Task<Microsoft.AspNetCore.Identity.IdentityResult>
  ResetPassword(string Email, string Token, string NewPassword);
}
