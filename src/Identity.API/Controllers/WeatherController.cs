
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using hms.Identity.API.Models;
using hms.Identity.API.Authorization.Constants;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
[Route("api/[controller]")]
[ApiController]
public class WeatherController : ControllerBase
{
  [Authorize]
  [HttpGet]
  public async Task<string> Get() { return "No Policy"; }

  [HttpGet("/administrators")]
  [Authorize(Policy = ApplicationPolicy.Administrators)]
  public async Task<string> GetAdmin() { return "Administrators"; }
}
