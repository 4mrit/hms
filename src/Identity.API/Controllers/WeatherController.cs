
using Microsoft.AspNetCore.Mvc;
[Route("api/[controller]")]
[ApiController]
public class WeatherController : ControllerBase
{
  // public DatabaseTenantService TenantService { get; }
  // public TenantsController(DatabaseTenantService tenantService) {
  //   this.TenantService = tenantService;
  // }
  //
  [HttpGet]
  public async Task<string> Get() { return "hello"; }
}
