using hms.Identity.API.Services;
using hms.Identity.API.Models;
using hms.Identity.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace hms.Identity.API.Controllers;
[Route("[controller]")]
[ApiController]
public class AccountController : ControllerBase {
  private IAccountService<ApplicationUser, ApplicationUserLoginDTO,
                          ApplicationUserRegisterDTO> _accountService {
    get;
  }

  public AccountController(
      IAccountService<ApplicationUser, ApplicationUserLoginDTO,
                      ApplicationUserRegisterDTO> accountService) {
    this._accountService = accountService;
  }

  [HttpPost("register")]
  public async Task Register(ApplicationUserRegisterDTO user) {
    await _accountService.Register(user);
    // await _accountService.Register(new ApplicationUserResponseDTO());
    return;
  }
  [HttpPost("registerAdmin")]
  public async Task RegisterAdmin(ApplicationUserRegisterDTO user) {
    await _accountService.RegisterAdmin(user);
    // await _accountService.Register(new ApplicationUserResponseDTO());
    return;
  }
  // [HttpPost("login/email/")]
  // public async Task LoginEmail(string userName, string Password)
  // {
  //   await _accountService.SignInUsingEmailAsync(userName, Password);
  //   return;
  // }
  //
  // [HttpPost("login/username/")]
  // public async Task LoginUserNameAsync(string userName, string Password)
  // {
  //   await _accountService.SignInUsingUserNameAsync(userName, Password);
  //   return;
  // }
  [HttpGet("getClaims")]
  public async Task<IList<System.Security.Claims.Claim>>
  GetClaims(string userName) {
    return await _accountService.GetClaimsUserUsingUserName(userName);
  }

  [HttpPost("login/")]
  public async Task Login(ApplicationUserLoginDTO user) {
    await _accountService.SignInUsingUserNameOrEmailAsync(user);
    return;
  }

  [HttpPost("refresh")]
  public async Task<string> Refresh() { return "refresh"; }

  [HttpPost("forgotPassword")]
  public async Task<string> ForgotPassword() { return "forgot password"; }

  [HttpPost("changePassword")]
  public async Task ChangePassword(ApplicationUserChangePasswordDTO user) {
    await _accountService.ChangePassword(user.EmailOrUserName, user.OldPassword,
                                         user.NewPassword);
  }
}
