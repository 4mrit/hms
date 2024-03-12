using Microsoft.AspNetCore.Mvc;
using hms.Tenant.API.Services;
using hms.Tenant.API.Model;
using hms.Tenant.API.Extensions;
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

  [HttpPut]
  public async Task<ActionResult<HospitalTenant>> UpdateTenant(HospitalTenant Tenant)
  {
    if (TenantService.UpdateTenant(Tenant) == null){
      return NotFound();
    }
    return NoContent();
  }

  [HttpPost]
  public async Task<ActionResult<HospitalTenant>> AddTenant(HospitalTenant Tenant)
  {
    HospitalTenant AddedTenant = TenantService.AddTenant(Tenant);
    return CreatedAtAction(
        actionName: nameof(GetTenant) ,
        controllerName: ControllerContext.GetControllerName(),
        routeValues: new { id = AddedTenant.Id },
        value: AddedTenant
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
