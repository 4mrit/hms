using thms.Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using thms.Identity.API.Services.Interfaces;
using thms.Identity.API.Services.Helpers;

namespace thms.Identity.API.Services;

public class EFPasswordService : IPasswordService
{
  private UserManager<ApplicationUser> _userManager;
  private IEmailSender _emailSender;
  private static readonly EmailAddressAttribute _emailAddressAttribute = new();

  public EFPasswordService(UserManager<ApplicationUser> userManager,
                           IEmailSender emailSender)
  {
    _userManager = userManager;
    _emailSender = emailSender;
  }

  public async Task<IdentityResult> ChangePassword(string emailOrUserName,
                                                   string oldPassword,
                                                   string newPassword)
  {
    var user = await GetUserFromEmailOrUserName(emailOrUserName);
    return await _userManager.ChangePasswordAsync(user, oldPassword,
                                                  newPassword);
  }

  private async Task<ApplicationUser>
  GetUserFromEmailOrUserName(string emailOrUserName)
  {
    if (string.IsNullOrEmpty(emailOrUserName))
      throw new ValidationException("Email or Username Required !!");

    if (_emailAddressAttribute.IsValid(emailOrUserName))
    {
      return await _userManager.FindByEmailAsync(emailOrUserName);
    }
    else
    {
      return await _userManager.FindByNameAsync(emailOrUserName);
    }
  }

  public async Task<IdentityResult> ResetPassword(string userId, string token,
                                                  string newPassword)
  {
    var user = await _userManager.FindByIdAsync(userId);
    if (user is null)
    {
      var errors = new List<IdentityError> { new IdentityError {
        Code = "UserNotFound", Description = "Specfied user is not found !!"
      } };
      IdentityResult result = IdentityResult.Failed(errors.ToArray());
      return result;
    }
    if (!await _userManager.IsEmailConfirmedAsync(user))
    {
      var errors = new List<IdentityError> { new IdentityError {
        Code = "EmailNotConfirmed",
        Description = "Email Not Confirmed for Specified User !!"
      } };
      IdentityResult result = IdentityResult.Failed(errors.ToArray());
      return result;
    }
    token = EncodingHelper.Decode(token);
    return await _userManager.ResetPasswordAsync(user, token, newPassword);
  }

  public async Task<string> ForgetPassword(string Email)
  {

    var user = await _userManager.FindByEmailAsync(Email);
    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
    var tokenSuitableForHtml = EncodingHelper.Encode(token);
    var confirmationLink =
        $@"http://meditech.com.np/Admin/resetPassword?userId={user.Id}&code={tokenSuitableForHtml}";
    var body = $@" 
        Click on the <a href='{confirmationLink}'> link </a>to reset your Password !!
      ";

    var result = _emailSender.SendEmail(Email, "Password Reset", body);
    return result;
  }
}
