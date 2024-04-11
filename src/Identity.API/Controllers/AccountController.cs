using hms.Identity.API.Services;
using hms.Identity.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace hms.Identity.API.Controllers;
[Route("[controller]")]
[ApiController]
public class AccountController : ControllerBase {
  private IAccountService<ApplicationUser> _accountService { get; }

  public AccountController(IAccountService<ApplicationUser> accountService) {
    this._accountService = accountService;
  }

  [HttpPost("register")]
  public async Task Register(ApplicationUser user, string password) {
    await _accountService.Register(user, password);
    return;
  }

  [HttpPost("login/email/")]
  public async Task LoginEmail(string userName, string Password) {
    await _accountService.SignInUsingEmail(userName, Password);
    return;
  }

  [HttpPost("login/username/")]
  public async Task LoginUserName(string userName, string Password) {
    await _accountService.SignInUsingUserName(userName, Password);
    return;
  }

  [HttpPost("login/")]
  public async Task Login(string userName, string Password) {
    await _accountService.SignInUsingUserNameOrEmail(userName, Password);
    return;
  }

  [HttpPost("refresh")]
  public async Task<string> Refresh() { return "refresh"; }

  [HttpPost("forgotPassword")]
  public async Task<string> ForgotPassword() { return "forgot password"; }

  [HttpPost("changePassword")]
  public async Task ChangePassword(ApplicationUser user, string oldPassword,
                                   string newPassword) {
    await _accountService.ChangePassword(user, oldPassword, newPassword);
  }
}
