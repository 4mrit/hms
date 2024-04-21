using hms.Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using hms.Identity.API.Services.Interfaces;

namespace hms.Identity.API.Services;

public class EFUserInformationService : IUserInformationService
{
  private UserManager<ApplicationUser> _userManager;
  private static readonly EmailAddressAttribute _emailAddressAttribute = new();

  public EFUserInformationService(UserManager<ApplicationUser> userManager)
  {
    _userManager = userManager;
  }

  public async Task<IList<System.Security.Claims.Claim>>
  GetClaimsUserUsingUserName(string userName)
  {

    return await _userManager.GetClaimsAsync(
        await _userManager.FindByNameAsync(userName));
  }
}
