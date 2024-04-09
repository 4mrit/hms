using hms.Identity.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace hms.Identity.API.Controllers;
[Route("[controller]")]
[ApiController]
public class AccountController : ControllerBase {
  private IAccountService _accountService { get; }

  public AccountController(IAccountService accountService) {
    this._accountService = accountService;
  }

  [HttpPost("register")]
  public async Task<string> Register() { return "register"; }

  [HttpPost("login")]
  public async Task<string> Login() { return _accountService.LoginUser(); }

  [HttpPost("refresh")]
  public async Task<string> Refresh() { return "refresh"; }

  [HttpPost("forgotPassword")]
  public async Task<string> ForgotPassword() { return "forgot password"; }

  [HttpPost("resetPassword")]
  public async Task<string> ResetPassword() { return "reset password"; }
}
