using hms.Identity.API.Models;
using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations;
namespace hms.Identity.API.Services;

public class EFAccountService
    : IAccountService<ApplicationUser, ApplicationUserRequestDTO,
                      ApplicationUserResponseDTO>
{
  private SignInManager<ApplicationUser> _signInManager;
  private UserManager<ApplicationUser> _userManager;
  private static readonly EmailAddressAttribute _emailAddressAttribute = new();

  public EFAccountService(SignInManager<ApplicationUser> signInManager,
                          UserManager<ApplicationUser> userManager)
  {
    _signInManager = signInManager;
    _userManager = userManager;
  }

  public Task SignInUsingUserName(string userName, string Password)
  {
    var user = new ApplicationUser() { UserName = userName };
    return _signInManager.PasswordSignInAsync(userName, Password, false, false);
    // return _signInManager.PasswordSignInAsync(userName, Password, false,
    // false);
  }

  public async Task SignInUsingEmail(string Email, string Password)
  {
    ApplicationUser user;
    user = await _userManager.FindByEmailAsync(Email);
    if (user is null)
      throw new Exception("user not found , email : " + Email);
    await _signInManager.PasswordSignInAsync(user, Password, false, false);
    return;
  }

  public async Task SignInUsingUserNameOrEmail(ApplicationUserRequestDTO user)
  {

    if (string.IsNullOrEmpty(user.EmailOrUserName))
      throw new ValidationException("Email or Username Required !!");

    if (_emailAddressAttribute.IsValid(user.EmailOrUserName))
    {
      await SignInUsingEmail(user.EmailOrUserName, user.Password);
    }
    else
    {
      await SignInUsingUserName(user.EmailOrUserName, user.Password);
    }
    return;
  }

  public Task Register(ApplicationUser user, string Password)
  {
    return _userManager.CreateAsync(user, Password);
  }

  public Task ChangePassword(ApplicationUser user, string oldPassword,
                             string newPassword)
  {
    return _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
  }
}
