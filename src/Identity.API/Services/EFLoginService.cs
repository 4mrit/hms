using thms.Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using thms.Identity.API.DTOs;
using System.ComponentModel.DataAnnotations;
using thms.Identity.API.Services.Interfaces;
namespace thms.Identity.API.Services;

public class EFLoginService : ILoginService<ApplicationUserLoginDTO>
{
  private SignInManager<ApplicationUser> _signInManager;
  private UserManager<ApplicationUser> _userManager;
  private static readonly EmailAddressAttribute _emailAddressAttribute = new();

  public EFLoginService(SignInManager<ApplicationUser> signInManager,
                        UserManager<ApplicationUser> userManager)
  {
    _signInManager = signInManager;
    _userManager = userManager;
  }

  public async Task<SignInResult> SignInUsingUserNameAsync(string userName,
                                                           string Password)
  {
    return await _signInManager.PasswordSignInAsync(userName, Password, false,
                                                    false);
  }

  public async Task<SignInResult> SignInUsingEmailAsync(string Email,
                                                        string Password)
  {
    if (!_userManager.SupportsUserEmail)
      throw new NotSupportedException(
          $"{nameof(SignInUsingEmailAsync)} requires a user store with email support.");

    var user = await _userManager.FindByEmailAsync(Email);
    if (user is null)
      throw new ValidationException("user not found , email : " + Email);

    return await _signInManager.PasswordSignInAsync(
        user, Password, isPersistent: false, lockoutOnFailure: false);
  }

  public async Task<SignInResult>
  SignInUsingUserNameOrEmailAsync(ApplicationUserLoginDTO user)
  {
    if (string.IsNullOrEmpty(user.EmailOrUserName))
      throw new ValidationException("Email or Username Required !!");

    if (_emailAddressAttribute.IsValid(user.EmailOrUserName))
      return await SignInUsingEmailAsync(user.EmailOrUserName, user.Password);
    else
      return await SignInUsingUserNameAsync(user.EmailOrUserName,
                                            user.Password);
  }
}
