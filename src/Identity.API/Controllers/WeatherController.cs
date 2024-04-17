
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using hms.Identity.API.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
[Route("api/[controller]")]
[ApiController]
public class WeatherController : ControllerBase {
  private UserManager<ApplicationUser> _userManager { get; }
  public WeatherController(UserManager<ApplicationUser> userManager) {
    this._userManager = userManager;
  }
  //
  //
  [Authorize]
  [HttpPost("makeAdmin")]
  public async Task<IList<Claim>> MakeAdmin([FromBody] string userName) {
    var user = await _userManager.FindByNameAsync(userName);

    // if (!_userManager.SupportsUserClaim) {
    //   return "Claims not supported";
    // }
    //
    // if (!_userManager.SupportsUserRole) {
    //   return "Role not supported";
    // }

    Claim claim = new Claim("role", "admin");
    var result = _userManager.AddClaimAsync(user, claim);

    return await _userManager.GetClaimsAsync(user);
    // if (result.IsCompletedSuccessfully) {
    //   return user.Id + "is successfully made " + claim.Value;
    // } else if (result.Id
    //   return "Something Went Wrong";
  }

  [Authorize]
  [HttpGet]
  public async Task<string> Get() { return "No Policy"; }

  [HttpGet("/administrators")]
  [Authorize(Policy = "Administrators")]
  public async Task<string> GetAdmin() { return "Administrators"; }
}
