using hms.Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using hms.Identity.API.Services.Interfaces;
using hms.Identity.API.Services.Helpers;

namespace hms.Identity.API.Services;

public class EFPasswordService : IPasswordService {
  private UserManager<ApplicationUser> _userManager;
  private IEmailSender _emailSender;
  private static readonly EmailAddressAttribute _emailAddressAttribute = new();

  public EFPasswordService(UserManager<ApplicationUser> userManager,
                           IEmailSender emailSender) {
    _userManager = userManager;
    _emailSender = emailSender;
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

  public async Task<string> ResetPassword(string email, string token,
                                          string newPassword) {
    var user = await _userManager.FindByEmailAsync(email);
    if (user is null) {
      return "No User";
    }
    if (!await _userManager.IsEmailConfirmedAsync(user)) {
      return "Email Not Confirmed";
    }
    token = EncodingHelper.Decode(token);
    await _userManager.ResetPasswordAsync(user, token, newPassword);
    return "";
  }

  public async Task<string> ForgetPassword(string Email) {
    var user = await _userManager.FindByEmailAsync(Email);
    if (user is null)
      return "No user Found";

    if (!await _userManager.IsEmailConfirmedAsync(user)) {
      return "Email Not Confirmed";
    }

    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
    var tokenSuitableForHtml = EncodingHelper.Encode(token);

    var result = _emailSender.SendEmailAsync(Email, "Password Reset",
                                             tokenSuitableForHtml);
    await result;
    if (!result.IsCompletedSuccessfully) {
      var ex = result.Exception;
      Console.WriteLine(ex.Message);
      return "something went wrong";
    }
    // TODO: THIS IS FOR TESTING ONLY DONT RETURN LINK IN PRODUCTION
    return tokenSuitableForHtml;
  }
}
