
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
[Route("api/[controller]")]
[ApiController]
public class WeatherController : ControllerBase {
  // public DatabaseTenantService TenantService { get; }
  // public TenantsController(DatabaseTenantService tenantService) {
  //   this.TenantService = tenantService;
  // }
  //
  [Authorize]
  [HttpGet]
  public async Task<string> Get() { return "hello"; }
}
