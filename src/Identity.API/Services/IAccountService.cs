namespace hms.Identity.API.Services;
public interface IAccountService<T> {
  public Task SignInUsingUserName(string userName, string Password);
  public Task SignInUsingEmail(string userName, string Password);
  public Task SignInUsingUserNameOrEmail(string UserNameOrEmail,
                                         string Password);
  public Task Register(T user, string Password);
  public Task ChangePassword(T user, string oldPassword, string newPassword);
}
