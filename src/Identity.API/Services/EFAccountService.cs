using hms.Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using hms.Identity.API.DTOs;
using System.ComponentModel.DataAnnotations;
namespace hms.Identity.API.Services;

public class EFAccountService
    : IAccountService<ApplicationUser, ApplicationUserLoginDTO,
                      ApplicationUserRegisterDTO> {
  private SignInManager<ApplicationUser> _signInManager;
  private UserManager<ApplicationUser> _userManager;
  private static readonly EmailAddressAttribute _emailAddressAttribute = new();

  public EFAccountService(SignInManager<ApplicationUser> signInManager,
                          UserManager<ApplicationUser> userManager) {
    _signInManager = signInManager;
    _userManager = userManager;
  }

  public async Task<SignInResult> SignInUsingUserNameAsync(string userName,
                                                           string Password) {
    return await _signInManager.PasswordSignInAsync(userName, Password, false,
                                                    false);
  }

  public async Task<SignInResult> SignInUsingEmailAsync(string Email,
                                                        string Password) {
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
  SignInUsingUserNameOrEmailAsync(ApplicationUserLoginDTO user) {
    if (string.IsNullOrEmpty(user.EmailOrUserName))
      throw new ValidationException("Email or Username Required !!");

    if (_emailAddressAttribute.IsValid(user.EmailOrUserName))
      return await SignInUsingEmailAsync(user.EmailOrUserName, user.Password);
    else
      return await SignInUsingUserNameAsync(user.EmailOrUserName,
                                            user.Password);
  }
  public Task Register(ApplicationUserRegisterDTO userDTO) {

    if (string.IsNullOrEmpty(userDTO.Email) &&
        string.IsNullOrEmpty(userDTO.UserName)) {
      throw new ValidationException(
          "UserName or Email Required for User Registration");
    }

    var user = new ApplicationUser() { UserName = userDTO.UserName,
                                       Email = userDTO.Email,
                                       PhoneNumber = userDTO.PhoneNumber };

    return _userManager.CreateAsync(user, userDTO.Password);
  }

  public async Task<IdentityResult> ChangePassword(string emailOrUserName,
                                                   string oldPassword,
                                                   string newPassword) {
    var user = await GetUserFromEmailOrUserName(emailOrUserName);
    return await _userManager.ChangePasswordAsync(user, oldPassword,
                                                  newPassword);
  }

  private async Task<ApplicationUser>
  GetUserFromEmailOrUserName(string emailOrUserName) {
    if (string.IsNullOrEmpty(emailOrUserName))
      throw new ValidationException("Email or Username Required !!");

    if (_emailAddressAttribute.IsValid(emailOrUserName)) {
      return await _userManager.FindByEmailAsync(emailOrUserName);
    } else {
      return await _userManager.FindByNameAsync(emailOrUserName);
    }
  }
}
