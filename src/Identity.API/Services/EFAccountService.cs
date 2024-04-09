using hms.Identity.API.Models;
using Microsoft.AspNetCore.Identity;
namespace hms.Identity.API.Services;

public class EFAccountService : IAccountService<ApplicationUser> {
  private SignInManager<ApplicationUser> _signInManager;
  public EFAccountService(SignInManager<ApplicationUser> signInManager) {
    _signInManager = signInManager;
  }

  public Task SignIn(string userName, string Password) {
    // return _signInManager.SignInAsync(user, true);
    return _signInManager.PasswordSignInAsync(userName, Password, false, false);
  }
}
