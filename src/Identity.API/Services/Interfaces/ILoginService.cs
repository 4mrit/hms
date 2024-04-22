namespace hms.Identity.API.Services.Interfaces;
public interface ILoginService<TLogin>
{
  public Task<Microsoft.AspNetCore.Identity.SignInResult>
  SignInUsingUserNameAsync(string userName, string Password);
  public Task<Microsoft.AspNetCore.Identity.SignInResult>
  SignInUsingEmailAsync(string userName, string Password);
  public Task<Microsoft.AspNetCore.Identity.SignInResult>
  SignInUsingUserNameOrEmailAsync(TLogin request);
}
