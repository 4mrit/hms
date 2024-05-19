using thms.Identity.API.DTOs;
using thms.Identity.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace thms.Identity.API.Controllers;

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
  public async Task<IActionResult> Register(ApplicationUserRegisterDTO user)
  {
    var result = await _registerService.Register(user);
    return result.Succeeded ? Ok(result) : BadRequest(result);
  }

  // [Authorize(Policy = ApplicationPolicy.Administrator)]
  [HttpPost("register/admin")]
  public async Task<IActionResult>
  RegisterAdmin(ApplicationUserRegisterDTO user)
  {
    var result = await _registerService.RegisterAdmin(user);
    return result.Succeeded ? Ok(result) : BadRequest(result);
  }

  [AllowAnonymous]
  [HttpPost("login/")]
  public async Task<IActionResult> Login(ApplicationUserLoginDTO user)
  {
    var result = await _loginService.SignInUsingUserNameOrEmailAsync(user);
    return result.Succeeded ? Ok(result) : BadRequest(result);
  }

  [HttpGet("getClaims")]
  public async Task<IList<System.Security.Claims.Claim>>
  GetClaims(string userName)
  {
    return await _userInformationService.GetClaimsUserUsingUserName(userName);
  }

  [HttpPost("changePassword")]
  public async Task<IActionResult>
  ChangePassword(ApplicationUserChangePasswordDTO user)
  {
    var result = await _passwordService.ChangePassword(
        user.EmailOrUserName, user.OldPassword, user.NewPassword);
    return result.Succeeded ? Ok(result) : BadRequest(result);
  }

  [HttpPost("forgotPassword")]
  public async Task<IActionResult>
  ForgotPassword(ApplicationUserForgetPasswordDTO user)
  {
    await _passwordService.ForgetPassword(user.Email);
    return Ok();
  }

  [HttpPost("resetPassword")]
  public async Task<IActionResult>
  ResetPassword(ApplicationUserResetPasswordDTO request)
  {
    var result = await _passwordService.ResetPassword(
        request.UserId, request.Token, request.Password);
    return result.Succeeded ? Ok(result) : BadRequest(result);
  }

  [HttpPost("sendConfirmationEmail")]
  public async Task<IActionResult> SendConfirmationEmail(string email)
  {
    await _accountEmailService.SendConfirmationEmail(email);
    return Ok();
  }

  [HttpGet("confirmEmail")]
  public async Task<IActionResult> ConfirmEmail(string userId, string code)
  {
    var result = await _accountEmailService.ConfirmEmail(userId, code);
    return result.Succeeded ? Ok(result) : BadRequest(result);
  }
}
