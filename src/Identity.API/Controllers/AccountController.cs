using hms.Identity.API.Services;
using hms.Identity.API.Models;
using hms.Identity.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using hms.Identity.API.Authorization.Constants;
namespace hms.Identity.API.Controllers;
[Route("[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
  private IAccountService<ApplicationUser, ApplicationUserLoginDTO,
                          ApplicationUserRegisterDTO> _accountService
  {
    get;
  }
  public AccountController(
      IAccountService<ApplicationUser, ApplicationUserLoginDTO,
                      ApplicationUserRegisterDTO> accountService)
  {
    this._accountService = accountService;
  }

  [AllowAnonymous]
  [HttpPost("register")]
  public async Task Register(ApplicationUserRegisterDTO user)
  {
    await _accountService.Register(user);
    return;
  }

  // [Authorize(Policy = ApplicationPolicy.Administrator)]
  [HttpPost("register/admin")]
  public async Task RegisterAdmin(ApplicationUserRegisterDTO user)
  {
    await _accountService.RegisterAdmin(user);
    return;
  }

  [AllowAnonymous]
  [HttpPost("login/")]
  public async Task Login(ApplicationUserLoginDTO user)
  {
    await _accountService.SignInUsingUserNameOrEmailAsync(user);
    return;
  }

  [HttpGet("getClaims")]
  public async Task<IList<System.Security.Claims.Claim>>
  GetClaims(string userName)
  {
    return await _accountService.GetClaimsUserUsingUserName(userName);
  }

  [HttpPost("changePassword")]
  public async Task ChangePassword(ApplicationUserChangePasswordDTO user)
  {
    await _accountService.ChangePassword(user.EmailOrUserName, user.OldPassword,
                                         user.NewPassword);
  }

  [HttpPost("refresh")]
  public async Task<string> Refresh() { return "refresh"; }

  [HttpPost("forgotPassword")]
  public async Task<string> ForgotPassword() { return "forgot password"; }
}
