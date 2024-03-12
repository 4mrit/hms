using Microsoft.AspNetCore.Mvc;
using hms.Tenant.API.Services;
using hms.Tenant.API.Model;
namespace hms.Tenant.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TenantsController : ControllerBase
{
  public DatabaseTenantService TenantService { get; }
  public TenantsController(DatabaseTenantService tenantService)
  {
    this.TenantService = tenantService;
  }

  [HttpGet]
  public IEnumerable<HospitalTenant> GetAllTenants()
  {
    return TenantService.GetAllTenants();
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<HospitalTenant>> GetTenant(int id)
  {
    return TenantService.GetTenant(id);
  }

  [HttpPost]
  public async Task<ActionResult<HospitalTenant>> AddTenant(HospitalTenant Tenant)
  {
    var createdResource = new { Id = 1 };
    var actionName = nameof(GetTenant);
    var routeValues = new { id = createdResource.Id };
    return CreatedAtAction(
        actionName,
        createdResource,
        routeValues
    );
  }

  [Route("AddDummyData")]
  [HttpGet]
  public ActionResult SetDummyData()
  {
    TenantService.InsertDummyData();
    return Ok();
  }
}
