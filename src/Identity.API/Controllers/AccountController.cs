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
  public async Task ChangePassword(string emailOrUserName, string oldPassword,
                                   string newPassword) {
    await _accountService.ChangePassword(emailOrUserName, oldPassword,
                                         newPassword);
  }
}
