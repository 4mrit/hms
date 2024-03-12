using Microsoft.AspNetCore.Mvc;
using hms.Tenant.API.Services;
using hms.Tenant.API.Model;
namespace hms.Tenant.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class TenantsController : ControllerBase
{
    public DatabaseTenantService TenantService { get;}
    public TenantsController(DatabaseTenantService tenantService)
    {
        this.TenantService = tenantService;
    }

    [HttpGet]
    public IEnumerable<HospitalTenant> GetAllTenants(){
        return TenantService.GetAllTenants();
    }

    [Route("AddDummyData")]
    [HttpGet]
    public ActionResult SetDummyData(){
        TenantService.InsertDummyData();
        return Ok();
    }
}
