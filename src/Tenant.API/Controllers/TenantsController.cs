using Microsoft.AspNetCore.Mvc;
using hms.Tenant.API.Services;
using hms.Tenant.API.Model;
using hms.Tenant.API.Extensions;
namespace hms.Tenant.API.Controllers;

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
  public async Task<ActionResult<HospitalTenant>> GetTenantByID(int TenantId) {
    return await TenantService.GetTenantById(TenantId);
  }

  [HttpGet("Features")]
  public async Task<IEnumerable<Feature>> GetAllFeatures() {
    return await TenantService.GetAllFeatures();
  }

  [HttpPut]
  public async Task<ActionResult<HospitalTenant>>
  UpdateTenant(HospitalTenant Tenant) {
    if (await TenantService.UpdateTenant(Tenant) == null) {
      return NotFound();
    }
    return NoContent();
  }

  [HttpPost]
  public async Task<ActionResult<HospitalTenant>>
  AddTenant(HospitalTenant Tenant) {
    HospitalTenant AddedTenant = await TenantService.AddTenant(Tenant);
    return CreatedAtAction(
        actionName: nameof(GetTenantByID),
        controllerName: ControllerContext.GetControllerName(),
        routeValues: new { id = AddedTenant.Id }, value: AddedTenant);
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
