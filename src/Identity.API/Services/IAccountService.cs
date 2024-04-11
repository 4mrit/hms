namespace hms.Identity.API.Services;
public interface IAccountService<T, TRequest, TResponse>
{
  public Task SignInUsingUserName(string userName, string Password);
  public Task SignInUsingEmail(string userName, string Password);
  public Task SignInUsingUserNameOrEmail(TRequest request);
  public Task Register(T user, string Password);
  public Task ChangePassword(T user, string oldPassword, string newPassword);
}
