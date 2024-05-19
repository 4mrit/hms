// TenantsController.cs
using Microsoft.AspNetCore.Mvc;
using thms.Tenant.API.Services;
using thms.Tenant.API.Model;
using thms.Tenant.API.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace thms.Tenant.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TenantsController : ControllerBase {
  public DatabaseTenantService TenantService { get; }
  public TenantsController(DatabaseTenantService tenantService) {
    this.TenantService = tenantService;
  }

  [HttpGet]
  public async Task<IEnumerable<HospitalTenant>> GetAllTenants() {
    return await TenantService.GetAllTenants();
  }

  [HttpGet("{TenantId}")]
  public async Task<ActionResult<HospitalTenant>> GetTenantById(int TenantId) {
    return await TenantService.GetTenantById(TenantId);
  }

  [HttpPut("{TenantId}")]
  public async Task<ActionResult<HospitalTenant>>
  UpdateTenant(int TenantId, HospitalTenant Tenant) {
    if (await TenantService.UpdateTenantUsingId(TenantId, Tenant) == null) {
      return NotFound();
    }
    return NoContent();
  }

  [HttpPost]
  public async Task<ActionResult<HospitalTenant>>
  AddTenant(HospitalTenant Tenant) {
    HospitalTenant AddedTenant = await TenantService.AddTenant(Tenant);
    return CreatedAtAction(
        actionName: nameof(GetTenantById),
        controllerName: ControllerContext.GetControllerName(),
        routeValues: new { TenantId = AddedTenant.Id }, value: AddedTenant);
  }

  [HttpDelete("{TenantId}")]
  public async Task<ActionResult> DeleteTenant(int TenantId) {
    await TenantService.DeleteTenant(TenantId);
    return NoContent();
  }

  [Route("AddDummyData")]
  [HttpGet]
  public ActionResult SetDummyData() {
    TenantService.InsertDummyData();
    return Ok();
  }
}
