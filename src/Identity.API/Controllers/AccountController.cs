using hms.Identity.API.Services;
using hms.Identity.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace hms.Identity.API.Controllers;
[Route("[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
  private IAccountService<ApplicationUser, ApplicationUserRequestDTO,
                          ApplicationUserResponseDTO> _accountService
  {
    get;
  }

  public AccountController(
      IAccountService<ApplicationUser, ApplicationUserRequestDTO,
                      ApplicationUserResponseDTO> accountService)
  {
    this._accountService = accountService;
  }

  [HttpPost("register")]
  public async Task Register(ApplicationUser user, string password)
  {
    await _accountService.Register(user, password);
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
  public async Task Login(ApplicationUserRequestDTO user)
  {
    await _accountService.SignInUsingUserNameOrEmailAsync(user);
    return;
  }

  [HttpPost("refresh")]
  public async Task<string> Refresh() { return "refresh"; }

  [HttpPost("forgotPassword")]
  public async Task<string> ForgotPassword() { return "forgot password"; }

  [HttpPost("changePassword")]
  public async Task ChangePassword(ApplicationUser user, string oldPassword,
                                   string newPassword)
  {
    await _accountService.ChangePassword(user, oldPassword, newPassword);
  }
}
