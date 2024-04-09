namespace hms.Identity.API.Services;
public interface IAccountService<T> {
  public Task SignIn(string userName, string Password);
}
