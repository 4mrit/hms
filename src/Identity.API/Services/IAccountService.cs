namespace hms.Identity.API.Services;
public interface IAccountService<T, TRequest, TResponse>
{
  public Task<Microsoft.AspNetCore.Identity.SignInResult>
  SignInUsingUserNameAsync(string userName, string Password);

  public Task<Microsoft.AspNetCore.Identity.SignInResult>
  SignInUsingEmailAsync(string userName, string Password);

  public Task<Microsoft.AspNetCore.Identity.SignInResult>
  SignInUsingUserNameOrEmailAsync(TRequest request);

  public Task Register(T user, string Password);
  public Task ChangePassword(T user, string oldPassword, string newPassword);
}
