using hms.Identity.API.Services;
using hms.Identity.API.Services.Interfaces;
using hms.Identity.API.Models;
using hms.Identity.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace hms.Identity.API.Controllers;
[Route("[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
  private ILoginService<ApplicationUserLoginDTO> _loginService { get; }
  private IRegisterService<ApplicationUserRegisterDTO> _registerService { get; }
  private IPasswordService _passwordService { get; }
  private IAccountEmailService _accountEmailService { get; }
  private IUserInformationService _userInformationService { get; }

  public AccountController(
      ILoginService<ApplicationUserLoginDTO> loginService,
      IRegisterService<ApplicationUserRegisterDTO> registerService,
      IPasswordService passwordService,
      IAccountEmailService accountEmailService,
      IUserInformationService userInformationService)
  {
    this._loginService = loginService;
    this._registerService = registerService;
    this._passwordService = passwordService;
    this._accountEmailService = accountEmailService;
    this._userInformationService = userInformationService;
  }

  [AllowAnonymous]
  [HttpPost("register")]
  public async Task Register(ApplicationUserRegisterDTO user)
  {
    await _registerService.Register(user);
    return;
  }

  // [Authorize(Policy = ApplicationPolicy.Administrator)]
  [HttpPost("register/admin")]
  public async Task RegisterAdmin(ApplicationUserRegisterDTO user)
  {
    await _registerService.RegisterAdmin(user);
    return;
  }

  [AllowAnonymous]
  [HttpPost("login/")]
  public async Task Login(ApplicationUserLoginDTO user)
  {
    await _loginService.SignInUsingUserNameOrEmailAsync(user);
    return;
  }

  [HttpGet("getClaims")]
  public async Task<IList<System.Security.Claims.Claim>>
  GetClaims(string userName)
  {
    return await _userInformationService.GetClaimsUserUsingUserName(userName);
  }

  [HttpPost("changePassword")]
  public async Task ChangePassword(ApplicationUserChangePasswordDTO user)
  {
    await _passwordService.ChangePassword(user.EmailOrUserName,
                                          user.OldPassword, user.NewPassword);
  }

  [HttpPost("forgotPassword")]
  public async Task<string>
  ForgotPassword(ApplicationUserForgetPasswordDTO user)
  {
    var link = await _passwordService.ForgetPassword(user.Email);
    return link;
  }

  [HttpPost("resetPassword")]
  public async Task<string>
  ResetPassword(ApplicationUserResetPasswordDTO request)
  {
    return await _passwordService.ResetPassword(request.Email, request.Token,
                                                request.Password);
  }

  [HttpPost("sendConfirmationEmail")]
  public async Task<string> SendConfirmationEmail(string email)
  {
    return await _accountEmailService.SendConfirmationEmail(email);
  }

  [HttpGet("confirmEmail")]
  public async Task<string> ConfirmEmail(string userId, string code)
  {
    var result = await _accountEmailService.ConfirmEmail(userId, code);
    return "Thank you for confirming your email";
  }
}
