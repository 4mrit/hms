using hms.Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using hms.Identity.API.DTOs;
using System.ComponentModel.DataAnnotations;
using hms.Identity.API.Authorization.Constants;
using hms.Identity.API.Services.Interfaces;
namespace hms.Identity.API.Services;

public class EFRegisterService : IRegisterService<ApplicationUserRegisterDTO>
{
  private UserManager<ApplicationUser> _userManager;
  private static readonly EmailAddressAttribute _emailAddressAttribute = new();

  public EFRegisterService(UserManager<ApplicationUser> userManager)
  {
    _userManager = userManager;
  }

  public Task Register(ApplicationUserRegisterDTO userDTO)
  {
    if (string.IsNullOrEmpty(userDTO.Email) &&
        string.IsNullOrEmpty(userDTO.UserName))
    {
      throw new ValidationException(
          "UserName or Email Required for User Registration");
    }

    var user = new ApplicationUser()
    {
      UserName = userDTO.UserName,
      Email = userDTO.Email,
      PhoneNumber = userDTO.PhoneNumber
    };

    return _userManager.CreateAsync(user, userDTO.Password);
  }
  public Task RegisterAdmin(ApplicationUserRegisterDTO userDTO)
  {
    if (string.IsNullOrEmpty(userDTO.Email) &&
        string.IsNullOrEmpty(userDTO.UserName))
    {
      throw new ValidationException(
          "UserName or Email Required for User Registration");
    }

    var user = new ApplicationUser()
    {
      UserName = userDTO.UserName,
      Email = userDTO.Email,
      PhoneNumber = userDTO.PhoneNumber
    };

    var result = _userManager.CreateAsync(user, userDTO.Password);
    var claim = new System.Security.Claims.Claim(ApplicationClaims.Role,
                                                 ApplicationRoles.Admin);
    return _userManager.AddClaimAsync(user, claim);
  }
}
