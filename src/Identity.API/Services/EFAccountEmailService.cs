using hms.Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using hms.Identity.API.Services.Interfaces;
using hms.Identity.API.Services.Helpers;

namespace hms.Identity.API.Services;

public class EFAccountEmailService : IAccountEmailService
{
  private UserManager<ApplicationUser> _userManager;
  private IEmailSender _emailSender;
  private static readonly EmailAddressAttribute _emailAddressAttribute = new();

  public EFAccountEmailService(UserManager<ApplicationUser> userManager,
                               IEmailSender emailSender)
  {
    _userManager = userManager;
    _emailSender = emailSender;
  }
  public async Task<string> SendConfirmationEmail(string Email)
  {
    var user = await _userManager.FindByEmailAsync(Email);
    if (user is null)
      return "No user Found";

    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

    var tokenSuitableForHtml = EncodingHelper.Encode(token);
    var confirmationLink =
        $@"http://localhost:5085/Account/confirmEmail?userId={user.Id}&code={tokenSuitableForHtml}";
    var body = $@" 
        Click on the <a href='{confirmationLink}'> link </a>to confirm your Email !!
      ";
    var result = _emailSender.SendEmail(Email, "Confirm Email", body);
    return result;
  }

  public async Task<IdentityResult> ConfirmEmail(string userId, string code)
  {
    var user = await _userManager.FindByIdAsync(userId);
    var token = EncodingHelper.Decode(code);
    if (user is null)
    {
      var errors = new List<IdentityError> { new IdentityError {
        Code = "UserNotFound", Description = "Specfied user is not found !!"
      } };
      IdentityResult result = IdentityResult.Failed(errors.ToArray());
      return result;
    }
    return await _userManager.ConfirmEmailAsync(user, token);
  }
}

