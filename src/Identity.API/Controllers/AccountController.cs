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
  public async Task<string> Register() { return "register"; }

  [HttpPost("login/")]
  public async Task Login(string userName, string Password) {
    await _accountService.SignIn(userName, Password);
    return;
  }

  [HttpPost("refresh")]
  public async Task<string> Refresh() { return "refresh"; }

  [HttpPost("forgotPassword")]
  public async Task<string> ForgotPassword() { return "forgot password"; }

  [HttpPost("resetPassword")]
  public async Task<string> ResetPassword() { return "reset password"; }
}
