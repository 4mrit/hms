// using hms.Identity.API.Models;
// using Microsoft.AspNetCore.Identity;
// using hms.Identity.API.DTOs;
// using System.ComponentModel.DataAnnotations;
// using hms.Identity.API.Authorization.Constants;
// using Microsoft.AspNetCore.WebUtilities;
// using System.Text;
// using System.Text.Encodings.Web;
// using hms.Identity.API.Services.Interfaces;
//
// namespace hms.Identity.API.Services;
//
// public class EFAccountService
//     : IAccountService<ApplicationUser, ApplicationUserLoginDTO,
//                       ApplicationUserRegisterDTO>
// {
//   private SignInManager<ApplicationUser> _signInManager;
//   private UserManager<ApplicationUser> _userManager;
//   private IEmailSender _emailSender;
//   private static readonly EmailAddressAttribute _emailAddressAttribute =
//   new();
//
//   public EFAccountService(SignInManager<ApplicationUser> signInManager,
//                           UserManager<ApplicationUser> userManager,
//                           IEmailSender emailSender)
//   {
//     _signInManager = signInManager;
//     _userManager = userManager;
//     _emailSender = emailSender;
//   }
//
//   public async Task<SignInResult> SignInUsingUserNameAsync(string userName,
//                                                            string Password)
//   {
//     return await _signInManager.PasswordSignInAsync(userName, Password,
//     false,
//                                                     false);
//   }
//
//   public async Task<SignInResult> SignInUsingEmailAsync(string Email,
//                                                         string Password)
//   {
//     if (!_userManager.SupportsUserEmail)
//       throw new NotSupportedException(
//           $"{nameof(SignInUsingEmailAsync)} requires a user store with email
//           support.");
//
//     var user = await _userManager.FindByEmailAsync(Email);
//     if (user is null)
//       throw new ValidationException("user not found , email : " + Email);
//
//     return await _signInManager.PasswordSignInAsync(
//         user, Password, isPersistent: false, lockoutOnFailure: false);
//   }
//
//   public async Task<SignInResult>
//   SignInUsingUserNameOrEmailAsync(ApplicationUserLoginDTO user)
//   {
//     if (string.IsNullOrEmpty(user.EmailOrUserName))
//       throw new ValidationException("Email or Username Required !!");
//
//     if (_emailAddressAttribute.IsValid(user.EmailOrUserName))
//       return await SignInUsingEmailAsync(user.EmailOrUserName,
//       user.Password);
//     else
//       return await SignInUsingUserNameAsync(user.EmailOrUserName,
//                                             user.Password);
//   }
//   public Task Register(ApplicationUserRegisterDTO userDTO)
//   {
//
//     if (string.IsNullOrEmpty(userDTO.Email) &&
//         string.IsNullOrEmpty(userDTO.UserName))
//     {
//       throw new ValidationException(
//           "UserName or Email Required for User Registration");
//     }
//
//     var user = new ApplicationUser()
//     {
//       UserName = userDTO.UserName,
//       Email = userDTO.Email,
//       PhoneNumber = userDTO.PhoneNumber
//     };
//
//     return _userManager.CreateAsync(user, userDTO.Password);
//   }
//   public Task RegisterAdmin(ApplicationUserRegisterDTO userDTO)
//   {
//
//     if (string.IsNullOrEmpty(userDTO.Email) &&
//         string.IsNullOrEmpty(userDTO.UserName))
//     {
//       throw new ValidationException(
//           "UserName or Email Required for User Registration");
//     }
//
//     var user = new ApplicationUser()
//     {
//       UserName = userDTO.UserName,
//       Email = userDTO.Email,
//       PhoneNumber = userDTO.PhoneNumber
//     };
//
//     var result = _userManager.CreateAsync(user, userDTO.Password);
//     var claim = new System.Security.Claims.Claim(ApplicationClaims.Role,
//                                                  ApplicationRoles.Admin);
//     return _userManager.AddClaimAsync(user, claim);
//   }
//
//   public async Task<IdentityResult> ChangePassword(string emailOrUserName,
//                                                    string oldPassword,
//                                                    string newPassword)
//   {
//     var user = await GetUserFromEmailOrUserName(emailOrUserName);
//     return await _userManager.ChangePasswordAsync(user, oldPassword,
//                                                   newPassword);
//   }
//
//   private async Task<ApplicationUser>
//   GetUserFromEmailOrUserName(string emailOrUserName)
//   {
//     if (string.IsNullOrEmpty(emailOrUserName))
//       throw new ValidationException("Email or Username Required !!");
//
//     if (_emailAddressAttribute.IsValid(emailOrUserName))
//     {
//       return await _userManager.FindByEmailAsync(emailOrUserName);
//     }
//     else
//     {
//       return await _userManager.FindByNameAsync(emailOrUserName);
//     }
//   }
//
//   public async Task<IList<System.Security.Claims.Claim>>
//   GetClaimsUserUsingUserName(string userName)
//   {
//
//     return await _userManager.GetClaimsAsync(
//         await _userManager.FindByNameAsync(userName));
//   }
//
//   public async Task<string> ResetPassword(string email, string token,
//                                           string newPassword)
//   {
//     var user = await _userManager.FindByEmailAsync(email);
//     if (user is null)
//     {
//       return "No User";
//     }
//     if (!await _userManager.IsEmailConfirmedAsync(user))
//     {
//       return "Email Not Confirmed";
//     }
//     token = DecodeHtmlSuitableCodeToToken(token);
//     await _userManager.ResetPasswordAsync(user, token, newPassword);
//     return "";
//   }
//
//   public async Task<string> ForgetPassword(string Email)
//   {
//     var user = await _userManager.FindByEmailAsync(Email);
//     if (user is null)
//       return "No user Found";
//
//     if (!await _userManager.IsEmailConfirmedAsync(user))
//     {
//       return "Email Not Confirmed";
//     }
//
//     var token = await _userManager.GeneratePasswordResetTokenAsync(user);
//     var tokenSuitableForHtml = EncodeTokenToHtmlSuitableCode(token);
//
//     var result = _emailSender.SendEmailAsync(Email, "Password Reset",
//                                              tokenSuitableForHtml);
//     await result;
//     if (!result.IsCompletedSuccessfully)
//     {
//       var ex = result.Exception;
//       Console.WriteLine(ex.Message);
//       return "something went wrong";
//     }
//     // TODO: THIS IS FOR TESTING ONLY DONT RETURN LINK IN PRODUCTION
//     return tokenSuitableForHtml;
//   }
//
//   public async Task<string> SendConfirmationEmail(string Email)
//   {
//     var user = await _userManager.FindByEmailAsync(Email);
//     if (user is null)
//       return "No user Found";
//
//     var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
//
//     var tokenSuitableForHtml = EncodeTokenToHtmlSuitableCode(token);
//     var confirmationLink =
//         $@"http://localhost:5085/Account/confirmEmail?userId={user.Id}&code={tokenSuitableForHtml}";
//     var body = $@"
//         Click on the <a href='{confirmationLink}'> link </a>to confirm your
//         Email !!
//       ";
//     var result = _emailSender.SendEmailAsync(Email, "Confirm Email", body);
//     await result;
//     if (!result.IsCompletedSuccessfully)
//     {
//       var ex = result.Exception;
//       Console.WriteLine(ex.Message);
//       return "something went wrong";
//     }
//     return tokenSuitableForHtml;
//   }
//
//   public async Task<string> ConfirmEmail(string userId, string code)
//   {
//     var user = await _userManager.FindByIdAsync(userId);
//     var token = DecodeHtmlSuitableCodeToToken(code);
//     if (user is null)
//     {
//       return "No User";
//     }
//     await _userManager.ConfirmEmailAsync(user, token);
//     return "";
//   }
//
//   private string EncodeTokenToHtmlSuitableCode(string token)
//   {
//     var tokenEncoded =
//         WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
//     return HtmlEncoder.Default.Encode(tokenEncoded);
//   }
//
//   private string DecodeHtmlSuitableCodeToToken(string htmlSuitableCode)
//   {
//     return Encoding.UTF8.GetString(
//         WebEncoders.Base64UrlDecode(htmlSuitableCode));
//   }
// }
