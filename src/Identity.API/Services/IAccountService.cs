namespace hms.Identity.API.Services;
public interface IAccountService<T, TLogin, TRegister>
{
  public Task<Microsoft.AspNetCore.Identity.SignInResult>
  SignInUsingUserNameAsync(string userName, string Password);

  public Task<Microsoft.AspNetCore.Identity.SignInResult>
  SignInUsingEmailAsync(string userName, string Password);

  public Task<Microsoft.AspNetCore.Identity.SignInResult>
  SignInUsingUserNameOrEmailAsync(TLogin request);

  public Task Register(TRegister user);

  public Task RegisterAdmin(TRegister user);

  public Task<Microsoft.AspNetCore.Identity.IdentityResult>
  ChangePassword(string emailOrUserName, string oldPassword,
                 string newPassword);

  public Task<IList<System.Security.Claims.Claim>>
  GetClaimsUserUsingUserName(string userName);

  public Task<string> ForgetPassword(string Email);
  public Task<string> ResetPassword(string Email, string Token,
                                    string NewPassword);
}
