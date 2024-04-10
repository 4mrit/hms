using hms.Identity.API.Models;
using Microsoft.AspNetCore.Identity;
namespace hms.Identity.API.Services;

public class EFAccountService : IAccountService<ApplicationUser>
{
  private SignInManager<ApplicationUser> _signInManager;
  private UserManager<ApplicationUser> _userManager;
  public EFAccountService(SignInManager<ApplicationUser> signInManager,
                          UserManager<ApplicationUser> userManager)
  {
    _signInManager = signInManager;
    _userManager = userManager;
  }

  public Task SignIn(string userName, string Password)
  {
    // return _signInManager.SignInAsync(user, true);
    return _signInManager.PasswordSignInAsync(userName, Password, false, false);
  }

  public Task Register(ApplicationUser user, string Password)
  {
    return _userManager.CreateAsync(user, Password);
  }
}
