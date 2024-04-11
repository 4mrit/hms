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
  SignInUsingUserNameOrEmailAsync(ApplicationUserRequestDTO user)
  {
    if (string.IsNullOrEmpty(user.EmailOrUserName))
      throw new ValidationException("Email or Username Required !!");

    if (_emailAddressAttribute.IsValid(user.EmailOrUserName))
      return await SignInUsingEmailAsync(user.EmailOrUserName, user.Password);
    else
      return await SignInUsingUserNameAsync(user.EmailOrUserName,
                                            user.Password);
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
