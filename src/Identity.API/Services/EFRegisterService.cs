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

  public Task<IdentityResult> Register(ApplicationUserRegisterDTO userDTO)
  {
    var user = new ApplicationUser()
    {
      UserName = userDTO.UserName,
      Email = userDTO.Email,
      PhoneNumber = userDTO.PhoneNumber
    };
    return _userManager.CreateAsync(user, userDTO.Password);
  }

  public Task<IdentityResult>
  RegisterAdmin(ApplicationUserRegisterDTO userDTO)
  {
    var user = new ApplicationUser()
    {
      UserName = userDTO.UserName,
      Email = userDTO.Email,
      PhoneNumber = userDTO.PhoneNumber
    };
    var claim = new System.Security.Claims.Claim(ApplicationClaims.Role,
                                                 ApplicationRoles.Admin);
    _userManager.AddClaimAsync(user, claim);
    return _userManager.CreateAsync(user, userDTO.Password);
  }
}
